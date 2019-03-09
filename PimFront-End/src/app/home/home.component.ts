import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit {
    currentUser: string;
    currentUserSubscription: Subscription;        

    constructor() {
       
        
      }

    ngOnInit() {      
    }
    
}