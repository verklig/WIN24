const consent = document.cookie.includes("cookie_consent=accepted");
const overlay = document.querySelector(".overlay");

if (!consent && overlay) {
  overlay.classList.remove("hidden");
} else if (overlay) {
  overlay.classList.add("hidden");
}

setTimeout(updateScrollLock, 0);

function acceptCookies() {
  document.cookie = "cookie_consent=accepted; path=/; max-age=31536000; Secure; SameSite=Strict";
  location.reload();
  setTimeout(updateScrollLock, 0);
}