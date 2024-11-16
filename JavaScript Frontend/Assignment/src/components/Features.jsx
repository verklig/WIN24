function Features() {
  return (
    <section id="features">
      <div className="container grid">
        <div className="images">
          <img src="images/yourcards_popout.svg" alt="Your Cards Showcase Popout" />
        </div>
        <div className="content">
          <div className="title flex">
            <h2>App Features</h2>
          </div>
          <div className="description flex">
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit.
              Proin volutpat mollis egestas. Nam luctus facilisis ultrices.
              Pellentesque volutpat ligula est. Mattis fermentum, at nec lacus.
            </p>
          </div>
          <div id="inner-grid" className="grid">
            <div className="features-box grid">
              <div className="icon-box flex">
                <img src="images/credit-card.svg" alt="Credit Card Icon" />
              </div>
              <div>
                <h3>Easy Payments</h3>
                <p>Id mollis consectetur congue egestas egestas suspendisse blandit justo.</p>
              </div>
            </div>
            <div className="features-box grid">
              <div className="icon-box flex">
                <img src="images/shield.svg" alt="Shield Icon" />
              </div>
              <div>
                <h3>Data Security</h3>
                <p>Augue pulvinar justo, fermentum fames aliquam accumsan vestibulum non.</p>
              </div>
            </div>
            <div className="features-box grid">
              <div className="icon-box flex">
                <img src="images/bars-graphic.svg" alt="Diagram Icon" />
              </div>
              <div>
                <h3>Cost Statistics</h3>
                <p>Mattis urna ultricies non amet, purus in auctor non. Odio vulputate ac nibh.</p>
              </div>
            </div>
            <div className="features-box grid">
              <div className="icon-box flex">
                <img src="images/communication.svg" alt="Communication Icon" />
              </div>
              <div>
                <h3>Support 24/7</h3>
                <p>A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris.</p>
              </div>
            </div>
            <div className="features-box grid">
              <div className="icon-box flex">
                <img src="images/wallet.svg" alt="Wallet Icon" />
              </div>
              <div>
                <h3>Regular Cashback</h3>
                <p>Sit facilisis dolor arcu, fermentum vestibulum arcu elementum imperdiet eleifend.</p>
              </div>
            </div>
            <div className="features-box grid">
              <div className="icon-box flex">
                <img src="images/happy.svg" alt="Happy Icon" />
              </div>
              <div>
                <h3>Top Standards</h3>
                <p>Faucibus cursus maecenas lorem cursus nibh. Sociis sit risus id. Sit facilisis dolor arcu.</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Features;