import * as _ from "underscore";
import { Component, OnInit } from "@angular/core";
import {VehicleService} from "../../services/vehicle.service";
import { Router, ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs/Observable";
import "rxjs/add/Observable/forkJoin";
import { SaveVehicle } from "../../models/saveVehicle";
import { Vehicle } from "../../models/vehicle";

@Component({
    selector: "vehicle-form",
    templateUrl: "./vehicle-form.component.html",
    styleUrls:['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit{
    makes: any[];
    vehicles: SaveVehicle = {
        id:0,
        modelId: 0,
        makeId: 0,
        year: 0,
        title: "",
        odometer:0,
        features: [],
        contact: {
            name: '',
            phone:'',
            email:''
        }
    };
    models: any[];
    features: any[];
    makeYear:any[];
    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private vehicleService: VehicleService) {
        this.route.params.subscribe(p => {
            this.vehicles.id = +p['id'];
       });
    }

    ngOnInit() {
        var sources = [
            this.vehicleService.getMakes(),
            this.vehicleService.getFeatures()
        ];
        if (this.vehicles.id) {
            sources.push(this.vehicleService.getVehicle(this.vehicles.id));
        }
        Observable.forkJoin(sources).subscribe(data => {
            this.makes = <any[]>(data[0]);
            this.features = <any[]>((data[1]));
            if (this.vehicles.id) {
                this.setVehicle((data[2]) as any);
                this.populateModel();
            }
        },err => {
            if (err.status == 404) {
                this.router.navigate(['/home']);
            }
        });
        this.makeYear=this.vehicleService.getYear();
    }

    setVehicle(v:Vehicle) {
        this.vehicles.id = v.id;
        this.vehicles.makeId = v.make.id;
        this.vehicles.modelId = v.model.id;
        this.vehicles.year = v.year;
        this.vehicles.title = v.title;
        this.vehicles.odometer = v.odometer;
        this.vehicles.features = _.pluck(v.features, 'id');
        this.vehicles.contact = v.contact;
    }

    onMakeChange() {
        this.populateModel();
        delete this.vehicles.modelId;
    }
    private populateModel() {
        var selectedMake = this.makes.find(m => m.id == this.vehicles.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }

    onFeatureToggle(featureId:number,$event:any) {
        if ($event.target.checked) {
            this.vehicles.features.push(featureId);
        } else {
            var index = this.vehicles.features.indexOf(featureId);
            this.vehicles.features.splice(index,1);
        }
    }

    onSubmit() {
        if (this.vehicles.id) {
            this.vehicleService.update(this.vehicles).subscribe(x => {
                alert("Vehicle was succesfully updated.");
                this.router.navigate(['/vehicles']);
            });
            
        } else {
            this.vehicleService.create(this.vehicles)
                .subscribe(x => {
                    alert("Vehicle was succesfully added.");
                    this.router.navigate(['/vehicles']);
                });
        }
    }

    
}