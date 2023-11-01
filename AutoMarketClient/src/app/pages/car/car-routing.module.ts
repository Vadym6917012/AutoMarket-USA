import { NgModule } from '@angular/core';
import { CarListComponent } from './car-list/car-list.component';
import { RouterModule, Routes } from '@angular/router';
import { CarAddComponent } from './car-add/car-add.component';
import { AuthorizationGuard } from 'src/app/guards/authorization.guard';

const routes: Routes = [
  { path: 'car-list', component: CarListComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthorizationGuard],
    children: [
      { path: 'car-add', component: CarAddComponent }
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
