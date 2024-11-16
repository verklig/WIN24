function LearnMore() {
  return (
    <section id="learn-more">
      <div className="container grid">
        <div>
          <h2 className="title">Make your money<br />transfer simple and clear</h2>
          <ul className="list">
            <li className="list-item flex"><img className="checkmark" src="images/checkmark.svg" alt="Checkmark Icon" />
              <span>Banking transactions are free for you</span>
            </li>
            <li className="list-item flex"><img className="checkmark" src="images/checkmark.svg" alt="Checkmark Icon" />
              <span>No monthly cash commission</span>
            </li>
            <li className="list-item flex"><img className="checkmark" src="images/checkmark.svg" alt="Checkmark Icon" />
              <span>Manage payments and transactions online</span>
            </li>
          </ul>
          <a href="#" className="btn-learn-more flex">
            <span>Learn more</span>
            <i className="fa-sharp fa-solid fa-arrow-right"></i>
          </a>
        </div>
        <div>
          <img src="images/transfer_box.svg" alt="Diagram/Send Showcase" />
        </div>
        <div>
          <img src="images/card_box.svg" alt="Contacts/Card Showcase" />
        </div>
        <div>
          <h2 className="title">Receive payment from<br />international bank details</h2>
          <div className="grid">
            <div>
              <div className="icon-box flex">
                <img src="images/credit-card.svg" alt="Credit Card Icon" />
              </div>
              <p className="description">Manage your payments online. Mollis congue egestas egestas fermentum fames.</p>
            </div>
            <div>
              <div className="icon-box flex">
                <img src="images/wallet.svg" alt="Wallet Icon" />
              </div>
              <p className="description">A elementur and imperdiet enim, pretium etiam facilisi aenean quam mauris.</p>
            </div>
            <a href="#" className="btn-learn-more flex">
              <span>Learn more</span>
              <i className="fa-sharp fa-solid fa-arrow-right"></i>
            </a>
          </div>
        </div>
      </div>
    </section>
  );
};

export default LearnMore;