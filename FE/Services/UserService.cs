using API.Models;
using Core;

namespace FE.Services;

public class UserService
{
    
    private readonly string baseUrl = "https://localhost:7154";
    
    private readonly IHttpClientFactory _httpClientFactory;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<UserReadDto>?> GetAll(Pagination? pagination = null)
    {
        var httpClient = _httpClientFactory.CreateClient();
        if (pagination == null)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<UserReadDto>>($"{baseUrl}/user/all");
        }
        return await httpClient.GetFromJsonAsync<IEnumerable<UserReadDto>>($"{baseUrl}/user/all?Page={pagination.Page}&ItemsPerPage={pagination.ItemsPerPage}&QueryFilter.FilterValue={pagination.FilterValue}&QuerySort.SortParam={pagination.QuerySort.SortParam}&QuerySort.SortOrder={pagination.QuerySort.SortOrder}");
    }

    public async Task<UserReadDto?> GetById(int id)
    {
        var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.GetFromJsonAsync<UserReadDto>($"{baseUrl}/user/{id}");
    }

    public async Task<HttpResponseMessage> AddUser(UserCreateDto user)
    {
        var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.PostAsJsonAsync($"{baseUrl}/user/add", user);
    }

    public async Task<HttpResponseMessage> UpdateUser(int id, UserUpdateDto userToUpdate)
    {
        var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.PutAsJsonAsync($"{baseUrl}/user/update/{id}", userToUpdate);
    }

    public async Task<HttpResponseMessage> DeleteUser(int id)
    {
        var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.DeleteAsync($"{baseUrl}/user/delete/{id}");
    }
}