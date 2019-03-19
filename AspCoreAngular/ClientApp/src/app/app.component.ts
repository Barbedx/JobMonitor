import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel} from '@aspnet/signalr'

import { Message } from 'primeng/api'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  private _hubConnection: HubConnection;
  msgs: Message[] = [];
  constructor() { }
  ngOnInit(): void {
    //this._hubConnection = new HubConnectionBuilder()
    //  .withUrl('http://localhost:50508/hub')
    //  .configureLogging(LogLevel.Trace)
    //  .build();

    //this._hubConnection
    //  .start()
    //  .then(() => console.log('Connection started!'))
    //  .catch(err => console.log('Error while establishing connection :('+err.error+')'));


    //this._hubConnection.on('SendMessage', (user: string, text: string) => {
    //  this.msgs.push({ severity: user, closable: true,  summary: text });
    //});
  }
}

