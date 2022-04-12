import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable, pluck } from "rxjs";
import { environment } from "src/environments/environment";
import { Flight, SearchFlightQuery } from "../shared/models/flights.model";

@Injectable ({
  providedIn: 'root'
})
export class FlightService {

  constructor(private http: HttpClient) {  }

  searchFlights(query: SearchFlightQuery): Observable<Flight[]> {
    const url = [
      environment.baseUrl,
      'api/flights/search'
    ].join('');

    return this.http.post<any>(url, query).pipe(pluck('items'));
  }
}
