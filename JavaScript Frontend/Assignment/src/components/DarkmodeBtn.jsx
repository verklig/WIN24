import React, { useEffect, useState } from "react";

function DarkmodeBtn() {
  const [isDarkmode, setIsDarkmode] = useState(false);

  function toggleDarkmode() {
    const newMode = !isDarkmode;
    setIsDarkmode(newMode);

    if (newMode) {
      document.documentElement.classList.add("dark");
      localStorage.setItem("darkmode", "on");
    }
    else {
      document.documentElement.classList.remove("dark");
      localStorage.setItem("darkmode", "off");
    }
  };

  useEffect(() => {
    const savedMode = localStorage.getItem("darkmode");

    if (savedMode === "on") {
      document.documentElement.classList.add("dark")
      setIsDarkmode(true);
    }
    else {
      document.documentElement.classList.remove("dark")
      setIsDarkmode(false);
    }
  }, []);
  
  return (
    <>
      <div className="btn-toggle-switch">
        <span className="label">Dark Mode</span>
        <label htmlFor="darkmode-switch" className="toggle-switch">
          <input id="darkmode-switch" type="checkbox" checked={ isDarkmode } onChange={toggleDarkmode}/>
          <span className="slider round"></span>
        </label>
      </div>
    </>
  );
};

export default DarkmodeBtn;