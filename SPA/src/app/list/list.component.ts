import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ProdcutDataService } from '../_services/productdataservice';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductData } from '../_models/productdata';
import { ServerDataSource, LocalDataSource } from 'ng2-smart-table';
import {ExcelService} from '../_services/excelservice';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements OnInit {
  productDatas: ProductData[];
  selectedProductDatas: Array<ProductData> = [];
  source: LocalDataSource;

  settings = {
    selectMode: 'multi',
    //mode: 'external',   
    actions: {  
      columnTitle: 'Action',  
      add: false,  
      edit: true,  
      delete: true,  
      position: 'left'  
    },  
    delete: {
      confirmDelete: true,
    },
    add: {
      //confirmCreate: true,
    },
    edit: {
      confirmSave: true,
    },
    columns: {    
      name: {
        title: 'name',
      },
      description: {
        title: 'description',
      },
      categoryID: {
        title: 'categoryID',
      },
      minOrderQuantity: {
        title: 'minOrderQuantity',
      },
      available: {
        title: 'available',
      },
      purchasePrice: {
        title: 'purchasePrice',
      },
      unitOfMeasure: {
        title: 'unitOfMeasure',
      }
    },
  };

  constructor(private productDataService: ProdcutDataService, private activatedRoute: ActivatedRoute, private excelService:ExcelService) 
  {   
  }
  ngOnInit() {
    this.activatedRoute.data.subscribe(data => {      
      this.productDatas = data['productdatalist'];      
    });
  }

  deleteRecord(event) {                                                                                   
    console.log('deleteRecord' + event.data.zamroID);  
    this.productDataService.deleteProduct(event.data.zamroID);        
   
    this.productDataService.getProductDataList()
      .subscribe(
            (data) =>{            
            this.productDatas = data;
            }),
            err => {
            console.log("Error occured while deleting the data.");
    }                                                                   
    
  } 

  editRecord(event) { 
    console.log('editRecord===>' + event.newData.name);  
    this.productDataService.updateProduct(event.newData);     
  }

  onRowSelect(event){
    console.log(event);  
    if(event.isSelected){
      this.selectedProductDatas.push(event.data);
    }
    else{
      this.deleteItem(event.data);
    }
  }

  deleteItem(product:ProductData) {
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
