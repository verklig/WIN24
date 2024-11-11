function Navbar() {
  return (
    <header>
      <div className="container">
        <a href="index.html">
          <img className="lightmode" src="images/logotype.svg" alt="Silicon Logotype" />
          <img className="darkmode" src="images/logotype_darkmode.svg" alt="Silicon Logotype" />
        </a>
        <nav id="main-menu" className="navbar">
          <a href="#app-features" className="nav-link">Features</a>
          <a href="contact.html" className="nav-link">Contact</a>
        </nav>
        <div id="darkmode-toggle-switch" className="btn-toggle-switch">
          <span className="label">Dark Mode</span>
          <label htmlFor="darkmode-switch" className="toggle-switch">
            <input id="darkmode-switch" type="checkbox" />
            <span className="slider round"></span>
          </label>
        </div>
        <a href="#" id="auth-signin">
          <i className="fa-regular fa-user-large"></i>
          <span>Sign in / up</span>
        </a>
        <button className="btn-mobile">
          <div className="bar-1"></div>
          <div className="bar-2"></div>
          <div className="bar-3"></div>
        </button>
        <div id="hidden" className="side-menu">
          <a href="index.html" className="nav-link">Home</a>
          <a href="index.html#app-features" className="nav-link">Features</a>
          <a href="contact.html" className="nav-link">Contact</a>
        </div>
      </div>
    </header>
  )
}

export default Navbar;