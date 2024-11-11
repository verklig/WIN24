import { useState } from 'react'
import Navbar from './components/Navbar'
import Hero from './components/Hero'
import Brands from './components/Brands'
import Features from './components/Features'
import HowItWorks from './components/HowItWorks'
import LearnMore from './components/LearnMore'
import Feedback from './components/Feedback'
import Faq from './components/Faq'
import Newsletter from './components/Newsletter'
import Footer from './components/Footer'

function App() {
  const [count, setCount] = useState(0)

  return (
    <div class="wrapper">
      <Navbar />
      <main>
        <Hero />
        <Brands />
        <Features />
        <HowItWorks />
        <LearnMore />
        <Feedback />
        <Faq />
        <Newsletter />
      </main>
      <Footer />
    </div>
  )
}

export default App
