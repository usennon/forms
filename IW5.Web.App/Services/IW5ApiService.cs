﻿using IW5.Common.Enums.Sorts;
using IW5.Models.Entities;
using System.Net.Http.Json;
using IW5.BL.Models.ManipulationModels.FormsModels;
using IW5.BL.Models.ListModels;
using IW5.BL.Models.DetailModels;
using IW5.BL.Models.ManipulationModels.UserModels;

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
    public async Task<List<UserListModel>> GetAllUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<UserListModel>>("api/Users/all");
    }
    public async Task<UserDetailModel> GetUserByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<UserDetailModel>($"api/Users/{id}");
    }
    public async Task<List<Answer>> GetAllAnswersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Answer>>("api/Answers/all");
    }
    public async Task<Answer> GetAnswerAsync(Guid answerId)
    {
        return await _httpClient.GetFromJsonAsync<Answer>($"api/Answers/{answerId}");
    }
    public async Task DeleteAnswerAsync(Guid answerId)
    {
        var response = await _httpClient.DeleteAsync($"api/Answers/{answerId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error deleting form");
        }
    }
    public async Task DeleteUserAsync(Guid userId)
    {
        var response = await _httpClient.DeleteAsync($"api/Users/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error deleting form");
        }
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

    public async Task CreateFormAsync(FormForManipulationModel newForm)
    {
        var response = await _httpClient.PostAsJsonAsync("api/forms", newForm);
        response.EnsureSuccessStatusCode();
    }
    public async Task SubmitFormAsync(SubmitFormModel newForm)
    {
        var response = await _httpClient.PostAsJsonAsync("api/forms/submit", newForm);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateFormAsync(Guid id, FormForManipulationModel updatedForm)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/forms/{id}", updatedForm);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateUserAsync(Guid id, UserForManipulationModel updatedUser)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Users/{id}", updatedUser);

        response.EnsureSuccessStatusCode();
    }

    public async Task CreateUserAsync(UserForManipulationModel newUser)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Users", newUser);

        response.EnsureSuccessStatusCode();
    }

}



