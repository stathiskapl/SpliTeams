import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { SkillListComponent } from './skill-list/skill-list.component';


const routes: Routes = [ 
  { path: 'home', component: NavBarComponent },
  { path: 'players', component: PlayerListComponent },
  { path: 'skills', component: SkillListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
