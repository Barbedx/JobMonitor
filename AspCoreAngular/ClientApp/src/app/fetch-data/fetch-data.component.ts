import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
import { Data } from '@angular/router';
import { UserService } from '../shared/_services';
import { first } from 'rxjs/operators';


@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit {
  public jobsList: RootObject[];
  public servers: string[];
  public lastUpdate : Date
  @Inject('BASE_URL') baseUrl: string;
  //private _hubConnection: HubConnection;
  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<WeatherForecast[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
  //}

  constructor(private userService: UserService) { }
  ngOnInit(): void { 
    this.userService.getAllServers()
      .pipe(first())
      .subscribe(servers => { this.servers = servers })
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
