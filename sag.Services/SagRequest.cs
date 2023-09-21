using System.Net.Http.Headers;
using System.Text.Json;

namespace sag.Services;

public class SagRequest
{
    private static HttpClient? _client { get; set; }
    private static HttpRequestMessage _request { get; set; }

    private static async Task<T?> BaseRequest<T>(HttpMethod method, Uri uri, object content)
    {
        _client = new HttpClient();
        _request = new HttpRequestMessage();
        _request.Method = method;
        _request.RequestUri = uri;
        _request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", SagSettings.Token ?? string.Empty);
        _request.Content = new StringContent(JsonSerializer.Serialize(content))
        {
            Headers =
            {
                ContentType = new MediaTypeHeaderValue("application/json")
            }
        };

        using var response = await _client.SendAsync(_request);

        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(body);
    }

    public static async Task<T?> Post<T>(Uri uri, object content)
    {
        return await BaseRequest<T>(HttpMethod.Post, uri, content);
    }

    public static async Task<T?> Get<T>(Uri uri)
    {
        return await BaseRequest<T>(HttpMethod.Post, uri, new { });
    }
}