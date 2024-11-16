import { useEffect, useState } from "react";
import AccordionItem from "./AccordionItem";

function Accordion() {
  const [faqs, setFaqs] = useState([]);
  const [openId, setOpenId] = useState(false);

  const getFaq = async () => {
    const res = await fetch("https://win24-assignment.azurewebsites.net/api/faq");
    const data = await res.json();
    setFaqs(data);
  }

  const toggleFaqItem = (id) => {
    setOpenId(openId === id ? false : id);
  }

  useEffect(() => {
    getFaq();
  }, []);

  return (
    <div className="grid">
      <div className="list-box">
        <ul className="list">
          {
            faqs.map((item, id) => (
              <AccordionItem key={item.id} item={item} isOpen={openId === id} onToggle={() => toggleFaqItem(id)}/>
            ))
          }
        </ul>
      </div>
    </div>
  );
};

export default Accordion;