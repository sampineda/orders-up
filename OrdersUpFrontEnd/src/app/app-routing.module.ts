import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './features/products/products.component';
import { LandingComponent } from './features/landing/landing.component';
import { CreateProductComponent } from './features/create-product/create-product.component';
import { EditProductComponent } from './features/edit-product/edit-product.component';
import { PageNotFoundComponent } from './features/page-not-found/page-not-found.component';
import { OrderComponent } from './features/order/order.component';
import { CreateOrderComponent } from './features/create-order/create-order.component';
import { EditOrderComponent } from './features/edit-order/edit-order.component';
import { EventosComponent } from './features/eventos/eventos.component';
import { DetailsComponent } from './features/details/details.component';
import { InventoriesComponent } from './features/inventories/inventories.component';
import { CreateIteminventoryComponent } from './features/create-iteminventory/create-iteminventory.component';
import { EditIteminventoryComponent } from './features/edit-iteminventory/edit-iteminventory.component';
import { LogInComponent } from './features/log-in/log-in.component';
import { AuthGuard } from './auth/auth.guard';
import { ClientsComponent } from './features/clients/clients.component';
import { EditClientComponent } from './features/edit-client/edit-client.component';
import { CreateClientComponent } from './features/create-client/create-client.component';
import { BusinessComponent } from './features/business/business.component';

const routes: Routes = [
  {path:'',component:LandingComponent},
  {path:'inventory', component: InventoriesComponent, canActivate:[AuthGuard]},
  {path:'inventory/create', component: CreateIteminventoryComponent, canActivate:[AuthGuard]},
  {path:'product/create', component: CreateProductComponent, canActivate:[AuthGuard]},
  {path:'inventory/edit/:id', component: EditIteminventoryComponent, canActivate:[AuthGuard]},
  {path:'orders', component: OrderComponent, canActivate:[AuthGuard]},
  {path:'details/:id/:orderId', component: DetailsComponent, canActivate:[AuthGuard]},
  {path:'orders/create', component: CreateOrderComponent, canActivate:[AuthGuard]},
  {path:'orders/edit/:id', component: EditOrderComponent, canActivate:[AuthGuard]},
  {path:'events', component: EventosComponent, canActivate:[AuthGuard]},
  {path:'login', component: LogInComponent},
  {path:'clients', component: ClientsComponent, canActivate:[AuthGuard]},
  {path:'clients/edit/:id', component: EditClientComponent, canActivate:[AuthGuard]},
  {path:'clients/create', component: CreateClientComponent, canActivate:[AuthGuard]},
  {path:'business', component: BusinessComponent, canActivate:[AuthGuard]},
  {path:'**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
