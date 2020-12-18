import { Component, AfterViewInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as CanvasJS from '../../assets/canvasjs.min.js';

@Component({
  selector: 'app-weather',
  templateUrl: './weather.component.html'
})
export class WeatherComponent implements AfterViewInit {

  public http: HttpClient;
  public baseUrl: string;

  public weather_Obs_forOrigins: Map<string, number>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngAfterViewInit() {
    // Feature 9 - Weather observations for origins
    this.loadWeather_Obs_forOrigins();

    // Feature 6 - Temperature attributes for origins
    this.loadTemperatureAttributesForOrigins();

    // Feature 5 - Temperatures for JFK
    this.loadJFK_Celsius_temp();

    // Feature 8 - Daily mean temperature for JFK
    this.loadJFK_DailyMean_CelTemp();

    // Feature 7 - Daily mean temperature for origins
    this.loadDailyMeanTemperatureForOrigins();
  }

  loadWeather_Obs_forOrigins() {
    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/Weather_Obs_forOrigins').subscribe(result => {
      this.weather_Obs_forOrigins = result;
    }, error => console.error(error));
  }

  loadTemperatureAttributesForOrigins() {
    let dataPointsJFK = [];
    let dataPointsEWR = [];
    let dataPointsLGA = [];

    let chart = new CanvasJS.Chart("chartContainer6", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Temperature attributes for origins",
        fontSize: 20
      },
      axisX: {
        title: "Date",
      },
      axisY: {
        title: "Temperature in Celsius"
      },
      data: [{
        type: "scatter",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "scatter",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "scatter",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    chart.render();

    this.http.get<Map<Date, number>>(this.baseUrl + 'api/Nycflights/JFK_Celsius_temp').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<Date, number>>(this.baseUrl + 'api/Nycflights/EWR_Celsius_temp').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsEWR.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<Date, number>>(this.baseUrl + 'api/Nycflights/LGA_Celsius_temp').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsLGA.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));
  }

  loadJFK_Celsius_temp() {
    let dataPointsJFK = [];

    let chart = new CanvasJS.Chart("chartContainer7", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Temperatures registered for JFK",
        fontSize: 20
      },
      axisX: {
        title: "Date",
      },
      axisY: {
        title: "Temperature in Celsius"
      },
      data: [{
        type: "scatter",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      }]
    });

    chart.render();

    this.http.get<Map<Date, number>>(this.baseUrl + 'api/Nycflights/JFK_Celsius_temp').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));
  }

  loadJFK_DailyMean_CelTemp() {
    let dataPointsJFK = [];

    let chart = new CanvasJS.Chart("chartContainer8", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Daily mean temperature for JFK",
        fontSize: 20
      },
      axisX: {
        title: "Date",
      },
      axisY: {
        title: "Mean temperature"
      },
      data: [{
        type: "scatter",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      }]
    });

    chart.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/JFK_DailyMean_CelTemp').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));
  }

  loadDailyMeanTemperatureForOrigins() {
    let dataPointsJFK = [];
    let dataPointsEWR = [];
    let dataPointsLGA = [];

    let chart = new CanvasJS.Chart("chartContainer9", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Daily mean temperature for origins",
        fontSize: 20
      },
      axisX: {
        title: "Date",
      },
      axisY: {
        title: "Mean temperature"
      },
      data: [{
        type: "scatter",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "scatter",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "scatter",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    chart.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/JFK_DailyMean_CelTemp').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/EWR_DailyMean_CelTemp').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsEWR.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/LGA_DailyMean_CelTemp').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsLGA.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));
  }
}

