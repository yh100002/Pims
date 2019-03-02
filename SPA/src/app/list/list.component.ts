import { Component, OnInit } from '@angular/core';
import { ProdcutDataService } from '../_services/productdataservice';
import { ActivatedRoute } from '@angular/router';
import { ProductData } from '../_models/productdata';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  productDatas: ProductData[];

  constructor(private productDataService: ProdcutDataService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.data.subscribe(data => {
      console.log(data['productdatalist']);  
      this.productDatas = data['productdatalist'].items;
        
    });
  }


}
