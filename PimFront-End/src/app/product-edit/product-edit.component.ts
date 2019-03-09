import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ApiService } from '../_services/api.service';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ProductData } from '../_models';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent implements OnInit {
  productForm: FormGroup;
  zamroID: string;
  name: string;
  description: string;
  minOrderQuantity: number;
  unitOfMeasure: string;
  categoryID: number;
  purchasePrice: number;
  available: number;
  isLoadingResults = false;

  constructor(private router: Router, private route: ActivatedRoute, private api: ApiService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.getProduct(this.route.snapshot.params['id']);
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

  getProduct(id:string) {
    this.api.getProduct(id).subscribe(data => {
      this.zamroID = data.zamroID;
      this.productForm.setValue({        
        name: data.name,
        description: data.description,
        minOrderQuantity: data.minOrderQuantity,
        unitOfMeasure: data.unitOfMeasure,
        categoryID: data.categoryID,
        purchasePrice: data.purchasePrice,
        available: data.available
      });
    });
  }

  onFormSubmit(form:NgForm) {
    this.isLoadingResults = true;        
    this.api.updateProduct(this.zamroID, form)
      .subscribe(res => {          
          this.isLoadingResults = false;
          this.router.navigate(['/product-detail', this.zamroID]);
        }, (err) => {
          console.log(err);
          this.isLoadingResults = false;
        }
      );
  }

  productDetails() {
    this.router.navigate(['/product-detail', this.zamroID]);
  }

}
