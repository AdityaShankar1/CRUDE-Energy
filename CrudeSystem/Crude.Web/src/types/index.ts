// src/types/index.ts

export enum NodeStatus {
    Healthy = 0,
    Warning = 1,
    Flagged = 2,
    Maintenance = 3
}

export interface EnergyAsset {
    id: string;
    name: string;
    currentReading: number;
    status: NodeStatus;
    lastUpdated: string;
    maxConsumptionThreshold: number;
}

// Adding this dummy constant forces Vite to treat this as a non-empty module
export const TYPE_CHECK = true;