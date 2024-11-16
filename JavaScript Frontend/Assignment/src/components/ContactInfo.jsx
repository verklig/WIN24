function ContactInfo() {
  return (
    <section id="contact-info">
      <div className="container grid">
        <div className="map">
          <img src="images/map.svg" alt="Map Over Locations" />
        </div>
        <div className="directions">
          <div className="directions-box">
            <h2 className="title">Medical Center 1</h2>
            <div className="direction-item">
              <i className="fa-regular fa-location-dot icon"></i>
              <span>4517 Washington Ave. Manchester, Kentucky 39495</span>
            </div>
            <div className="direction-item">
              <i className="fa-regular fa-phone-volume icon"></i>
              <span>(406) 555-0120</span>
            </div>
            <div className="direction-item">
              <i className="fa-regular fa-clock icon"></i>
              <span><b>Mon - Fri:</b> 9:00 am - 22:00 am</span><br />
              <span id="weekend"><b>Sat - Sun:</b> 9:00 am - 20:00 am</span>
            </div>
          </div>
          <div className="directions-box">
            <h2 className="title">Medical Center 2</h2>
            <div className="direction-item">
              <i className="fa-regular fa-location-dot icon"></i>
              <span>2464 Royal Ln. Mesa, New Jersey 45463</span>
            </div>
            <div className="direction-item">
              <i className="fa-regular fa-phone-volume icon"></i>
              <span>(406) 544-0123</span>
            </div>
            <div className="direction-item">
              <i className="fa-regular fa-clock icon"></i>
              <span><b>Mon - Fri:</b> 9:00 am - 22:00 am</span><br />
              <span id="weekend"><b>Sat - Sun:</b> 9:00 am - 20:00 am</span>
            </div>
          </div>
          <div className="socials flex">
            <button className="icon-box">
              <i className="fa-brands fa-square-facebook"></i>
            </button>
            <button className="icon-box">
              <i className="fa-brands fa-twitter"></i>
            </button>
            <button className="icon-box">
              <i className="fa-brands fa-instagram"></i>
            </button>
            <button className="icon-box">
              <i className="fa-brands fa-youtube"></i>
            </button>
          </div>
        </div>
      </div>
    </section>
  );
};

export default ContactInfo;