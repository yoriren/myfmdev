import React, { useEffect, useState } from "react";
import { Table, Spinner, Alert } from "react-bootstrap";
import { getProducts } from "../api/productsApi";
import { useTheme } from "../context/ThemeContext";
import "./ProductList.css";

interface Product {
    productId: string;
    name: string;
    description: string;
    price: number;
}

export const ProductList = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const { theme } = useTheme();

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const data = await getProducts();
                setProducts(data);
            } catch (err) {
                setError("Failed to load products. Please try again later.");
            } finally {
                setLoading(false);
            }
        };

        fetchProducts();
    }, []);

    if (loading) return <Spinner animation="border" />;
    if (error) return <Alert variant="danger">{error}</Alert>;

    return (
        <div className="table-responsive">
            <Table striped bordered hover variant={theme === 'dark' ? 'dark' : 'light'} className="product-table">
                <thead className="table-header">
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                        <th className="price-column">Price</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map((product) => (
                        <tr key={product.productId} className="product-row">
                            <td className="product-name">{product.name}</td>
                            <td className="product-description">{product.description}</td>
                            <td className="product-price">${product.price.toFixed(2)}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
};