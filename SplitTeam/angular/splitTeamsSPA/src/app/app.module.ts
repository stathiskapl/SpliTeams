import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { PlayerService } from './Services/player.service';

@NgModule({
   declarations: [
      AppComponent,
      NavBarComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule
   ],
   providers: [
      PlayerService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
