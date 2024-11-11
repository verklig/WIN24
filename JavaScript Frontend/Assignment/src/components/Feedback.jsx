function Feedback() {
  return (
    <section id="feedback">
      <div className="container grid">
        <h2 className="title">Clients are<br />Loving Our App</h2>
        <div id="inner-grid" className="grid">
          <div>
            <div className="quote flex">
              <i className="fa-sharp fa-solid fa-quote-left"></i>
            </div>
            <div className="feedback-box">
              <div className="rating">
                <i className="fa-solid fa-star"></i>
                <i className="fa-solid fa-star"></i>
                <i className="fa-solid fa-star"></i>
                <i className="fa-solid fa-star"></i>
                <i className="fa-regular fa-star"></i>
              </div>
              <p className="description">
                Sit pretium aliquam tempor, orci dolor sed maecenas rutrum sagittis.
                Laoreet posuere rhoncus, egestas lacus, egestas justo aliquam vel.
                Nisi vitae lectus hac hendrerit. Montes justo turpis sit amet.
              </p>
              <div className="grid">
                <div>
                  <img src="images/fannie_summers.svg" alt="Avatar" />
                </div>
                <div>
                  <h3>Fannie Summers</h3>
                  <span>Designer</span>
                </div>
              </div>
            </div>
          </div>
          <div>
            <div className="quote flex">
              <i className="fa-sharp fa-solid fa-quote-left"></i>
            </div>
            <div className="feedback-box">
              <div className="rating">
                <i className="fa-solid fa-star"></i>
                <i className="fa-solid fa-star"></i>
                <i className="fa-solid fa-star"></i>
                <i className="fa-solid fa-star"></i>
                <i className="fa-solid fa-star"></i>
              </div>
              <p className="description">
                Nunc senectus leo vel venenatis accumsan vestibulum sollicitudin amet porttitor.
                Nisl bibendum nulla tincidunt eu enim ornare dictumst sit amet.
                Dictum pretium dolor tincidunt egestas eget nunc.
              </p>
              <div className="grid">
                <div>
                  <img src="images/albert_flores.svg" alt="Avatar" />
                </div>
                <div>
                  <h3>Albert Flores</h3>
                  <span>Developer</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}

export default Feedback;