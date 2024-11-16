function Testimonial({item}) {
  const maxStars = 5;
  const filledStars = Math.min(item.starRating, maxStars);
  const emptyStars = maxStars - filledStars;

  return (
    <div>
      <div className="quote flex">
        <i className="fa-sharp fa-solid fa-quote-left"></i>
      </div>
      <div className="feedback-box">
        <div className="rating">

          {
            [...Array(filledStars)].map((_, id) => (
              <i key={id} className="fa-solid fa-star"></i>
            ))
          }

          {
            [...Array(emptyStars)].map((_, id) => (
              <i key={id} className="fa-regular fa-star"></i>
            ))
          }

        </div>
        <p className="description">{item.comment}</p>
        <div className="grid">
          <div>
            <img src={item.avatarUrl} alt="Avatar" />
          </div>
          <div>
            <h3>{item.author}</h3>
            <span>{item.jobRole}</span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Testimonial;