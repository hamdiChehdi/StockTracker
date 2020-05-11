import { Injectable, EventEmitter } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { LastPrice } from '../models/lastPrice';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class PriceService {
  constructor(private http: HttpClient) {
    this.startConnection();
  }

  priceReceived = new EventEmitter<LastPrice>();

  private hubConnection: HubConnection;

  private startConnection = () => {
    this.hubConnection = new HubConnectionBuilder()
                            .withUrl('http://localhost:52540/PriceHub')
                            .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection to the Price update server : ' + err));
  }

  public addPriceUpdateDataListener = () => {
    this.hubConnection.on('PriceUpdated', (data: LastPrice) => {
      this.priceReceived.emit(data);
    });
  }
}
