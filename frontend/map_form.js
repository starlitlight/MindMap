import React, { useState, useEffect } from 'react';

const API_URL = process.env.REACT_APP_API_URL;

const MindMapForm = ({ onSubmitSuccess, existingMap }) => {
  const getInitialFormData = () => ({
    title: existingMap ? existingMap.title : '',
    nodes: existingMap ? existingMap.nodes : '',
  });

  const [formData, setFormData] = useState(getInitialFormData);

  const [formErrors, setFormErrors] = useState({});

  useEffect(() => {
    setFormData(getInitialFormData);
  }, [existingMap]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const validateForm = () => {
    let errors = {};
    if (!formData.title) errors.title = 'Title is required';
    if (!formData.nodes) errors.nodes = 'Nodes are required';
    setFormErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const resetForm = () => {
    setFormData(getInitialFormData());
    setFormErrors({});
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validateForm()) return;
    try {
      const response = await fetch(`${API_URL}/mindMaps`, {
        method: existingMap ? 'PUT' : 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });
      if (!response.ok) throw new Error('Network response was not ok');
      onSubmitSuccess();
      resetForm(); 
    } catch (error) {
      console.error('Failed to submit mind map:', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="title">Title:</label>
        <input
          type="text"
          name="title"
          id="title"
          value={formData.title}
          onChange={handleChange}
          className={formErrors.title ? 'error' : ''}
        />
        {formErrors.title && <div className="error">{formErrors.title}</div>}
      </div>
      <div>
        <label htmlFor="nodes">Nodes:</label>
        <textarea
          name="nodes"
          id="nodes"
          value={formData.nodes}
          onChange={handleChange}
          className={formErrors.nodes ? 'error' : ''}
        />
        {formErrors.nodes && <div className="error">{formErrors.nodes}</div>}
      </div>
      <button type="submit">Submit</button>
      <button type="button" onClick={resetForm}>Reset</button>
    </form>
  );
};

export default MindMapForm;