import React, { createContext, useState } from "react";

export const ConsultationFormContext = createContext();

export const ConsultationFormProvider = ({ children }) => {
  const options = [{ id: 1, text: "Financial Consulting" }, { id: 2, text: "Investment Planning" }, { id: 3, text: "Tax Optimization" }, { id: 4, text: "Wealth Management" }];
  const [formData, setFormData] = useState({ fullName: "", email: "", specialist: options[0].text });
  const [errors, setErrors] = useState({});

  const initialFormState = {
    fullName: "",
    email: "",
    specialist: options[0].text
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;

    setFormData({ ...formData, [name]: value });
    validateField(name, value);
  };

  const validateField = (name, value) => {
    let error = "";

    if (name === "fullName" && !/^[A-Öa-ö\s\-]{2,}$/.test(value)) {
      error = "The name must be at least 2 characters long, no numbers.";
    }
    else if (name === "email" && !/^[A-Za-z0-9._-]+@[A-Za-z0-9.-]+\.[A-Za-z0-9]{2,}$/.test(value)) {
      error = "The email address must be valid.";
    }

    setErrors(prevErrors => ({...prevErrors, [name]: error}));
  };

  const validateForm = () => {
    const newErrors = {};

    if (!/^[A-Öa-ö\s\-]{2,}$/.test(formData.fullName)) {
      newErrors.fullName = "The name must be at least 2 characters long, no numbers.";
    }

    if (!/^[A-Za-z0-9._-]+@[A-Za-z0-9.-]+\.[A-Za-z0-9]{2,}$/.test(formData.email)) {
      newErrors.email = "The email address must be valid.";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (validateForm()) {
      const response = await fetch(
        "https://win24-assignment.azurewebsites.net/api/forms/contact",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(formData)
        }
      );

      if (!response.ok) {
        alert("Something went wrong.");
        return;
      }

      alert("Form submitted successfully, thank you for contacting us!");
      setFormData(initialFormState);
      setErrors({});
    }
  };

  return (
    <ConsultationFormContext.Provider value={{ formData, errors, options, handleInputChange, handleSubmit }}>
      {children}
    </ConsultationFormContext.Provider>
  );
};