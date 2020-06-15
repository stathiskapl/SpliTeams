import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { JwtModule } from '@auth0/angular-jwt';
import { SidebarModule } from 'ng-sidebar';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PlayerService } from './_services/player.service';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { SkillListComponent } from './skill-list/skill-list.component';
import { CommonModule } from '@angular/common';
import { RankingsListComponent } from './rankings-list/rankings-list.component';
import { environment } from 'src/environments/environment';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MoreInfoComponent } from './more-info/more-info.component';
import { UserListComponent } from './user-list/user-list.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { TeamListComponent } from './team-list/team-list.component';
import { MatchListComponent } from './match-list/match-list.component';
import { MatchDetailComponent } from './match-detail/match-detail.component';

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavBarComponent,
      PlayerDetailComponent,
      PlayerListComponent,
      SkillListComponent,
      RankingsListComponent,
      HomeComponent,
      RegisterComponent,
      HomeComponent,
      MoreInfoComponent,
      UserListComponent,
      UserDetailComponent,
      UserListComponent,
      TeamListComponent,
      MatchListComponent,
      MatchDetailComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      CommonModule,
      ToastrModule.forRoot(),
      ReactiveFormsModule,
      NgbModule,
      SidebarModule.forRoot(),
      AppRoutingModule,
      FormsModule,
      BrowserAnimationsModule,
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/user/Login', 'localhost:5000/api/user/Create']
         }
      })
   ],
   providers: [
      PlayerService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule {
   baseUrl = environment.apiUrl;
}
