import { Component, AfterViewInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-planes-component',
  templateUrl: './planes.component.html'
})

export class PlanesComponent implements AfterViewInit {

  public http: HttpClient;
  public baseUrl: string;

  public moreThan200Manufa_Planes: Map<string, number>;
  public flights_MoreThan200_ManufaPlanes: Map<string, number>;
  public planesForEachAirbusModel: Map<string, number>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngAfterViewInit() {
    //Feature 11 - Manufacturers with more than 200 planes
    this.loadMoreThan200Manufa_Planes() ;

    //Feature 12 - Flights for manufacturers with more than 200 planes
    this.loadflights_MoreThan200_ManufaPlanes();

    //Feature 13 - Planes for each Airbus model
    this.loadPlanesForEachAirbusModel();
  }

  loadMoreThan200Manufa_Planes() {
    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/MoreThan200Manufa_Planes').subscribe(result => {
      this.moreThan200Manufa_Planes = result;
    }, error => console.error(error));
  }

  loadflights_MoreThan200_ManufaPlanes() {
    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/Flights_MoreThan200_ManufaPlanes').subscribe(result => {
      this.flights_MoreThan200_ManufaPlanes = result;
    }, error => console.error(error));
  }

  loadPlanesForEachAirbusModel() {
    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/Airbus_Planes').subscribe(result => {
      this.planesForEachAirbusModel = result;
    }, error => console.error(error));
  }
}

