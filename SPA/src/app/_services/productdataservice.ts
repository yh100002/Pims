import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductData } from '../_models/productData';
import { PaginatedResult } from '../_models/pagination';
import { AlertifyService } from './alertify.service';


@Injectable({
    providedIn: 'root'
  })
  export class ProdcutDataService {
  baseUrl = environment.apiUrl + 'product/';
  
  constructor(private http: HttpClient, private alertify: AlertifyService) {}  
 
  
  getProductDataList(): Observable<ProductData[]> {
    return this.http.get<ProductData[]>(this.baseUrl + 'getproducts');
  }

  deleteProduct (id: string) {   
    console.log('deleteProduct:' + this.baseUrl + id);
    return this.http.delete<any>(this.baseUrl + id).subscribe(
      res => { console.log(res);  },
    (err: HttpErrorResponse) => {
      if (err.error instanceof Error) {
        this.alertify.error(err.message);
      } else {
        this.alertify.error(err.message);
      }
    });
  }

  updateProduct(product : ProductData)
  {
    console.log('updateProduct:' + product.name);    
    return this.http.put<ProductData>(this.baseUrl + 'update', product).subscribe(
      res => { console.log(res);  },
    (err: HttpErrorResponse) => {
      if (err.error instanceof Error) {
        this.alertify.error(err.message);
      } else {
        this.alertify.error(err.message);
      }
    });
  }
  
}
  