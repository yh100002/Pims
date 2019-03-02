import {Injectable} from '@angular/core';
import { ProductData } from '../_models/productData';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ProdcutDataService } from '../_services/productdataservice';

@Injectable()
export class ProductDataResolver implements Resolve<ProductData[]> {
    pageNumber = 1;
    pageSize = 12;

    constructor(private prodcutDataService: ProdcutDataService, private router: Router) {}


    resolve(route: ActivatedRouteSnapshot): Observable<ProductData[]> {
        console.log('ProductDataResolver');        
        return this.prodcutDataService.getProductDataList().pipe(
            catchError(error => {
                //this.alertify.error('Problem retrieving data');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
