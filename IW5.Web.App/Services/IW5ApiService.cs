using IW5.Models.Entities;
using System.Net.Http.Json;

public class IW5ApiService
{
    private readonly HttpClient _httpClient;

    public IW5ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Example: Get data from API
    public async Task<List<Form>> GetAllFormsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Form>>("api/Forms/all");
    }

    public async Task<Form> GetFormByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Form>($"api/forms/{id}");
    }

}



