import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { NgxJsonViewerModule } from 'ngx-json-viewer';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ProductimportComponent } from './import/productimport/productimport.component';
import { HomeComponent } from './home/home.component';
import { AddHeaderInterceptor } from './_intercepter/addheader';
import { ListComponent } from './list/list.component';
import { ProductDataResolver } from './_resolvers/productdata.resolver';
import { ProdcutDataService } from './_services/productdataservice';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      ProductimportComponent,
      HomeComponent,
      ListComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      ButtonsModule.forRoot(),
      PaginationModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      /*\nJwtModule.forRoot(\\\\\\\\nconfig
   ],
   //theauthenticationendpointdoesn’tneedtoreceiveitbecausethere’snopoint: [
      Thetokenistypicallynullwhenit’scalledanyway.\\\\\\\\nblacklistedRoutes
   ]
}),*/
       NgbModule,
       NgxJsonViewerModule
   ],
   providers: [{
      provide: HTTP_INTERCEPTORS,
      useClass: AddHeaderInterceptor,
      multi: true    
    },
    ProdcutDataService,
    ProductDataResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
