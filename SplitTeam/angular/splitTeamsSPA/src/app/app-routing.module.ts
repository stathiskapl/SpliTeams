import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PlayerListComponent } from './player-list/player-list.component';
import { SkillListComponent } from './skill-list/skill-list.component';
import { RankingsListComponent } from './rankings-list/rankings-list.component';
import { HomeComponent } from './home/home.component';
import { MoreInfoComponent } from './more-info/more-info.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserDetailComponent } from './user-detail/user-detail.component';


const routes: Routes = [
  { path: '', component: PlayerListComponent },
  { path: 'moreinfo', component: MoreInfoComponent },
  { path: 'home', component: HomeComponent },
  { path: 'players', component: PlayerListComponent },
  { path: 'skills', component: SkillListComponent },
  { path: 'rankings', component: RankingsListComponent },
  { path: 'users', component: UserListComponent },
  { path: 'users/:userId', component: UserDetailComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
