using Blazored.LocalStorage;

namespace MBA.Modulo2.Spa.Services.Api
{
    public class CustomHttpHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;
        public CustomHttpHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.ToLower().Contains("login") ||
                request.RequestUri.AbsolutePath.ToLower().Contains("register"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var JwtToken = await _localStorageService.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(JwtToken))
            {
                request.Headers.Add("Authorization", $"bearer {JwtToken}");
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
