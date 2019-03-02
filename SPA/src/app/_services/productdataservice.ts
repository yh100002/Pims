import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductData } from '../_models/productData';
import { PaginatedResult } from '../_models/pagination';



@Injectable({
    providedIn: 'root'
  })
  export class ProdcutDataService {
  baseUrl = environment.apiUrl + 'productquery/productlist/';
  
  constructor(private http: HttpClient) {}  
 
  
  getProductDataList(): Observable<ProductData[]> {
    return this.http.get<ProductData[]>(this.baseUrl);
  }
  
  }
  