namespace MBA.Modulo2.Spa.Services
{
    public interface IThemeService
    {
        string CurrentTheme { get; }
        event Action? OnThemeChanged;
        Task SetThemeAsync(string theme);
        Task<string> GetThemeAsync();
    }
}