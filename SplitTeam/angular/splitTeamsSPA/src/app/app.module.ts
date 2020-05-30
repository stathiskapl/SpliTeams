import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { ToastrModule } from 'ngx-toastr';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { FormsModule } from '@angular/forms';
import { PlayerService } from './Services/player.service';
import { PlayerDetailComponent } from './player-detail/player-detail.component';
import { PlayerListComponent } from './player-list/player-list.component';
import { SkillListComponent } from './skill-list/skill-list.component';
import { CommonModule } from '@angular/common';

@NgModule({
   declarations: [
      AppComponent,
      NavBarComponent,
      PlayerDetailComponent,
      PlayerListComponent,
      SkillListComponent
   ],
   imports: [
      HttpClientModule,
      CommonModule,
      ToastrModule.forRoot(),
      BrowserModule,
      AppRoutingModule,
      FormsModule,
      BrowserAnimationsModule,
   ],
   providers: [
      PlayerService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
