import { NgModule } from '@angular/core';
import { CarListComponent } from './car-list/car-list.component';
import { RouterModule, Routes } from '@angular/router';
import { CarAddComponent } from './car-add/car-add.component';
import { AuthorizationGuard } from 'src/app/guards/authorization.guard';
import { CarDetailsComponent } from './car-details/car-details.component';
import { CarUpdateComponent } from './car-update/car-update.component';

const routes: Routes = [
  { path: 'car-list', component: CarListComponent },
  { path: 'car-details/:id', component: CarDetailsComponent},
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthorizationGuard],
    children: [
      { path: 'car-add', component: CarAddComponent },
      { path: 'car-update/:id', component: CarUpdateComponent}
    ]
  },
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ], 
  exports: [
    RouterModule
  ]
})
export class CarRoutingModule { }
