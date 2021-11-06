import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingComponent } from './features/landing/landing.component';
import { PageNotFoundComponent } from './features/page-not-found/page-not-found.component';
import { NavigationComponent } from './features/navigation/navigation.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ProductsComponent } from './features/products/products.component';
import { CreateProductComponent } from './features/create-product/create-product.component';
import { EditProductComponent } from './features/edit-product/edit-product.component';
import { OrderComponent } from './features/order/order.component';
import { CreateOrderComponent } from './features/create-order/create-order.component';
import { EditOrderComponent } from './features/edit-order/edit-order.component';
import { DeleteOrderComponent } from './features/delete-order/delete-order.component';
import { FullCalendarModule } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import { EventosComponent } from './features/eventos/eventos.component';
import { DetailsComponent } from './features/details/details.component';
import { InventoriesComponent } from './features/inventories/inventories.component';
import { CreateIteminventoryComponent } from './features/create-iteminventory/create-iteminventory.component';
import { EditIteminventoryComponent } from './features/edit-iteminventory/edit-iteminventory.component';
import { NavigationUserComponent } from './features/navigation-user/navigation-user.component';
import { LogInComponent } from './features/log-in/log-in.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { UsersService } from './services/users.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { DatePipe } from '@angular/common';
import { ClientsComponent } from './features/clients/clients.component';
import { EditClientComponent } from './features/edit-client/edit-client.component';
import { CreateClientComponent } from './features/create-client/create-client.component';
import { BusinessComponent } from './features/business/business.component';

FullCalendarModule.registerPlugins([
  dayGridPlugin,
  interactionPlugin
]);

@NgModule({
  declarations: [
    AppComponent,
    LandingComponent,
    PageNotFoundComponent,
    NavigationComponent,
    ProductsComponent,
    CreateProductComponent,
    EditProductComponent,
    OrderComponent,
    CreateOrderComponent,
    EditOrderComponent,
    DeleteOrderComponent,
    EventosComponent,
    DetailsComponent,
    InventoriesComponent,
    CreateIteminventoryComponent,
    EditIteminventoryComponent,
    NavigationUserComponent,
    LogInComponent,
    ClientsComponent,
    EditClientComponent,
    CreateClientComponent,
    BusinessComponent
  ],
  imports: [
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FullCalendarModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [DatePipe,UsersService,{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
