import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GetCitiesComponent } from './get-cities/get-cities.component';
import { LikeCitiesComponent } from './like-cities/like-cities.component';
import { GetAllCitiesComponent } from './get-all-cities/get-all-cities.component';
import { LikeAllCitiesComponent } from './like-all-cities/like-all-cities.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GetCitiesComponent,
    LikeCitiesComponent,
    GetAllCitiesComponent,
    LikeAllCitiesComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'get-city', component: GetCitiesComponent },
      { path: 'like-city', component: LikeCitiesComponent },
      { path: 'get-all-cities', component: GetAllCitiesComponent },
      { path: 'like-all-cities', component: LikeAllCitiesComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
