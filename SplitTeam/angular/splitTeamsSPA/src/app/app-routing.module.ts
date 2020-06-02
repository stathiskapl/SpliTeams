import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { SkillListComponent } from './skill-list/skill-list.component';
import { RankingsListComponent } from './rankings-list/rankings-list.component';


const routes: Routes = [
  { path: '', component: PlayerListComponent },
  { path: 'home', component: NavBarComponent },
  { path: 'players', component: PlayerListComponent },
  { path: 'skills', component: SkillListComponent },
  { path: 'rankings', component: RankingsListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
