import { Component, OnInit } from '@angular/core';
import { AdminService } from "../../services/admin.service";
import { Vehicle } from "../../models/vehicle";
import "rxjs/add/Observable/forkJoin";
import { Observable } from "rxjs/Observable";
import { ChartData } from "../../models/chartData";

@Component({
    selector: 'admin',
    templateUrl:'./admin.component.html'
})
export class AdminComponent implements OnInit{
    vehicles: Vehicle[];
    apiData:any[]=[
        {
            name:String,
            number:Number
        }];
    count:number[]=[];
    data:ChartData={
        labels:[],
        datasets:[
            {
                data: [],
                backgroundColor: ["#ffff1a", "#00e68a", "#ff0000", "#1a53ff","#000000"]
            }]
    };
    constructor(private adminService:AdminService) { }

    ngOnInit() {
        var sources = [this.adminService.getSoldVehicles(), this.adminService.getChartData()];
        Observable.forkJoin(sources).subscribe(data => {
            this.vehicles = <any[]>(data[0]);
            this.apiData = <any[]>data[1];
            this.populateChart();
            console.log(this.apiData);
            console.log(this.data);
        });
    }
    populateChart() {
        for(let i of this.apiData) {
            this.data.labels.push(i.name);
            for(let x of this.data.datasets)
            x.data.push(i.number);
        }
        
    }
    checkForData() {
        if (this.data.datasets[0].data.length > 0) {
            return true;
        }
        return false;
    };
   
}