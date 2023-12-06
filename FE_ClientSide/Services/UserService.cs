using System.Net.Http.Json;
using AutoMapper;
using Core;
using Core.Models;
using HttpClientFactory;

namespace FE_ClientSide.Services;

public class UserService
{
    private const string BaseUrl = "https://localhost:7154";

    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(BaseUrl);
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReadDto>?> GetAll(Pagination? pagination = null)
    {
        if (pagination == null)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserReadDto>>("/user/all");
        }
        return await _httpClient.GetFromJsonAsync<IEnumerable<UserReadDto>>($"/user/all?Page={pagination.Page}&ItemsPerPage={pagination.ItemsPerPage}&QueryFilter.FilterValue={pagination.FilterValue}&QuerySort.SortParam={pagination.QuerySort.SortParam}&QuerySort.SortOrder={pagination.QuerySort.SortOrder}");
    }

    public async Task<UserReadDto?> GetById(int id)
    {
        return await _httpClient.GetFromJsonAsync<UserReadDto>($"/user/{id}");
    }

    public async Task<HttpResponseMessage> AddUser(UserCreateDto user)
    {
        return await _httpClient.PostAsJsonAsync($"/user/add", user);
    }

    public async Task<HttpResponseMessage> UpdateUser(int id, UserReadDto? userToEdit)
    {
        var userToUpdate = _mapper.Map<UserUpdateDto>(userToEdit);
        return await _httpClient.PutAsJsonAsync($"/user/update/{id}", userToUpdate);
    }

    public async Task<HttpResponseMessage> DeleteUser(int id)
    {
        return await _httpClient.DeleteAsync($"/user/delete/{id}");
    }
}