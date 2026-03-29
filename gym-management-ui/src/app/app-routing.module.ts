import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MembersComponent } from './members/members.component';
import { TrainersComponent } from './trainers/trainers.component';
import { PlansComponent } from './plans/plans.component';

const routes: Routes = [
  { path: 'members', component: MembersComponent },
  { path: 'trainers', component: TrainersComponent },
  { path: 'plans', component: PlansComponent },
  { path: '', redirectTo: '/members', pathMatch: 'full' },
  { path: '**', redirectTo: '/members' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
