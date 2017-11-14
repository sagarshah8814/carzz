import { Injectable} from '@angular/core';
import { AuthService } from "./auth.service";
import { CanActivate } from "@angular/router";

@Injectable()
export class AuthGuard implements CanActivate{
    constructor(protected auth: AuthService) { }
    canActivate():boolean {
        if (this.auth.isAuthenticated()) {
            return true;
        } else {
            window.location.href = "https://sagarshah.auth0.com/login?client=uKBlaIdA5sN6PYsw3VbDglCVaJivarcB";
            return false;
        }
    }    
}