import {Component, NgModule, VERSION, Pipe, PipeTransform, OnInit} from '@angular/core';
import {BrowserModule, DomSanitizer} from '@angular/platform-browser';
import { ApiService } from '../_services/api.service';
import { ProductData } from '../_models/productdata';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { PaginationComponent } from 'ngx-bootstrap';
import { ExcelService } from '../_services/excelservice';


@Pipe({
  name: 'highlight'
})
export class HighlightSearch implements PipeTransform {
constructor(private sanitizer: DomSanitizer){}

transform(value: any, args: any): any {
  if (!args) {
    return value;
  }
  // Match in a case insensitive maneer
  const re = new RegExp(args, 'gi');
  const match = value.match(re);

  // If there's no match, just return the original value.
  if (!match) {
    return value;
  }

  const replacedValue = value.replace(re, "<mark>" + match[0] + "</mark>")
  return this.sanitizer.bypassSecurityTrustHtml(replacedValue);
}
}


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {
  pagination : Pagination = { from: 0, count:0, index:0, pages:0, size:0, hasPrevious:false, hasNext:false};
  displayedColumns: string[] = ['select', 'detail', 'name', 'description', 'minOrderQuantity'];
  data: ProductData[] = [];
  isLoadingResults = true;
  searchTerm: string;
  selectedProductDatas: Array<ProductData> = [];

  constructor(private api: ApiService, private excelService:ExcelService) { }

  ngOnInit() {
    this.api.getProducts(1) 
      .subscribe(res => {
        this.data = res.items;       
        this.pagination.from = res.from;
        this.pagination.count = res.count;
        this.pagination.index = res.index;
        this.pagination.pages = res.pages;
        this.pagination.size = res.size;       
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }

  pageChanged(event: any): void {
    this.pagination.index = event.page;
    this.loadAll();   
  }

  loadAll()
  {
    console.log(this.pagination.index);
    this.api.getProducts(this.pagination.index)
    .subscribe(res => {
      this.data = res.items;
      this.isLoadingResults = false;
      this.pagination.from = res.from;
      this.pagination.count = res.count;
      this.pagination.index = res.index;
      this.pagination.pages = res.pages;
      this.pagination.size = res.size;       
    }, err => {
      console.log(err);
      this.isLoadingResults = false;
    });    
  }
  
  updateSearch(e) {
    this.searchTerm = e.target.value;
  }

  onSelected(event, product:ProductData){
    console.log(event.checked);      
    if(event.checked){
      this.selectedProductDatas.push(product);
    }
    else{
      this.deleteItem(product);
    }
  }

  deleteItem(product:ProductData) {
    console.log(product);
    const index: number = this.selectedProductDatas.findIndex(p => p.zamroID === product.zamroID);
    if (index !== -1) {
        this.selectedProductDatas.splice(index, 1);
    }        
}
  
exportToExcel()
{
 console.log(this.selectedProductDatas);  
 this.excelService.exportAsExcelFile(this.selectedProductDatas, 'PIMS');
  
}

}
