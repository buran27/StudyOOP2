using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


public class RequestResult
{
    public string Url { get; }
    public string Json { get; }

    public RequestResult(string url, string json)
    {
        Url = url;
        Json = json;
    }
}

public class AsyncJsonRequestService
{
  private readonly HttpClient _httpClient;

  public AsyncJsonRequestService()
  {
    _httpClient = new HttpClient();
  }
    public async Task<List<RequestResult>> LoadAllAsync(string[] urls)
    {
        if (urls == null || urls.Length == 0)
            throw new ArgumentException("Список адресов не может быть пустым.");

        Task<RequestResult>[] tasks = new Task<RequestResult>[urls.Length];

        for (int i = 0; i < urls.Length; i++)
        {
            tasks[i] = LoadJsonAsync(urls[i]);
        }

        RequestResult[] results = await Task.WhenAll(tasks);

        return new List<RequestResult>(results);
    }

    private async Task<RequestResult> LoadJsonAsync(string url)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        return new RequestResult(url, json);
    }
}
