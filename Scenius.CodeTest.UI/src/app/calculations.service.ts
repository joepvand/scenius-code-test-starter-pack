import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map, observable } from 'rxjs';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class CalculationsService {
  private hubConnection: signalR.HubConnection;

  private baseURL = `http://localhost:5000`;
  public backingList$: Array<any> = [];
  


  constructor(private http: HttpClient) {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.baseURL + "/calculationsHub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        withCredentials: true // include credentials in requests if necessary
      })
    
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR connection started.'))
      .catch(err => console.error('Error while starting SignalR connection:', err));
  };

  public addDataListener = () => {
    this.hubConnection.on('ReceiveMessage', (data) => {
      console.log('Data received from SignalR:', data);
      this.backingList$[0].push(data);
      console.log(this.backingList$);
    });
  };
  


  getAllData() {
    this.http.get<any[]>(`${this.baseURL}` + '/api/calculations').forEach(v => this.backingList$.push(v));

    console.log(this.backingList$);
  }

  postData(data: any): Observable<any> {
    return this.http.post(`${this.baseURL}` + '/api/calculations', data);
  }
}
