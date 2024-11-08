const btnMenu = document.querySelector(".btn-mobile");
const sideMenu = document.querySelector(".side-menu");

btnMenu.addEventListener("click", e => {
  if (sideMenu.classList.contains("open")) {
    closeMenu();
    setTimeout(() => {
      hideMenu();
    }, 300);
  }
  else {
    sideMenu.id = "";

    setTimeout(() => {
      openMenu();
    }, 1);
  }
});

window.addEventListener("resize", e => {
  if (window.innerWidth >= 1400) {
    closeMenu();
    hideMenu();
  }
});

function openMenu() {
  sideMenu.classList.add("open");
  btnMenu.classList.add("open");
}

function closeMenu() {
  sideMenu.classList.remove("open");
  btnMenu.classList.remove("open");
}

function hideMenu() {
  sideMenu.id = "hidden";
}