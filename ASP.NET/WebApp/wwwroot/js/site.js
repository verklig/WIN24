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

// Resets the path to force reload the page
function resetBasePath() {
  const path = window.location.pathname.split('/').filter(Boolean);

  if (path.length > 0) {
    const isAdmin = path[0].toLowerCase() === 'admin';
    const basePath = isAdmin && path.length > 1
      ? `/${path[0]}/${path[1]}`
      : `/${path[0]}`;

    window.location.href = basePath;
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

    updateNotificationCount();
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

// Handling the selecting of members (users) in the project form
document.addEventListener('DOMContentLoaded', () => {
  const selectedUserIds = new Set();

  const dropdown = document.getElementById('UserDropdown');
  const selectedUsersDiv = document.getElementById('SelectedUsers');
  const hiddenInput = document.getElementById('SelectedUserIds');
  const form = document.querySelector('.project-form');

  // Enable horizontal scrolling in member wrapper
  const memberWrapper = document.querySelector('.member-wrapper');
  if (memberWrapper) {
    memberWrapper.addEventListener('wheel', (evt) => {
      evt.preventDefault();
      evt.currentTarget.scrollLeft += evt.deltaY;
    }, { passive: false });
  }

  // Show selected members when the page loads
  if (hiddenInput?.value) {
    const userIds = hiddenInput.value.split(',');
    userIds.forEach(userId => {
      const option = dropdown.querySelector(`option[value="${userId}"]`);
      if (!option) return;

      const name = option.getAttribute('data-name');
      const image = option.getAttribute('data-image') || '/images/profile-picture-placeholder.svg';

      const userHtml = `
        <div class="member-item" data-id="${userId}">
          <img src="${image}" class="profile-picture" alt="Profile Picture">
          <span>${name}</span>
          <button type="button" class="btn" onclick="removeSelectedUser('${userId}')">
            <i class="fa-regular fa-xmark"></i>
          </button>
        </div>
      `;

      selectedUsersDiv.insertAdjacentHTML('beforeend', userHtml);
      selectedUserIds.add(userId);
      option.style.display = 'none';
    });

    updateHiddenInput();
  }

  if (form) {
    form.addEventListener('submit', () => {
      updateHiddenInput();
    });
  }

  // Handles new selection of members
  window.handleUserSelection = function () {
    const selectedOption = dropdown.options[dropdown.selectedIndex];
    const userId = dropdown.value;

    if (!userId || selectedUserIds.has(userId)) return;

    const name = selectedOption.getAttribute('data-name');
    const image = selectedOption.getAttribute('data-image') || '/images/profile-picture-placeholder.svg';

    const userHtml = `
      <div class="member-item" data-id="${userId}">
        <img src="${image}" class="profile-picture" alt="Profile Picture">
        <span>${name}</span>
        <button type="button" class="btn" onclick="removeSelectedUser('${userId}')">
          <i class="fa-regular fa-xmark"></i>
        </button>
      </div>
    `;

    selectedUsersDiv.insertAdjacentHTML('beforeend', userHtml);
    selectedUserIds.add(userId);
    updateHiddenInput();

    selectedOption.style.display = 'none';

    dropdown.selectedIndex = 0;
    const placeholderOption = dropdown.querySelector('option[value=""]');
    if (placeholderOption) {
      placeholderOption.textContent = "";
    }
  };

  // Handles removal of members
  window.removeSelectedUser = function (userId) {
    document.querySelector(`#SelectedUsers .member-item[data-id="${userId}"]`)?.remove();
    selectedUserIds.delete(userId);
    updateHiddenInput();

    const optionToRestore = dropdown.querySelector(`option[value="${userId}"]`);
    if (optionToRestore) {
      optionToRestore.style.display = 'block';
    }

    if (selectedUserIds.size === 0) {
      const placeholderOption = dropdown.querySelector('option[value=""]');
      if (placeholderOption) {
        placeholderOption.textContent = "Select members";
      }
    }
  };

  function updateHiddenInput() {
    hiddenInput.value = Array.from(selectedUserIds).join(',');
  }
});

// Shows the form again after submitting goes wrong
document.addEventListener("DOMContentLoaded", () => {
  const body = document.body;
  const shouldShowAdd = body.dataset.showAdd === "true";
  const shouldShowEdit = body.dataset.showEdit === "true";

  if (shouldShowAdd) {
    document.getElementById('add-overlay')?.classList.remove('hidden');
  }

  if (shouldShowEdit) {
    document.getElementById('edit-overlay')?.classList.remove('hidden');
  }
});

// Error validation for the status id since it's and int instead of a string
document.addEventListener("DOMContentLoaded", function () {
  const showAddForm = document.getElementById('show-add-form')?.value === "true";
  const statusValue = document.getElementById('status-id-value')?.value;
  const error = document.getElementById('status-validation-error');

  if (showAddForm && statusValue === "0" && error) {
    error.classList.remove('hidden');
  }

  document.querySelector('.project-form')?.addEventListener('submit', function (e) {
    const status = document.getElementById('status-id');
    const error = document.getElementById('status-validation-error');
  
    if (status.value === "0") {
      error.classList.remove('hidden');
    } else {
      error.classList.add('hidden');
    }
  });
});