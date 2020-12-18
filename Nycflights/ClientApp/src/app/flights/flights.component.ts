import { Component, AfterViewInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import * as CanvasJS from '../../assets/canvasjs.min.js';
import { Pipe, PipeTransform } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './flights.component.html'
})

export class FlightsComponent implements AfterViewInit {

  public http: HttpClient;
  public baseUrl: string;

  public topTenDestFlights: Map<string, number>;
  public Origins_MeanAirtime: Map<string, string>;
  public JFK_Mean_Dep_Arr_delay: Map<string, string>;
  public EWR_Mean_Dep_Arr_delay: Map<string, string>;
  public LGA_Mean_Dep_Arr_delay: Map<string, string>;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngAfterViewInit() {

    // Feature 1 - Flights per month
    this.loadMonthlyFlight();

    // Feature 2 - Flights per month from origins
    this.loadFlightsPerMonthFromOrigins();

    // Feature 3 - Flights to top 10 destinations
    this.loadTopTenDestFlights();

    // Feature 4 - Mean airtime for origins
    this.loadOrigins_MeanAirtime();

    // Feature 10 - Departure and arrival delays for origins
    this.loadDepartureAndArrivalDelaysForOrigins();
  }

  loadMonthlyFlight() {
    let dataPoints = [];

    let chart = new CanvasJS.Chart("chartContainer1", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Flights per month",
        fontSize: 20,
      },
      axisX: {
        title: "Month",
        interval: 1
      },
      axisY: {
        title: "Number of flights",
        fontSize: 25
      },
      data: [{
        type: "column",
        dataPoints: dataPoints,
        color: "#c12e2e"
      }]
    });
    chart.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/monthlyFlight').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPoints.push({ label: key, y: result[key] });
      });
      chart.render();
    }, error => console.error(error));
  }

  loadFlightsPerMonthFromOrigins() {
    let dataPointsJFK = [];
    let dataPointsEWR = [];
    let dataPointsLGA = [];

    let chartFrequency = new CanvasJS.Chart("chartContainer2Frequency", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Number of flights per month - frequency",
        fontSize: 20
      },
      axisX: {
        title: "Month",
        interval: 1
      },
      axisY: {
        title: "Number of flights",
        fontSize: 20
      },
      data: [{
        type: "column",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "column",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "column",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    let chartStacked = new CanvasJS.Chart("chartContainer2Stacked", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Number of flights per month - stacked",
        fontSize: 20
      },
      axisX: {
        title: "Month",
        interval: 1
      },
      axisY: {
        title: "Number of flights",
        fontSize: 20
      },
      data: [{
        type: "stackedColumn",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "stackedColumn",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "stackedColumn",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    let chartStackedPercentage = new CanvasJS.Chart("chartContainer2StackedPercentage", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Number of flights per month - stacked percentage",
        fontSize: 20
      },
      axisX: {
        title: "Month",
        interval: 1
      },
      axisY: {
        title: "Number of flights",
        fontSize: 20,
        suffix: "%"
      },
      data: [{
        type: "stackedColumn100",
        legendText: "JFK",
        toolTipContent: "{y} (#percent%)",
        showInLegend: true,
        dataPoints: dataPointsJFK,
        color: "#2E86C1"
      },
      {
        type: "stackedColumn100",
        legendText: "EWR",
        toolTipContent: "{y} (#percent%)",
        showInLegend: true,
        dataPoints: dataPointsEWR,
        color: "#C13B2E"
      },
      {
        type: "stackedColumn100",
        legendText: "LGA",
        toolTipContent: "{y} (#percent%)",
        showInLegend: true,
        dataPoints: dataPointsLGA,
        color: "#2EC146"
      },
      ]
    });

    chartFrequency.render();
    chartStacked.render();
    chartStackedPercentage.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/JFK_MonthlyFlights').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFK.push({ label: key, y: result[key] });
      });

      chartFrequency.render();
      chartStacked.render();
      chartStackedPercentage.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/EWR_MonthlyFlights').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsEWR.push({ label: key, y: result[key] });
      });

      chartFrequency.render();
      chartStacked.render();
      chartStackedPercentage.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/LGA_MonthlyFligthts').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsLGA.push({ label: key, y: result[key] });
      });

      chartFrequency.render();
      chartStacked.render();
      chartStackedPercentage.render();
    }, error => console.error(error));
  }

  loadTopTenDestFlights() {

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/TopTenDestFlights').subscribe(result => {
      this.topTenDestFlights = result;
    }, error => console.error(error));

    let dataPointsJFKToDestinations = [];
    let dataPointsEWRToDestinations = [];
    let dataPointsLGAToDestinations = [];

    let chart = new CanvasJS.Chart("chartContainer3", {
      animationEnabled: true,
      backgroundColor: false,
      title: {
        text: "Number of flights to top 10 destinations from origins",
        fontSize: 20
      },
      axisX: {
        title: "Destination",
        interval: 1
      },
      axisY: {
        title: "Number of flights"
      },
      data: [{
        type: "column",
        legendText: "JFK",
        showInLegend: true,
        dataPoints: dataPointsJFKToDestinations,
        color: "#2E86C1"
      },
      {
        type: "column",
        legendText: "EWR",
        showInLegend: true,
        dataPoints: dataPointsEWRToDestinations,
        color: "#C13B2E"
      },
      {
        type: "column",
        legendText: "LGA",
        showInLegend: true,
        dataPoints: dataPointsLGAToDestinations,
        color: "#2EC146"
      },
      ]
    });
    chart.render();

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/TopTenDestFlights_from_JFK').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsJFKToDestinations.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));

    this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/TopTenDestFlights_from_EWR').subscribe(result => {

      Object.keys(result).forEach(function (key) {
        dataPointsEWRToDestinations.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));

     this.http.get<Map<string, number>>(this.baseUrl + 'api/Nycflights/TopTenDestFlights_from_LGA').subscribe(result => {

       Object.keys(result).forEach(function (key) {
       dataPointsLGAToDestinations.push({ label: key, y: result[key] });
      });

      chart.render();
    }, error => console.error(error));
  }

  loadOrigins_MeanAirtime() {
    this.http.get<Map<string, string>>(this.baseUrl + 'api/Nycflights/Origins_MeanAirtime').subscribe(result => {
      this.Origins_MeanAirtime = result;
    }, error => console.error(error));
  }

  loadDepartureAndArrivalDelaysForOrigins() {
    this.http.get<Map<string, string>>(this.baseUrl + 'api/Nycflights/JFK_Mean_Dep_Arr_delay').subscribe(result => {
      this.JFK_Mean_Dep_Arr_delay = result;
    }, error => console.error(error));

    this.http.get<Map<string, string>>(this.baseUrl + 'api/Nycflights/EWR_Mean_Dep_Arr_delay').subscribe(result => {
      this.EWR_Mean_Dep_Arr_delay = result;
    }, error => console.error(error));

    this.http.get<Map<string, string>>(this.baseUrl + 'api/Nycflights/LGA_Mean_Dep_Arr_delay').subscribe(result => {
      this.LGA_Mean_Dep_Arr_delay = result;
    }, error => console.error(error));
  }

}

@Pipe({ name: 'getValues' })
export class GetValuesPipe implements PipeTransform {
  transform(map: Map<string, number>): any[] {
    let ret = [];

    Object.keys(map).forEach(function (key) {
      ret.push({ key: key, val: map[key] });
    });

    return ret;
  }
}


