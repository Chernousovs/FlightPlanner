import { Component, Input } from '@angular/core';
import { Flight } from 'src/app/shared/models/flights.model';

@Component({
  selector: 'app-flight-card',
  templateUrl: './flight-card.component.html',
  styleUrls: ['./flight-card.component.scss']
})
export class FlightCardComponent{
  @Input()
  flight?: Flight;
}
