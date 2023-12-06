using AutoMapper;
using Core;
using Core.Models;

namespace FE.Services;

public class UserService
{
    private const string BaseUrl = "https://localhost:7154";

    private readonly IMapper _mapper;
    private readonly IHttpClientFactory _httpClientFactory;

    public UserService(IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReadDto>?> GetAll(Pagination? pagination = null)
    {
        var httpClient = _httpClientFactory.CreateClient();
        if (pagination == null)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<UserReadDto>>($"{BaseUrl}/user/all");
        }
        return await httpClient.GetFromJsonAsync<IEnumerable<UserReadDto>>($"{BaseUrl}/user/all?Page={pagination.Page}&ItemsPerPage={pagination.ItemsPerPage}&QueryFilter.FilterValue={pagination.FilterValue}&QuerySort.SortParam={pagination.QuerySort.SortParam}&QuerySort.SortOrder={pagination.QuerySort.SortOrder}");
    }

    public async Task<UserReadDto?> GetById(int id)
    {
        var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.GetFromJsonAsync<UserReadDto>($"{BaseUrl}/user/{id}");
    }

    public async Task<HttpResponseMessage> AddUser(UserCreateDto user)
    {
        var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.PostAsJsonAsync($"{BaseUrl}/user/add", user);
    }

    public async Task<HttpResponseMessage> UpdateUser(int id, UserReadDto userToEdit)
    {
        var userToUpdate = _mapper.Map<UserUpdateDto>(userToEdit);
        var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.PutAsJsonAsync($"{BaseUrl}/user/update/{id}", userToUpdate);
    }

    public async Task<HttpResponseMessage> DeleteUser(int id)
    {
        var httpClient = _httpClientFactory.CreateClient();
        return await httpClient.DeleteAsync($"{BaseUrl}/user/delete/{id}");
    }
}