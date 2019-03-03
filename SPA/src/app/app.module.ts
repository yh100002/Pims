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
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ngxCsv } from 'ngx-csv/ngx-csv';
import { AlertifyService } from './_services/alertify.service';
import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ProductimportComponent } from './import/productimport/productimport.component';
import { HomeComponent } from './home/home.component';
import { AddHeaderInterceptor } from './_intercepter/addheader';
import { ListComponent } from './list/list.component';
import { ProductDataResolver } from './_resolvers/productdata.resolver';
import { ProdcutDataService } from './_services/productdataservice';
import { ExcelService } from './_services/excelservice';

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
      Ng2SmartTableModule,    
      NgbModule,
      NgxJsonViewerModule      
   ],
   providers: [{
      provide: HTTP_INTERCEPTORS,
      useClass: AddHeaderInterceptor,
      multi: true    
    },
    ProdcutDataService,
    ProductDataResolver,
    ExcelService,
    AlertifyService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
