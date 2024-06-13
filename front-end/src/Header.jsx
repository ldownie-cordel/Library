import React from 'react';

export default function Header() {
    return (
        <header style={headerStyle}>
            <img src="logo.png" alt="Logo" style={logoStyle} />
            <h1 style={titleStyle}>Cordel Library</h1>
        </header>
    );
}