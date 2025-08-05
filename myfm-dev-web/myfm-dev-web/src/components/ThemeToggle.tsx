﻿import React from 'react'
import { useTheme } from '../context/ThemeContext'

const ThemeToggle: React.FC = () => {
    const { theme, toggleTheme } = useTheme()

    return (
        <button
            className="theme-toggle"
            onClick={toggleTheme}
            aria-label={`Switch to ${theme === 'light' ? 'dark' : 'light'} mode`}
        >
            {theme === 'light' ? (
                <span>🌙</span>
            ) : (
                <span>☀️</span>
            )}
        </button>
    )
}

export default ThemeToggle