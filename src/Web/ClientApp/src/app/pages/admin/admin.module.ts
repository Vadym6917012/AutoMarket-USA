import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { AdminRoutingModule } from './admin-routing.module';
import { AddEditMemberComponent } from './add-edit-member/add-edit-member.component';
import { SharedModule } from "../../shared/shared.module";
import { AddEditGenerationComponent } from './add-edit-generation/add-edit-generation.component';
import { SideBarComponent } from '../side-bar/side-bar.component';
import { GenerationsComponent } from './generations/generations.component';
import { MakesComponent } from './makes/makes.component';
import { ModelsComponent } from './models/models.component';
import { ModificationsComponent } from './modifications/modifications.component';
import { UsersComponent } from './users/users.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    declarations: [
        AdminComponent,
        AddEditMemberComponent,
        AddEditGenerationComponent,
        SideBarComponent,
        GenerationsComponent,
        MakesComponent,
        ModelsComponent,
        ModificationsComponent,
        UsersComponent,
        DashboardComponent,
    ],
    imports: [
        CommonModule,
        AdminRoutingModule,
        SharedModule,
    ]
})
export class AdminModule { }
