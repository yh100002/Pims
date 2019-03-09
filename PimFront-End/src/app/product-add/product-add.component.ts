import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../_services/api.service';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';

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
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.scss']
})
export class ProductAddComponent implements OnInit {

  productForm: FormGroup;
  name: string;
  description: string;
  minOrderQuantity: number;
  unitOfMeasure: string;
  categoryID: number;
  purchasePrice: number;
  available: number;
  isLoadingResults = false;

  constructor(private router: Router, private api: ApiService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.productForm = this.formBuilder.group({
      'name' : [null, Validators.required],
      'description' : [null, Validators.required],
      'minOrderQuantity' : [null, Validators.required],
      'unitOfMeasure' : [null, Validators.required],
      'categoryID' : [null, Validators.required],
      'purchasePrice' : [null, Validators.required],
      'available' : [null, Validators.required]
    });
  }

  onFormSubmit(form:NgForm) {
    this.isLoadingResults = true;
    this.api.addProduct(form)
      .subscribe(res => {          
          this.isLoadingResults = false;
          //this.router.navigate(['/books']);
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        });
  }

}
