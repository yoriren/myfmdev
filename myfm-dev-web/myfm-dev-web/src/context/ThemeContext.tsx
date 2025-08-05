import React, { createContext, useContext, useEffect, useState } from 'react'

type ThemeContextType = {
    theme: 'light' | 'dark'
    toggleTheme: () => void
}

const ThemeContext = createContext<ThemeContextType>({
    theme: 'light',
    toggleTheme: () => { },
})

export const ThemeProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [theme, setTheme] = useState<'light' | 'dark'>('light')

    useEffect(() => {
        const savedTheme = localStorage.getItem('theme')
        const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches

        if (savedTheme) {
            setTheme(savedTheme as 'light' | 'dark')
        } else if (systemPrefersDark) {
            setTheme('dark')
        }
    }, [])

    useEffect(() => {
        document.documentElement.setAttribute('data-theme', theme)
        localStorage.setItem('theme', theme)
    }, [theme])

    const toggleTheme = () => {
        setTheme(prev => prev === 'light' ? 'dark' : 'light')
    }

    return (
        <ThemeContext.Provider value={{ theme, toggleTheme }}>
            {children}
        </ThemeContext.Provider>
    )
}

export const useTheme = () => useContext(ThemeContext)