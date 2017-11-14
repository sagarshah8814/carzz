import { Component, OnInit, ElementRef, ViewChild } from "@angular/core";
import { Vehicle } from "../../models/vehicle";
import { VehicleService } from "../../services/vehicle.service";
import { ActivatedRoute, Router } from "@angular/router";
import { PhotoService } from "../../services/photo.service";
import { AuthService } from "../../services/auth.service";
import { AdminService } from "../../services/admin.service";

@Component({
    selector: "vehicle-view",
    templateUrl:"./vehicle-view.component.html"
})
export class VehicleViewComponent implements OnInit {
    @ViewChild("fileInput") fileInput:ElementRef;
    vehicle: any;
    id: number;
    photos:any[];

    constructor(private vehicleService: VehicleService,
        private route: ActivatedRoute,
        private router: Router,
        private photoService: PhotoService,
        private auth: AuthService, private adminService:AdminService) {
        this.route.params.subscribe(data => {
            this.id = +data['id'];
            if (isNaN(this.id) || this.id <= 0) {
                router.navigate(['/vehicles']);
                return;
            }
        }
        );
    }
    ngOnInit() {
        this.photoService.getPhotos(this.id).subscribe(
            p => this.photos = p
        );

        this.vehicleService.getVehicle(this.id).subscribe(
            v => this.vehicle = v,
            err => {
                if (err.status == 404) {
                    this.router.navigate(['/vehicles']);
                    return;
                }
            });

       
    }
    checkForUser(email:string) {
        if (this.auth.getUserEmail() == email)
            return true;
        return false;
    }

    onDelete() {
        if (confirm("Are you sure you want to delete this vehicle?")) {
            this.vehicleService.delete(this.vehicle.id).subscribe(x => {
                this.router.navigate(['/vehicles']);
            });
        }
    }

    uploadPhoto() {
        var nativeElement = this.fileInput.nativeElement;
            var file = nativeElement.files[0];
        this.photoService.upload(this.id, file).subscribe(x => {
            this.photos.push(x);
        });
    }

    onRemoveImage(photoId: number) {
        var index = this.photos.indexOf(photoId);
        if (confirm("Are you sure you want to remove this image?")) {
            this.photoService.removePhotos(this.id, photoId).subscribe(x => {
                console.log(x);
                this.photos.splice(index, 1);
            });
        }
    }

    onSellVehicle() {
        this.adminService.changeVehicleStatus(this.vehicle).subscribe(x => {
            console.log(x);
            this.router.navigate(['/admin']);
        });
    }
    
}