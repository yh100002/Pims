import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../_services/api.service';
import { ProductData } from '../_models/productdata';

/*
zamroID: string;
name: string;
description: string;
minOrderQuantity: number;
unitOfMeasure: string;
categoryID: number;
purchasePrice: number;
available: number;
*/

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {  
  product: ProductData = { zamroID: '', name: '', description: '', minOrderQuantity: 0, unitOfMeasure: '', categoryID: 0, purchasePrice: 0, available: 0, timestamp: null};
  isLoadingResults = true;

  constructor(private route: ActivatedRoute, private api: ApiService, private router: Router) { }

  ngOnInit() {
    console.log(this.route.snapshot.params['id']);
    this.getProductDetails(this.route.snapshot.params['id']);
  }

  getProductDetails(id) {
    console.log(id);
    this.api.getProduct(id)
      .subscribe(data => {
        this.product = data;
        console.log(this.product);
        this.isLoadingResults = false;
      });
  }

  deleteProduct(id) {
    this.isLoadingResults = true;
    this.api.deleteProduct(id)
      .subscribe(res => {
          this.isLoadingResults = false;
          this.router.navigate(['/products']);
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        }
      );
  }

}
