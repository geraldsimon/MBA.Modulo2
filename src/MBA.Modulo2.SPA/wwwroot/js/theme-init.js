(function() {
    const theme = localStorage.getItem('selectedTheme') || 'light';
    document.documentElement.setAttribute('data-theme', theme);
})();