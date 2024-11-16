import { NavLink } from "react-router-dom";
import ConsultationForm from "./ConsultationForm";

function ContactHero() {
  return (
    <section id="contact-hero">
      <div className="container">
        <div className="nav-extra flex">
          <i className="fa-solid fa-house"></i>
          <NavLink to="/" className="nav-link">Homepage</NavLink>
          <i className="fa-regular fa-angles-right"></i>
          <NavLink to="/contact" className="nav-link">Contact</NavLink>
        </div>
        <div className="title flex">
          <h1>Contact Us</h1>
        </div>
        <div className="contact-box grid">
          <div className="icon-box flex">
            <i className="fa-regular fa-envelope icon"></i>
          </div>
          <div>
            <h2>Email Us</h2>
            <p>Please feel free to drop us a line. We will respond as soon as possible.</p>
            <a href="#">
              <span>Leave a message</span>
              <i className="fa-sharp fa-solid fa-arrow-right"></i>
            </a>
          </div>
        </div>
        <div className="contact-box grid">
          <div className="icon-box flex">
            <i className="fa-regular fa-users-medical icon"></i>
          </div>
          <div>
            <h2>Careers</h2>
            <p>Sit ac ipsum leo lorem magna nunc mattis maecenas non vestibulum.</p>
            <a href="#">
              <span>Send an application</span>
              <i className="fa-sharp fa-solid fa-arrow-right"></i>
            </a>
          </div>
        </div>
        <ConsultationForm />
      </div>
    </section>
  );
};

export default ContactHero;