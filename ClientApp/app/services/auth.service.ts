import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import 'rxjs/add/operator/filter';
import {JwtHelper} from 'angular2-jwt';
import * as auth0 from 'auth0-js';
@Injectable()
export class AuthService {
    profile: any;
    private roles: string[] = [];
    jwtHelper = new JwtHelper();
   
    auth0 = new auth0.WebAuth({
        clientID: 'uKBlaIdA5sN6PYsw3VbDglCVaJivarcB',
        domain: 'sagarshah.auth0.com',
        responseType: 'token id_token',
        audience: 'https://api.carzz.com',
        redirectUri: 'http://localhost:49816/vehicles',
        scope: 'openid email profile'
    });

    constructor(public router: Router) {
        this.readUserProfileLocalStorage();
        this.readUserRolesLocalStorage();
    }
    
    public isInRole(roleName: string) {
        
            return this.roles.indexOf(roleName) > -1;
    }
    public login(): void {
        this.auth0.authorize();
    }
    public handleAuthentication(): void {
        
        this.auth0.parseHash((err, authResult) => {
            //console.log(authResult);
            if (authResult && authResult.accessToken && authResult.idToken) {
                window.location.hash = '';
                this.setSession(authResult);
                this.readUserRolesLocalStorage();
                this.router.navigate(['/']);
             
            } else if (err) {
                this.router.navigate(['/home']);
                console.log(err);
            }
        });
       
    }


    private setSession(authResult: any): void {
        var y: any;
        y = localStorage.getItem('profile');
        this.profile = JSON.parse(y);
        // Set the time that the access token will expire at
        const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
        localStorage.setItem('access_token', authResult.accessToken);
        //if (authResult.idToken) {
           //localStorage.setItem('token', authResult.idToken);
       // } else {
          localStorage.setItem('token',authResult.accessToken);
        //}
        localStorage.setItem('expires_at', expiresAt);
        this.auth0.client.userInfo(authResult.accessToken, (err, profile) => {
            if (err) {
                throw err;
            }
            localStorage.setItem('profile', JSON.stringify(profile));
            this.readUserProfileLocalStorage();
        });
    }
    private readUserProfileLocalStorage() {
        var x: any;
        x = localStorage.getItem('profile');
        this.profile = JSON.parse(x);
    }
    private readUserRolesLocalStorage() {

        var token = localStorage.getItem('token');
        if (token) {
            var jwtHelper = new JwtHelper();
            var decodedToken = jwtHelper.decodeToken(token);
            this.roles = decodedToken['https://carzz.com/roles'] || [];
        }
    }

    public getUserEmail() {
        return this.profile.email;
    }
    public logout(): void {
        // Remove tokens and expiry time from localStorage
        localStorage.removeItem('access_token');
        localStorage.removeItem('token');
        localStorage.removeItem('expires_at');
        localStorage.removeItem('profile');
        this.profile = null;
        this.roles = [];
        alert('You have successfully Logged Out');
        // Go back to the home route
        this.router.navigate(['/']);
    }

    public isAuthenticated(): boolean {
        // Check whether the current time is past the
        // access token's expiry time
        var x:any;
        
             x = localStorage.getItem('expires_at');
        
        var expiresAt = JSON.parse(x);
        
        return new Date().getTime() < expiresAt;
    }
}
