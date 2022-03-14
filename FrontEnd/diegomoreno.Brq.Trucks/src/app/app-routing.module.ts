import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TrucksDetailsComponent } from './componentes/trucks/trucks-details/trucks-details.component';
import { TrucksComponent } from './componentes/trucks/trucks.component';

const routes: Routes = [

  {path: 'truck/details/:id', component: TrucksDetailsComponent},
  {path: 'truck/details', component: TrucksDetailsComponent},
  {
    path: 'truck',
    component: TrucksComponent
  },
  { path: '', redirectTo: 'truck', pathMatch: 'full' },
  { path: '**', redirectTo: 'truck', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
