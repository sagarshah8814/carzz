import { Contact } from "./contact";

export interface SaveVehicle {
    id: number;
    modelId: number;
    makeId: number;
    year: number;
    title: string;
    odometer: number;
    features: number[];
    contact:Contact;
    price:number;
}