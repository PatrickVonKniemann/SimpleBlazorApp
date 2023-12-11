using Blazored.LocalStorage;
using Core.Models;
using FE_ClientSide.Providers;

namespace FE_ClientSide.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly CustomAuthStateProvider _authStateProvider;

    public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorage, CustomAuthStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        var response = await _httpClient.PostAsJsonAsync("/auth", loginDto);
        if (response.IsSuccessStatusCode)
        {
            var token = await response.Content.ReadAsStringAsync();
            await _localStorage.SetItemAsync("authToken", token);
            _authStateProvider.NotifyUserAuthentication(token);
            return true;
        }

        return false;
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _authStateProvider.NotifyUserLogout();
    }
}
