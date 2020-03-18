import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'get-all-cities',
  templateUrl: './get-all-cities.component.html'
})
export class GetAllCitiesComponent {
  public geoName: GeoName[];
  public baseUrl: string;
  public http: HttpClient;
  public city: string = '';
  public inputCity: string = '';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  onSubmit() {
    this.http.get<GeoName[]>(this.baseUrl + 'all-cities/' + this.city).subscribe(result => {
      this.geoName = result;
      this.inputCity = this.city;
    }, error => console.error(error));
  }
}

interface GeoName {
  city: string;
  state: string;
  country: string;
  alternativeNames: string[];
  latitude: number;
  longitude: number;
}