function Newsletter() {
  return (
    <section id="newsletter">
      <div className="container">
        <div className="newsletter-box">
          <div className="part-1 flex">
            <img src="images/notification-bell.svg" alt="Bell Icon" />
            <div>
              <h2 className="title">Subscribe to our newsletter
                <span>to stay informed about latest updates</span>
              </h2>
            </div>
          </div>
          <div className="part-2 flex">
            <div>
              <i className="fa-regular fa-envelope"></i>
              <input type="email" id="email" name="email" placeholder="Your email" autocomplete="on" />
            </div>
            <a href="#" className="btn-subscribe flex">
              <span>Subscribe</span>
            </a>
          </div>
        </div>
      </div>
    </section>
  )
}

export default Newsletter;