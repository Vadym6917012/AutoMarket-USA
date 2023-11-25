import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AdminGuard } from 'src/app/guards/admin.guard';
import { AddEditMemberComponent } from './add-edit-member/add-edit-member.component';
import { AddEditGenerationComponent } from './add-edit-generation/add-edit-generation.component';

const routes: Routes = [
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AdminGuard],
    children: [
      { path: '', component: AdminComponent},
      { path: 'add-edit-member', component: AddEditMemberComponent},
      { path: 'add-edit-member/:id', component: AddEditMemberComponent},
      { path: 'add-edit-generation', component: AddEditGenerationComponent},
      { path: 'add-edit-generation/:id', component: AddEditGenerationComponent},
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
