import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FlightsComponent, GetValuesPipe } from './flights/flights.component';
import { PlanesComponent } from './planes/planes.component';
import { WeatherComponent } from './weather/weather.component';
import { MapComponent } from './map/map.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FlightsComponent,
    WeatherComponent,
    PlanesComponent,
    MapComponent,
    GetValuesPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: FlightsComponent, pathMatch: 'full' },
      { path: 'weather', component: WeatherComponent },
      { path: 'planes', component: PlanesComponent },
      { path: 'map', component: MapComponent },
    ])
  ],
  providers: [GetValuesPipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
