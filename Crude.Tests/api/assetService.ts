import axios from 'axios';
import type { EnergyAsset } from '../types/index'; // Use 'type' keyword

const API_URL = 'http://localhost:5189/api/Assets';

export const assetService = {
    getAssets: async () => {
        const response = await axios.get<EnergyAsset[]>(API_URL);
        return response.data;
    },
    updateAsset: async (id: string, asset: EnergyAsset) => {
        await axios.put(`${API_URL}/${id}`, asset);
    }
};