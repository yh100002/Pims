import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { ProductData } from '../_models/productdata';
import { PaginatedResult } from '../_models/pagination';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};
const apiUrl = "https://localhost:5001/api/product";

@Injectable({
  providedIn: 'root'
})
export class ApiService {    
  constructor(private http: HttpClient) { }

  getProducts (page = 0): Observable<PaginatedResult<ProductData[]>> {
    let paginatedResult: PaginatedResult<ProductData[]> = new PaginatedResult<ProductData[]>();
    const url = `${apiUrl}/getProducts?page=${page}&size=100`;
    return this.http.get<PaginatedResult<ProductData[]>>(url)
      .pipe(
        
        map(response => {
          paginatedResult = response;  
          console.log(response);
          return paginatedResult;
        })     
      );
  }

  getProduct(id: string): Observable<ProductData> {
    const url = `${apiUrl}/getProduct/${id}`;
    return this.http.get<ProductData>(url).pipe(
      tap(_ => console.log(`fetched product id=${id}`)),
      catchError(this.handleError<ProductData>(`getProduct id=${id}`))
    );
  }

  addProduct (product): Observable<ProductData> {
    const url = `${apiUrl}/addProduct/`;
    return this.http.post<ProductData>(url, product, httpOptions).pipe(
      // tslint:disable-next-line:no-shadowed-variable
      tap((product: ProductData) => console.log(`added product w/ id=${product.zamroID}`)),
      catchError(this.handleError<ProductData>('addProduct'))
    );
  }

  updateProduct (id, product): Observable<any> {    
    const url = `${apiUrl}/update/`;
    product.zamroID = id;
    return this.http.put<ProductData>(url, product, httpOptions).pipe(
      tap(_ => console.log(`updated product id=${product.description}`)),
      catchError(this.handleError<ProductData>('updateBook'))
    );
  }

  deleteProduct (id: string): Observable<ProductData> {
    const url = `${apiUrl}/${id}`;

    return this.http.delete<ProductData>(url, httpOptions).pipe(
      tap(_ => console.log(`deleted product id=${id}`)),
      catchError(this.handleError<ProductData>('deleteProduct'))
    );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
