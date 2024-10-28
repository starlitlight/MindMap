import axios from 'axios';

const api = axios.create({
  baseURL: process.env.REACT_APP_API_URL,
});

const createMindMap = async (data) => {
  try {
    const response = await api.post('/mindmaps', data);
    console.log('Mind map created:', response.data);
  } catch (error) {
    console.error('Error creating mind map:', error);
  }
};

const updateMindMap = async (id, data) => {
  try {
    const response = await api.put(`/mindmaps/${id}`, data);
    console.log('Mind map updated:', response.data);
  } catch (error) {
    console.error('Error updating mind map:', error);
  }
};

const deleteMindMap = async (id) => {
  try {
    await api.delete(`/mindmaps/${id}`);
    console.log('Mind map deleted');
  } catch (error) {
    console.error('Error deleting mind map:', error);
  }
};

const retrieveMindMaps = async () => {
  try {
    const response = await api.get('/mindmaps');
    console.log('Mind maps retrieved:', response.data);
    return response.data;
  } catch (error) {
    console.error('Error retrieving mind maps:', error);
    return [];
  }
};

const displayMindMaps = async () => {
  const mindMaps = await retrieveMindMaps();
  const container = document.getElementById('mindMapContainer');
  container.innerHTML = '';
  mindMaps.forEach(map => {
    const element = document.createElement('div');
    element.textContent = map.title;
    container.appendChild(element);
  });
};

document.addEventListener('DOMContentLoaded', () => {
  displayMindMaps();
  
  document.getElementById('createMindMapButton').addEventListener('click', async () => {
    const newMapData = {
      title: 'New Mind Map',
    };
    await createMindMap(newMapData);
    displayMindMaps();
  });
});