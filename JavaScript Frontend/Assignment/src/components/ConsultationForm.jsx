import React, { useContext } from "react";
import { ConsultationFormContext } from "../contexts/ConsultationFormContext";

function ConsultationForm() {
  const { formData, errors, options, handleInputChange, handleSubmit } = useContext(ConsultationFormContext);

  return (
    <div className="form" onSubmit={handleSubmit}>
      <form noValidate>
        <h2 className="title">Get Online Consultation</h2>
        <div className="form-group">
          <h3>Full name</h3>
          <input type="text" name="fullName" value={formData.fullName} onChange={handleInputChange} autoComplete="on" required />
          <span className="validation-error">{errors.fullName || "\u00A0"}</span>
        </div>
        <div className="form-group">
          <h3>Email Address</h3>
          <input type="email" name="email" value={formData.email} onChange={handleInputChange} autoComplete="on" required />
          <span className="validation-error">{errors.email || "\u00A0"}</span>
        </div>
        <div className="form-group">
          <h3>Specialist</h3>
          <div>
            <select type="text" name="specialist" id="specialist" value={formData.specialist} onChange={handleInputChange} required>
              {options.map(option => (<option key={option.id} value={option.text}>{option.text}</option>))}
            </select>
            <i className="fas fa-angle-down"></i>
          </div>
        </div>
        <button type="submit">
          <span>Make an appointment</span>
        </button>
      </form>
    </div>
  );
};

export default ConsultationForm;