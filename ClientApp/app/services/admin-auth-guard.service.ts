import { Injectable } from '@angular/core';
import { AuthGuard } from './auth-guard.service';
import { AuthService } from './auth.service';
@Injectable()
export class AdminAuthGuard extends AuthGuard {
    constructor(auth: AuthService) {
        super(auth);
    }
    canActivate() {
        var isAuthenticatedUser = super.canActivate();
        return isAuthenticatedUser ? this.auth.isInRole('Admin') : false;
    }
}