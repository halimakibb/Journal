import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor( private router: Router ) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    // tslint:disable-next-line:no-debugger
    debugger;
    localStorage.removeItem('token');
    this.router.navigate(['']);
    window.location.reload();
  }
  isLoggedIn() {
    // tslint:disable-next-line:no-debugger
    debugger;
    const userId = localStorage.getItem('token');
    return !!userId;
  }
}
