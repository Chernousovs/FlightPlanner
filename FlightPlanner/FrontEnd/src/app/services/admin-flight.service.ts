import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, Observable, pipe, throwError } from "rxjs";
import { environment } from "src/environments/environment";
import { Flight } from "../shared/models/flights.model";
import { ToastrService } from "ngx-toastr";

@Injectable ({
  providedIn: 'root'
})
export class AdminFlightService {

  constructor(private http: HttpClient, private toastr: ToastrService) {  }

  addFlight(query: any): Observable<Flight> {
    const url = [
      environment.baseUrl,
      'admin-api/flights/'
    ].join('');

    const options = { headers: { 'Add-Auth-Data': '' }, withCredentials: true };

    return this.http.put<Flight>(url, query, options).pipe(map(flight => {
        this.toastr.success('Flight successfully added!')
        return flight
    }));
  }

  findFlight(id: number): Observable<Flight> {
    const url = [
      environment.baseUrl,
      'admin-api/flights/',
      id
    ].join('');

    const options = { headers: { 'Add-Auth-Data': '' }, withCredentials: true };

        return this.http.get<Flight>(url, options).pipe(map(flight => {
        this.toastr.success('Flight successfully found!');
        console.log(flight);
        return flight
      }));
  }

  deleteFlight(id: number): void {
    const url = [
      environment.baseUrl,
      'admin-api/flights/',
      id
    ].join('');

    const options = { headers: { 'Add-Auth-Data': '' }, withCredentials: true };

    this.http.delete(url, options).subscribe();
  }
}
