using System;
using System.Collections.Generic;
using System.Net.Http;

public class RequestResultsync
{
    public string Url { get; }
    public string Json { get; }

    public RequestResultsync(string url, string json)
    {
        Url = url;
        Json = json;
    }
}

public class SyncJsonRequestService
{
    private readonly HttpClient _httpClient;

    public SyncJsonRequestService()
    {
        _httpClient = new HttpClient();
    }

    public List<RequestResultsync> LoadAll(string[] urls)
    {
        if (urls == null || urls.Length == 0)
            throw new ArgumentException("Список адресов не может быть пустым.");

        List<RequestResultsync> results = new List<RequestResultsync>();

        for (int i = 0; i < urls.Length; i++)
        {
            RequestResultsync result = LoadJson(urls[i]);
            results.Add(result);
        }

        return results;
    }

    private RequestResultsync LoadJson(string url)
    {
        HttpResponseMessage response = _httpClient.GetAsync(url).GetAwaiter().GetResult();

        response.EnsureSuccessStatusCode();

        string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        return new RequestResultsync(url, json);
    }
}