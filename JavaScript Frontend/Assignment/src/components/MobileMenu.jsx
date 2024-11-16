import { useState, useEffect } from 'react';
import { NavLink } from "react-router-dom";

function MobileMenu() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [isHidden, setIsHidden] = useState(true);
  const [isButtonDisabled, setIsButtonDisabled] = useState(false);

  function toggleMenu() {
    setIsButtonDisabled(true);

    if (isMenuOpen) {
      closeMenuWithDelay();
    } else {
      openMenuWithDelay();
    }
  };

  function openMenuWithDelay() {
    setIsHidden(false);
    setTimeout(() => {
      setIsMenuOpen(true);
      setIsButtonDisabled(false);
    }, 1);
  };

  function closeMenuWithDelay() {
    setIsMenuOpen(false);
    setTimeout(() => { 
      setIsHidden(true);
      setIsButtonDisabled(false);
    }, 300);
  };

  useEffect(() => {
    function handleResize() {
      if (window.innerWidth >= 1400) {
        setIsMenuOpen(false);
        setIsHidden(true);
      }
    };

    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  return (
    <>
      <button className={`btn-mobile ${isMenuOpen ? "open" : ""}`} onClick={toggleMenu} disabled={isButtonDisabled}>
        <div className="bar-1"></div>
        <div className="bar-2"></div>
        <div className="bar-3"></div>
      </button>
      <div className={`side-menu ${isMenuOpen ? "open" : ""} ${isHidden ? "hidden" : ""}`}>
        <NavLink to="/" className="nav-link" onClick={closeMenuWithDelay}>Home</NavLink>
        <NavLink to="#app-features" className="nav-link" onClick={closeMenuWithDelay}>Features</NavLink>
        <NavLink to="/contact" className="nav-link" onClick={closeMenuWithDelay}>Contact</NavLink>
      </div>
    </>
  );
};

export default MobileMenu;