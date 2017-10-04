import { Injectable} from "@angular/core";
import { Http } from "@angular/http";
import "rxjs/add/operator/map";
import { SaveVehicle } from "../models/saveVehicle";

@Injectable()
export class VehicleService {
    year: any[] = [1989];
    constructor(private http:Http) { }

    getMakes() {
        return this.http.get('/api/makes').map(res => res.json());
    }
    getFeatures() {
        return this.http.get('/api/features').map(res => res.json());
    }
    getYear() {
        for (var i = 1990; i < 2017; i++) {
            this.year.push(i);
        }
        return this.year.slice();
    }
    create(vehicle: SaveVehicle) {
        vehicle.id = 0;
        return this.http.post('/api/vehicles',vehicle).map(res => res.json());
    }
    getVehicle(id:number) {
        return this.http.get('/api/vehicles/' + id).map(res => res.json());
    }
    getVehicles(filter:any) {
        return this.http.get('/api/vehicles' + '?' + this.toQueryString(filter)).map(res => res.json());
    }
    toQueryString(obj:any) {
        var parts:any[] = [];
        for (var property in obj) {
            var value = obj[property];
            if (value != null) {
                parts.push(encodeURIComponent(property) + "=" + encodeURIComponent(value));
            }
        }
        return parts.join("&");
    }
    update(v:SaveVehicle) {
       return this.http.put('/api/vehicles/' + v.id, v).map(res => res.json());
    }
    delete(id:number) {
        return this.http.delete('/api/vehicles/' + id).map(res => res.json());
    }
}