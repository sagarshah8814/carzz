import { Contact } from "./contact";
import { KeyValuePair } from "./keyValuePair";

export interface Vehicle {
    id: number;
    model: KeyValuePair;
    make: KeyValuePair;
    year: number;
    title: string;
    odometer: number;
    features: KeyValuePair[];
    contact:Contact;
    price:number;
    lastUpdate:string;
}