function HowItWorks() {
  return (
    <section id="how-it-works">
      <div className="container">
        <div className="title flex">
          <h2>How Does It Work?</h2>
        </div>
        <div className="grid">
          <img className="desktop-image images" src="images/mybudget_faded.svg" alt="My Budget Showcase" />
          <img className="desktop-image images" src="images/yourcards_full.svg" alt="Your Cards Showcase" />
          <img className="desktop-image images" src="images/transfer_faded.svg" alt="Transfer Showcase" />
          <img className="tablet-image images" src="images/yourcards_faded.svg" alt="Your Cards Showcase" />
          <img className="mobile-image images" src="images/transfer_full.svg" alt="Transfer Showcase" />
          <img className="tablet-image images" src="images/transfer_faded-2.svg" alt="Transfer Showcase" />
        </div>
        <div className="flex">
          <h3 className="desktop-text">Latest transaction history</h3>
          <h3 className="mobile-text">Transfers to people from your contact list</h3>
        </div>
        <div className="description flex">
          <p className="desktop-text">
            Enim, et amet praesent pharetra. Mi non ante hendrerit amet sed.
            Arcu sociis tristique quisque hac in consectetur condimentum.
          </p>
          <p className="mobile-text">
            Proin volutpat mollis egestas. Nam luctus facilisis ultrices.
            Pellentesque volutpat ligula est. Mattis fermentum, at nec lacus.
          </p>
        </div>
      </div>
    </section>
  )
}

export default HowItWorks;