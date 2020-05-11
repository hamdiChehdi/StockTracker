import { Component, Input } from '@angular/core';
import { Stock } from 'src/app/models/stock';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent {

  constructor() { }

  @Input() stockData: Stock;

  get HasLastPrice(): boolean {
    return typeof this.stockData.latestPrice !== 'undefined';
  }

  get CurrentPrice(): number {
    return this.HasLastPrice ? this.stockData.latestPrice : this.stockData.price;
  }

  get IsIncreased(): boolean {
    return this.stockData.latestPrice > this.stockData.price;
  }

  get IsDecreased(): boolean {
    return this.stockData.latestPrice < this.stockData.price;
  }

  get IsIdle(): boolean {
    return !this.HasLastPrice || this.stockData.latestPrice === this.stockData.price;
  }
}
