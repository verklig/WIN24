import Brands from "../components/Brands";
import Features from "../components/Features";
import HowItWorks from "../components/HowItWorks";
import LearnMore from "../components/LearnMore";
import Feedback from "../components/Feedback";
import Faq from "../components/Faq";
import Newsletter from "../components/Newsletter";
import Hero from "../components/Hero";

function Home() {
  return (
    <>
      <Hero />
      <Brands />
      <Features />
      <HowItWorks />
      <LearnMore />
      <Feedback />
      <Faq />
      <Newsletter />
    </>
  );
};

export default Home;