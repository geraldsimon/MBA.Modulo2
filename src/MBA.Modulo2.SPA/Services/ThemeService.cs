using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MBA.Modulo2.Spa.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string THEME_KEY = "selectedTheme";
        private const string DEFAULT_THEME = "light";
        
        public string CurrentTheme { get; private set; } = DEFAULT_THEME;
        public event Action? OnThemeChanged;

        public ThemeService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetThemeAsync()
        {
            try
            {
                var theme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", THEME_KEY);
                CurrentTheme = string.IsNullOrEmpty(theme) ? DEFAULT_THEME : theme;
                return CurrentTheme;
            }
            catch
            {
                CurrentTheme = DEFAULT_THEME;
                return CurrentTheme;
            }
        }

        public async Task SetThemeAsync(string theme)
        {
            if (theme != "light" && theme != "dark")
                theme = DEFAULT_THEME;

            CurrentTheme = theme;
            
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", THEME_KEY, theme);
                await _jsRuntime.InvokeVoidAsync("document.documentElement.setAttribute", "data-theme", theme);
                
                OnThemeChanged?.Invoke();
            }
            catch
            {
                OnThemeChanged?.Invoke();
            }
        }
    }
}