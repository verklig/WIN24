import { useEffect, useState } from "react";
import Testimonial from "./Testimonial";

function Feedback() {
  const [testimonials, setTestimonials] = useState([]);

  const getTestimonials = async () => {
    const res = await fetch("https://win24-assignment.azurewebsites.net/api/testimonials");
    const data = await res.json();
    setTestimonials(data);
  }

  useEffect(() => {
    getTestimonials();
  }, []);

  return (
    <section id="feedback">
      <div className="container grid">
        <h2 className="title">Clients are<br />Loving Our App</h2>
        <div id="inner-grid" className="grid">
          {
            testimonials.map((testimonial) => (
              <Testimonial key={testimonial.id} item={testimonial} />
            ))
          }
        </div>
      </div>
    </section>
  );
};

export default Feedback;