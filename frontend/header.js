import React from 'react';
import { NavLink } from 'react-router-dom';

const Header = () => {
  // Ensures the external link is valid; otherwise, it provides a fallback  
  const externalLink = process.env.REACT_APP_EXTERNAL_LINK || "#";

  // Optional: Warn about missing external link in the development environment
  if (!process.env.REACT_APP_EXTERNAL_LINK && process.env.NODE_ENV === "development") {
    console.warn("REACT_APP_EXTERNAL_LINK is not defined");
  }

  return (
    <header>
      <nav>
        <ul>
          <li><NavLink to="/" exact activeClassName="active">Home</NavLink></li>
          <li><NavLink to="/about" activeClassName="active">About</NavLink></li>
          <li><NavLink to="/contact" activeClassName="active">Contact</NavLink></li>
          <li><a href={externalLink} target="_blank" rel="noopener noreferrer">External Link</a></li>
        </ul>
      </nav>
    </header>
  );
}

export default Header;