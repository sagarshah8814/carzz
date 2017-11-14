import { Injectable } from '@angular/core';
import { AuthHttp } from "angular2-jwt/angular2-jwt";
import {SaveVehicle} from '../models/saveVehicle';

@Injectable()
export class AdminService {

    constructor(private authHttp:AuthHttp) { }

    getSoldVehicles() {
        return this.authHttp.get('/api/admin').map(res => res.json());
    }

    changeVehicleStatus(v:SaveVehicle) {
        return this.authHttp.put('/api/admin/' + v.id,v).map(res => res.json());
    }
    getChartData() {
        return this.authHttp.get('/api/admin/chart').map(res => res.json());
    }
}