import React, { useState, useEffect } from 'react';
import { 
  getMindMaps, 
  selectMindMap 
} from './MindMapService';

const MindMapList = () => {
  const [mindMaps, setMindMaps] = useState([]);
  const [selectedMap, setSelectedMap] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    loadMindMaps();
  }, []);

  const loadMindMaps = async () => {
    try {
      const maps = await getMindMaps();
      setMindMaps(maps);
      setError(null);
    } catch (error) {
      console.error('Failed to load mind maps', error);
      handleErrors(error);
    }
  };

  const handleSelectMap = (mapId) => {
    const map = mindMaps.find((m) => m.id === mapId);
    setSelectedMap(map);
    selectMindMap(map);
  };

  const handleErrors = (error) => {
    if (error.response) {
      setError(`Server responded with a status code of ${error.response.status}`);
    } else if (error.request) {
      setError(`Network error: The request was made but no response was received`);
    } else {
      setError(`An error occurred: ${error.message}`);
    }
  };

  return (
    <div>
      <h2>Mind Maps List</h2>
      {error && <p className="error-message">{error}</p>}
      {mindMaps.length > 0 ? (
        <ul>
          {mindMaps.map((map) => (
            <li key={map.id} onClick={() => handleSelectMap(map.id)}>
              {map.name}
            </li>
          ))}
        </ul>
      ) : (
        <p>No mind maps available</p>
      )}
      {selectedMap && (
        <div>
          <h3>Selected Map</h3>
          <p>{selectedMap.name}</p>
        </div>
      )}
    </div>
  );
};

export default MindMapList;