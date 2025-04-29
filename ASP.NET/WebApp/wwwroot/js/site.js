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

// Closes overlay and optionally resets url
function closeOverlay(overlayId, resetUrlTo = null) {
  const overlay = document.getElementById(overlayId);

  if (overlay) {
    overlay.classList.add('hidden');

    // Reset the form inside the overlay
    const form = overlay.querySelector('form');
    if (form) {
      form.reset();

      // Resets the image when closing the add form and opening it again
      document.getElementById('upload-preview').src = '/images/upload-icon-border-round.svg';
      document.getElementById('imageFileInput').value = "";

      // Clear the validation errors
      const validationMessages = form.querySelectorAll('.field-validation-error, .input-validation-error');
      validationMessages.forEach(element => {
        element.textContent = '';
        element.classList.remove('field-validation-error');
        element.classList.remove('input-validation-error');
      });
    }
  }

  if (resetUrlTo && window.location.pathname !== resetUrlTo) {
    window.history.pushState({}, '', resetUrlTo);
  }
}

// Handles uploading images
function triggerUpload(button) {
  const wrapper = button.closest('.upload-wrapper');
  if (!wrapper) return;

  const fileInput = wrapper.querySelector('input[type="file"]');
  if (fileInput) {
    fileInput.click();
  }
}

// Handles previewing images on forms
function previewImage(event) {
  const input = event.target;
  const previewId = input.dataset.previewId;
  const preview = document.getElementById(previewId);

  if (!preview) return;

  if (!input.files || input.files.length === 0) {
    const original = preview.dataset.original;
    if (original) {
      preview.src = original;
    }
    
    return;
  }

  const file = input.files[0];
  const reader = new FileReader();
  reader.onload = function (e) {
    preview.src = e.target.result;
  };

  reader.readAsDataURL(file);
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
  const darkToggle = document.getElementById('dark-mode-toggle');
  const switchSlider = document.getElementById('theme-toggle-slider');
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

  // If there is a saved theme, set it to that, otherwise find the preference
  if (savedTheme) {
    setTheme(savedTheme);
  } else {
    setTheme(systemPrefersDark ? 'dark' : 'light');
  }

  // If the theme is set to dark, keep the slider active, otherwise remove active
  if (root.getAttribute('data-theme') === 'dark') {
    switchSlider.classList.add('active');
  } else {
    switchSlider.classList.remove('active');
  }

  // Toggle on button click
  darkToggle.addEventListener('click', () => {
    const current = root.getAttribute('data-theme');
    const newTheme = current === 'dark' ? 'light' : 'dark';
    setTheme(newTheme);
    switchSlider.classList.toggle('active');
  });
});
