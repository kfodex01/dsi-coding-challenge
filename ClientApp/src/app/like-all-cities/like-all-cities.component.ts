import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'like-all-cities',
  templateUrl: './like-all-cities.component.html'
})
export class LikeAllCitiesComponent {
  public geoName: GeoName[];
  public baseUrl: string;
  public http: HttpClient;
  public likeCity: string = '';
  public inputLikeCity: string = '';

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
    this.http = http;
  }

  onSubmit() {
    if (this.likeCity == '') {
      return;
    }
    this.http.get<GeoName[]>(this.baseUrl + 'all-cities?like=' + this.likeCity).subscribe(result => {
      this.geoName = result;
      this.inputLikeCity = this.likeCity;
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