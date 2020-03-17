import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-city.component.html'
})
export class FetchCityComponent {
  public geoName: GeoName;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<GeoName>(baseUrl + 'cities/Des Moines').subscribe(result => {
      this.geoName = result;
      console.log(result);
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
