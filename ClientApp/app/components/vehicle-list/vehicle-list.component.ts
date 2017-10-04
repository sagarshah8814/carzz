import { Component,OnInit } from "@angular/core";
import { Vehicle } from "../../models/vehicle";
import { VehicleService } from "../../services/vehicle.service";

@Component({
    selector: "vehicle-list",
    templateUrl: "./vehicle-list.component.html",
    styleUrls:[]
})
export class VehicleListComponent implements OnInit{
    vehicles:Vehicle[];
    makes: any[];
    models:any[];
    years:any[];
    filter: any = {};
    tableHead:any[] = [
        { title: 'Id' },
        { title: 'Make',key:'make',isSortable:true },
        { title: 'Model',key:'model',isSortable:true },
        { title: 'Contact Name',key:'contactName',isSortable:true },
        { title: 'Year',key:'year',isSortable:true },
        {  }
    ];

    constructor(private vehicleService:VehicleService) { }

    ngOnInit() {
        this.vehicleService.getMakes().subscribe(m => {
            this.makes = m;
        });
        this.years=this.vehicleService.getYear();
        this.populateVehicles();
    }

    populateVehicles() {
        this.vehicleService.getVehicles(this.filter).subscribe(v => {
            this.vehicles = v;
        });
    }

    onMakeSelect() {
        this.populateModel();
        delete this.filter.modelId;
    }

    private populateModel() {
        var selectedMake = this.makes.find(m => m.id == this.filter.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }
    
    onFilterChange() {
        this.populateVehicles();
    }

    onFilterReset() {
        this.filter = {};
        this.models = [];
        this.onFilterChange();
    }

    onSortBy(columnName: string) {
        if (this.filter.sortBy==columnName) {
            this.filter.isSortAscending = !this.filter.isSortAscending;
        } else {
            this.filter.sortBy = columnName;
            this.filter.isSortAscending = false;
        }
        this.populateVehicles();
    }
}