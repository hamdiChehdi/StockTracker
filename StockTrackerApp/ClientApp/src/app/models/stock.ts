export class Stock {
  symbol: string;

  name: string;

  price: number;

  exchange: string;

  latestPrice: number;

  SetNewPrice(newPrice) {
    if (typeof this.latestPrice !== 'undefined') {
      this.price = this.latestPrice;
      this.latestPrice = newPrice;
    } else {
      this.latestPrice = newPrice;
    }
  }
}
