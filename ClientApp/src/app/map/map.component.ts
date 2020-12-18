import { Component, AfterViewInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-map-component',
  templateUrl: './map.component.html'
})

export class MapComponent implements AfterViewInit {

  public http: HttpClient;
  public baseUrl: string;


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }
  ngAfterViewInit(): void {
    throw new Error("Method not implemented.");
  }
}
