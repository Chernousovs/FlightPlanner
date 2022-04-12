import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { AdminFlightService } from 'src/app/services/admin-flight.service';
import { Flight } from 'src/app/shared/models/flights.model';

@Component({
  selector: 'app-flight-find',
  templateUrl: './flight-find.component.html',
  styleUrls: ['./flight-find.component.scss']
})
export class FlightFindComponent implements OnInit {
  findFlightForm = new FormGroup({});
  flight$?: Observable<Flight>;

  constructor(private fb: FormBuilder, private adminFlightService: AdminFlightService) { }

  ngOnInit(): void {
    this.buildForm();
  }

  buildForm(): void {
    this.findFlightForm = this.fb.group({
      id: ''
    })
  }

  submit(): void {
    this.flight$ = this.adminFlightService.findFlight(this.findFlightForm.value.id);
  }

  delete(): void {
    this.adminFlightService.deleteFlight(this.findFlightForm.value.id);
  }
}
