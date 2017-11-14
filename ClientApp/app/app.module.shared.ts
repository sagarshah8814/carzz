import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ChartModule } from 'angular2-chartjs';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleService } from "./services/vehicle.service";
import { VehicleListComponent } from "./components/vehicle-list/vehicle-list.component";
import { VehicleViewComponent } from "./components/vehicle-view/vehicle-view.component";
import { PhotoService } from "./services/photo.service";
import { AuthService } from "./services/auth.service";
import { AdminComponent } from "./components/admin/admin.component";
import { AuthGuard } from "./services/auth-guard.service";
import { AdminAuthGuard } from "./services/admin-auth-guard.service";
import { AUTH_PROVIDERS } from "angular2-jwt/angular2-jwt";
import { AdminService } from "./services/admin.service";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VehicleFormComponent,
        VehicleListComponent,
        VehicleViewComponent,
        AdminComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ChartModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path:'vehicles',component:VehicleListComponent },
            { path:'admin',component:AdminComponent, canActivate:[AdminAuthGuard] },
            { path: 'vehicles/new', component: VehicleFormComponent, canActivate:[AuthGuard] },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent, canActivate:[AuthGuard] },
            { path: 'vehicles/:id', component:VehicleViewComponent, canActivate:[AuthGuard] },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        VehicleService,
        PhotoService,
        AuthService,
        AUTH_PROVIDERS,
        AuthGuard,
        AdminAuthGuard,
        AdminService
    ]
})
export class AppModuleShared {
}
