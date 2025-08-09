using Blazored.LocalStorage;
using MBA.Modulo2.Spa.Services.Autenticacao.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace MBA.Modulo2.Spa.Services.Autenticacao.Implementacao
{
    public class AuthService(ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : IAuthService
    {
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}
