// Opens the date picker on the specific input element clicked
function openPicker(input) {
  input?.showPicker?.();
  input?.focus();
}

// Toggles between showing and hiding the password
function togglePassword(button) {
  const container = button.closest('.password-input');
  const input = container.querySelector('input');
  const showIcon = button.querySelector('.show-icon');
  const hideIcon = button.querySelector('.hide-icon');

  const isPassword = input.type === 'password';
  input.type = isPassword ? 'text' : 'password';

  showIcon.classList.toggle('hidden', !isPassword);
  hideIcon.classList.toggle('hidden', isPassword);
}

// Opens menus when clicking their corresponding buttons using the data-toggle attribute
document.addEventListener("DOMContentLoaded", () => {
  const toggleButtons = document.querySelectorAll("[data-toggle]");
  const menus = document.querySelectorAll(".menu");

  toggleButtons.forEach(button => {
    button.addEventListener("click", e => {
      e.stopPropagation();
      const menuId = button.getAttribute("data-toggle");
      const menu = document.getElementById(menuId);
      const isMenuVisible = !menu.classList.contains("hidden");

      // Close all menus and remove active class from all buttons
      menus.forEach(m => m.classList.add("hidden"));
      toggleButtons.forEach(btn => btn.classList.remove("active"));

      // If it wasn't already visible, show it and activate button
      if (!isMenuVisible) {
        menu.classList.remove("hidden");
        button.classList.add("active");
      }
    });
  });

  // Clicking outside closes menus and deactivates buttons
  document.addEventListener("click", () => {
    menus.forEach(menu => menu.classList.add("hidden"));
    toggleButtons.forEach(btn => btn.classList.remove("active"));
  });

  // Prevent closing if clicking inside menu
  menus.forEach(menu => {
    menu.addEventListener("click", e => e.stopPropagation());
  });
});

// Handling the dark mode using a toggle button, system preference and saves a cookie to keep the choice
document.addEventListener('DOMContentLoaded', () => {
  const toggleBtn = document.getElementById('dark-mode-toggle');
  const root = document.documentElement;

  // Set theme and save as cookie for 1 year
  function setTheme(theme) {
    root.classList.add('no-theme-transition');

    root.setAttribute('data-theme', theme);
    document.cookie = `theme=${theme}; path=/; max-age=31536000`;

    setTimeout(() => {
      root.classList.remove('no-theme-transition');
    }, 0);
  }

  // Read cookie
  function getSavedTheme() {
    const match = document.cookie.match(/(?:^|; )theme=([^;]*)/);
    return match ? match[1] : null;
  }

  // Apply saved theme or fallback to system preference
  const savedTheme = getSavedTheme();
  const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

  if (savedTheme) {
    setTheme(savedTheme);
  } else {
    setTheme(systemPrefersDark ? 'dark' : 'light');
  }

  // Toggle on button click
  toggleBtn.addEventListener('click', () => {
    const current = root.getAttribute('data-theme');
    const newTheme = current === 'dark' ? 'light' : 'dark';
    setTheme(newTheme);
  });
});