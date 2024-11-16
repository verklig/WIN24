import Accordion from "./Accordion";

function Faq() {
  return (
    <section id="faq">
      <div className="container">
        <div className="text-box">
          <div className="title">
            <h2>Any questions? Check out the FAQs</h2>
          </div>
          <div className="description">
            <p>Still have unanswered questions and need to get in touch?</p>
          </div>
        </div>
        <Accordion />
        <div className="flex">
          <a href="#" className="btn-contact flex">
            <span>Contact us now</span>
          </a>
        </div>
        <div className="contact flex">
          <div className="contact-box">
            <div className="contact-items">
              <div className="contact-item">
                <i className="fa-solid fa-phone-volume icon"></i>
              </div>
              <div className="contact-item">
                <p>Still have questions?</p>
              </div>
              <div className="contact-item">
                <a href="#">
                  <span>Contact us</span>
                  <i className="fa-sharp fa-solid fa-arrow-right"></i>
                </a>
              </div>
            </div>
          </div>
          <div className="contact-box">
            <div className="contact-items">
              <div className="contact-item">
                <i className="fa-solid fa-comment-dots icon"></i>
              </div>
              <div className="contact-item">
                <p>Don't like phone calls?</p>
              </div>
              <div id="btn-contact-2" className="contact-item">
                <a href="#">
                  <span>Contact us</span>
                  <i className="fa-sharp fa-solid fa-arrow-right"></i>
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Faq;