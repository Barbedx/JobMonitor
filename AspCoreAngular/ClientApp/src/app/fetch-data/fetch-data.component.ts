import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
import { Data } from '@angular/router';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public jobsList: RootObject[];
  public lastUpdate : Date
  @Inject('BASE_URL') baseUrl: string;
  private _hubConnection: HubConnection;
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
  //}
  ngOnInit(): void {
    //this._hubConnection = new HubConnectionBuilder()
    //  .withUrl('http://sqljobmonitor.azurewebsites.net/jobhub'  )
    //  .configureLogging(LogLevel.Trace)
    //  .build();

    //this._hubConnection
    //  .start()
    //  .then(() => console.log('Connection started!'))
    //  .catch(err => console.log('Error while establishing connection :(' + err.error + ')'));
    //this._hubConnection.on('UpdateData', (sender,datetime,jobs) => {
    //  this.jobsList = JSON.parse(jobs) as RootObject[];
    //  console.log(this.jobsList);
    //  this.lastUpdate = datetime;
    //});
  }


}
export interface CurentExecutionStatus {
}
export interface RootObject {
  Name: string;
  ServerName: string;
  LastRunDate: Date;
  CurentExecutionStatus: CurentExecutionStatus;
  CurentExecutionStep: string;
  LastRunOutcome: number;
  LastOutcomeMessage: string;
  CurentRetryAttempt: number;
  NextRunDate: Date;
  Enable: boolean;
}
interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
