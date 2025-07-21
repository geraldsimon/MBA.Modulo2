let currentOpenDropdown = null;

function toggleDropdown(event, button) {
    event.stopPropagation();
    event.preventDefault();

    const productId = button.getAttribute('data-product-id');
    const dropdown = document.getElementById('dropdown-' + productId);

    if (currentOpenDropdown && currentOpenDropdown !== dropdown) {
        currentOpenDropdown.classList.remove('show');
    }

    if (dropdown.classList.contains('show')) {
        dropdown.classList.remove('show');
        currentOpenDropdown = null;
    } else {
        dropdown.classList.add('show');
        currentOpenDropdown = dropdown;
    }
}

function closeAllDropdowns() {
    const dropdowns = document.querySelectorAll('.dropdown-menu');

    dropdowns.forEach(dropdown => {
        dropdown.classList.remove('show');
    });

    currentOpenDropdown = null;
}

document.addEventListener('click', function (event) {
    if (event.target.closest('.options-btn') || event.target.closest('.card-options')) {
        return;
    }

    if (event.target.closest('.dropdown-menu')) {
        return;
    }

    closeAllDropdowns();
});

document.addEventListener('keydown', function (event) {
    if (event.key === 'Escape') {
        closeAllDropdowns();
    }
});

document.addEventListener('click', function (event) {
    if (event.target.closest('.dropdown-item')) {
        return true;
    }
});