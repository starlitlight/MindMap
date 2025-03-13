import React, { useState, useEffect } from 'react';
import { 
  getMindMaps, 
  selectMindMap 
} from './MindMapService';

const MindMapList = () => {
  const [mindMaps, setMindMaps] = useState([]);
  const [selectedMap, setSelectedMap] = useState(null);

  useEffect(() => {
    loadMindMaps();
  }, []);

  const loadMindMaps = async () => {
    try {
      const maps = await getMindMaps();
      setMindMaps(maps);
    } catch (error) {
      console.error('Failed to load mind maps', error);
    }
  };

  const handleSelectMap = (mapId) => {
    const map = mindMaps.find((m) => m.id === mapId);
    setSelectedMap(map);
    selectMindMap(map);
  };

  return (
    <div>
      <h2>Mind Maps List</h2>
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