using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;
using System.Net.Http.Json;

public class IW5ApiService
{
    private readonly HttpClient _httpClient;

    public IW5ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Form>> GetAllFormsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Form>>("api/Forms/all");
    }

    // Alternative to GetAllFormsAsync(). Will filter all data on server side. Bad: To many requests to Server, better filter forms on client side.
    public async Task<List<Form>> GetFilteredOrSortedFormsAsync(FormSortType sortType, string searchString)
    {
        //URL : /api/Forms/filter/{sortType}/{searchString}
        var url = $"api/Forms/filter/{(int)sortType}/{Uri.EscapeDataString(searchString)}";

        var response = await _httpClient.GetAsync(url);
        return await response.Content.ReadFromJsonAsync<List<Form>>();
    }
    public async Task<List<Question>> GetAllQuestionsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Question>>("api/Questions/all");
    }
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<User>>("api/Users/all");
    }
    public async Task DeleteFormAsync(Guid formId)
    {
        var response = await _httpClient.DeleteAsync($"api/Forms/{formId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error deleting form");
        }
    }

    public async Task<Form> GetFormByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<Form>($"api/forms/{id}");
    }

}



