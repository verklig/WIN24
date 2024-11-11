function Hero() {
  return (
    <section id="hero">
      <div className="container grid">
        <div className="title">
          <h1>Manage All Your<br />Money in One App</h1>
        </div>
        <div className="content">
          <div className="description">
            <p>We offer you a new generation of the mobile banking.
              <span className="br">Save, spend & manage money in your pocket.</span>
            </p>
          </div>
          <div id="downloads" className="flex">
            <a href="#" className="btn-download flex">
              <img className="lightmode" src="images/appstore-light.svg" alt="App Store Download" />
              <img className="darkmode" src="images/appstore-dark.svg" alt="App Store Download" />
            </a>
            <a href="#" className="btn-download flex">
              <img className="lightmode" src="images/googleplay-light.svg" alt="Google Play Download" />
              <img className="darkmode" src="images/googleplay-dark.svg" alt="Google Play Download" />
            </a>
          </div>
          <div id="discover-more" className="container flex">
            <a href="#" className="btn-discover-more flex">
              <i className="fas fa-angle-down"></i>
            </a>
            <span>Discover more</span>
          </div>
        </div>
        <div className="images">
          <img className="img-back" src="images/mybudget_cut.svg" alt="My Budget Showcase" />
          <img className="img-front" src="images/yourcards_cut.svg" alt="Your Cards Showcase" />
        </div>
      </div>
    </section>
  )
}

export default Hero;