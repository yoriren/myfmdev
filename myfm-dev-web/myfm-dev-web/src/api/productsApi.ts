import axios from "axios";

const API_BASE_URL = "http://localhost:5066"; 

export const getProducts = async () => {
    try {
        const response = await axios.get(`${API_BASE_URL}/api/products`);
        return response.data;
    } catch (error) {
        console.error("Error fetching products:", error);
        throw error;
    }
};