import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AdminGuard } from 'src/app/guards/admin.guard';
import { AddEditMemberComponent } from './add-edit-member/add-edit-member.component';
import { AddEditGenerationComponent } from './add-edit-generation/add-edit-generation.component';
import { GenerationsComponent } from './generations/generations.component';
import { MakesComponent } from './makes/makes.component';
import { ModelsComponent } from './models/models.component';
import { ModificationsComponent } from './modifications/modifications.component';
import { UsersComponent } from './users/users.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AdminGuard],
    component: AdminComponent,
    children: [
      { path: '', component: DashboardComponent},
      { path: 'add-edit-member', component: AddEditMemberComponent},
      { path: 'add-edit-member/:id', component: AddEditMemberComponent},
      { path: 'add-edit-generation', component: AddEditGenerationComponent},
      { path: 'add-edit-generation/:id', component: AddEditGenerationComponent},
      { path: 'generations', component: GenerationsComponent},
      { path: 'makes', component: MakesComponent},
      { path: 'models', component: ModelsComponent},
      { path: 'modifications', component: ModificationsComponent},
      { path: 'users', component: UsersComponent},
      { path: 'dashboard', component: DashboardComponent}
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
export class AdminRoutingModule { }
