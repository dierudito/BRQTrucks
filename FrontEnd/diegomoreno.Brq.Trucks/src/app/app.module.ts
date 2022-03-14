import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TrucksDetailsComponent } from './componentes/trucks/trucks-details/trucks-details.component';
import { TrucksComponent } from './componentes/trucks/trucks.component';
import { TruckService } from './services/truck.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    TrucksComponent,
    TrucksDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [TruckService],
  bootstrap: [AppComponent]
})
export class AppModule { }
