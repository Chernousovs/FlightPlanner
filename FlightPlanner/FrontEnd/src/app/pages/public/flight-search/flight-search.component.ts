import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { FlightService } from 'src/app/services/flight.service';
import { Flight, SearchFlightQuery } from 'src/app/shared/models/flights.model';

@Component({
  selector: 'app-flight-search',
  templateUrl: './flight-search.component.html',
  styleUrls: ['./flight-search.component.scss']
})
export class FlightSearchComponent {

  flights$?: Observable<Flight[]>;

  constructor( private flightService: FlightService) { }

  submitForm(query: SearchFlightQuery): void {
    this.flights$ = this.flightService.searchFlights(query);
  }
}
