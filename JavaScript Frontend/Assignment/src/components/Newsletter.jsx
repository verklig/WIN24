import { useState } from "react";

function Newsletter() {
  const [email, setEmail] = useState("");

  const handleInputChange = (e) => {
    setEmail(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!/^[A-Za-z0-9._-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/.test(email)) {
      alert("The email address must be valid.");
      return;
    }

    const response = await fetch(
      "https://win24-assignment.azurewebsites.net/api/forms/subscribe",
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email })
      }
    );

    if (!response.ok) {
      alert("Something went wrong.");
      return;
    }

    alert("You have successfully subscribed to our newsletter!");
    setEmail("");
  };

  return (
    <section id="newsletter">
      <div className="container">
        <div className="newsletter-box">
          <div className="part-1 flex">
            <img src="images/notification-bell.svg" alt="Bell Icon" />
            <div>
              <h2 className="title">
                Subscribe to our newsletter
                <span>to stay informed about latest updates</span>
              </h2>
            </div>
          </div>
          <form className="part-2 flex" onSubmit={handleSubmit} noValidate>
            <div>
              <i className="fa-regular fa-envelope"></i>
              <input type="email" id="email" name="email" placeholder="Your email" autoComplete="on" value={email} onChange={handleInputChange} />
            </div>
            <button className="btn-subscribe flex" type="submit">
              <span>Subscribe</span>
            </button>
          </form>
        </div>
      </div>
    </section>
  );
};

export default Newsletter;
