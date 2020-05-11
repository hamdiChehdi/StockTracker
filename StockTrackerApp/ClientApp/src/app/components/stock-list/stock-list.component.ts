import { Component, OnInit } from '@angular/core';
import { Stock } from 'src/app/models/stock';
import { StockService } from 'src/app/services/stock.service';
import { PriceService } from 'src/app/services/price.service';
import { LastPrice } from 'src/app/models/lastPrice';
import {plainToClass} from 'class-transformer';

@Component({
  selector: 'app-stock-list',
  templateUrl: './stock-list.component.html',
  styleUrls: ['./stock-list.component.css']
})
export class StockListComponent implements OnInit {

  constructor(private stockService: StockService,
    private priceService: PriceService) {
  }

  stocks: Stock[] = [];

  ngOnInit(): void {
    this.loadStocks();

    this.priceService.addPriceUpdateDataListener();

    this.priceService.priceReceived.subscribe((data: LastPrice) => {
      this.updateStocks(data);
    });
  }

  loadStocks(): void {
    this.stockService.get().subscribe((data: Stock[]) => {
      this.stocks = plainToClass(Stock, data);
    }, error => {
      console.log('Error in load stocks ! ');
    });
  }

  updateStocks(data: LastPrice): void {
    const index = this.stocks.findIndex((stock) => stock.symbol === data.symbol);
    this.stocks[index].SetNewPrice(data.price);
  }

}
