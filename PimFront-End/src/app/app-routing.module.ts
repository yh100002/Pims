import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ProductAddComponent } from './product-add/product-add.component';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { HomeComponent } from './home/home.component';
import { ProductsComponent } from './products/products.component';
import { ProductImportComponent } from './product-import/product-import.component';


const routes: Routes = [
  { path: 'home', component: HomeComponent },
  {
    path: 'import',
    component: ProductImportComponent,
    data: { title: 'Import Product' }    
  },
  {
    path: 'products',
    component: ProductsComponent,
    data: { title: 'List of Products' }    
  },
  {
    path: 'product-detail/:id',
    component: ProductDetailComponent,
    data: { title: 'product Details' }     
  },
  {
    path: 'product-add',
    component: ProductAddComponent,
    data: { title: 'Add Product' }    
  },
  {
    path: 'product-edit/:id',
    component: ProductEditComponent,
    data: { title: 'Product Edit' }    
  },  
  {path: '**', redirectTo: 'products', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
