import {Routes} from '@angular/router';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ProductimportComponent } from './import/productimport/productimport.component';
import { HomeComponent } from './home/home.component';

export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {
        path: '',          
        children: [
            {path: 'import/product', component: ProductimportComponent}   
        ]
    },
    {path: '**', redirectTo: 'home', pathMatch: 'full'},
];
