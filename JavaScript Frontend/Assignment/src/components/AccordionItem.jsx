import React from "react";

function AccordionItem({item, isOpen , onToggle}) {
  return (
    <li className={`list-item ${isOpen ? "open" : ""}`} onClick={onToggle}>
      <div className="question">
        <h3 className="grid">
          {item.title}
          <button className="btn-faq flex">
            <i className="fas fa-angle-down"></i>
          </button>
        </h3>
      </div>
      {
        <div className={`answer ${isOpen ? "open" : ""}`}>
          <p className="expandable">{item.content}</p>
        </div>
      }
    </li>
  );
};

export default AccordionItem;