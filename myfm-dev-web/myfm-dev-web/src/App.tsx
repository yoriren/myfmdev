import React from "react";
import { Container } from "react-bootstrap";
import { ProductList } from "./components/ProductList";
import ThemeToggle from "./components/ThemeToggle";

function App() {
    return (
        <div className="app-container">
            <ThemeToggle />
            <Container className="mt-4">
                <header className="app-header">
                    <h1 className="app-title">Cloud Services Marketplace</h1>
                    <p className="app-subtitle">Compare and select the best cloud solutions for your needs</p>
                </header>
                <main>
                    <ProductList />
                </main>
            </Container>
        </div>
    );
}

export default App;