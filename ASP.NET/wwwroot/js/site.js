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