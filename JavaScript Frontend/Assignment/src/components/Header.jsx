import { NavLink, Link } from "react-router-dom";
import DarkmodeBtn from "./DarkmodeBtn";
import MobileMenu from "./MobileMenu";

function Navbar() {
  return (
    <header>
      <div className="container">
        <Link to="/">
          <img className="lightmode" src="images/logotype.svg" alt="Silicon Logotype" />
          <img className="darkmode" src="images/logotype_darkmode.svg" alt="Silicon Logotype" />
        </Link>
        <nav id="main-menu" className="navbar">
          <NavLink to="#app-features" className="nav-link">Features</NavLink>
          <NavLink to="/contact" className="nav-link">Contact</NavLink>
        </nav>
        <DarkmodeBtn />
        <a href="#" id="auth-signin">
          <i className="fa-regular fa-user-large"></i>
          <span>Sign in / up</span>
        </a>
        <MobileMenu />
      </div>
    </header>
  );
};

export default Navbar;