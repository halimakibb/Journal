import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService,
    private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).pipe(
      map((response: any) => {
        const isSuccess = response.isSuccess;
        if (isSuccess) {
          const user = JSON.parse(response.data);
          localStorage.setItem('token', user.token);
          this.router.navigate(['']);
        } else { console.log(response.message); }
      })
    ).subscribe(next => {
      }, error => {
        console.log('login failed');
      });
  }
}
