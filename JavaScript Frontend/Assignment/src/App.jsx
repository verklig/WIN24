import { BrowserRouter, Routes, Route } from "react-router-dom";
import { ConsultationFormProvider } from "./contexts/ConsultationFormContext";
import Header from "./components/Header";
import Footer from "./components/Footer";
import Home from "./views/Home";
import Contact from "./views/Contact";

function App() {
  return (
    <ConsultationFormProvider>
      <BrowserRouter>
        <div className="wrapper">
          <Header />
          <main>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/contact" element={<Contact />} />
            </Routes>
          </main>
          <Footer />
        </div>
      </BrowserRouter>
    </ConsultationFormProvider>
  );
};

export default App;