import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  public title: string = 'Online Shop';
  private url: string = 'https://localhost:5001/api/categories';

  public categories!: any;

  /**
   *
   */
  constructor(private httpClient: HttpClient) {
    this.getCategories().subscribe((x) => {
      this.categories = x;
    });
  }

  private getCategories() {
    return this.httpClient.get(this.url);
  }
}
