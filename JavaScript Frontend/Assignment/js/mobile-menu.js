const btnMenu = document.querySelector(".btn-mobile");
const sideMenu = document.querySelector(".side-menu");

btnMenu.addEventListener("click", e => {
  // if(sideMenu.classList.contains("open")) {
  //   sideMenu.classList.remove("open");

  //   return;
  // };

  btnMenu.classList.toggle("open");
  sideMenu.classList.toggle("open");
});

window.addEventListener("resize", e => {
  if (window.innerWidth >= 1400) {
    closeMenu();
  }
});

function closeMenu() {
  btnMenu.classList.remove("open");
  sideMenu.classList.remove("open");
}