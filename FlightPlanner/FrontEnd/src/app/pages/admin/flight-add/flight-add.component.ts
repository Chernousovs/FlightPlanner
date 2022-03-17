import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AdminFlightService } from 'src/app/services/admin-flight.service';

@Component({
  selector: 'app-flight-add',
  templateUrl: './flight-add.component.html',
  styleUrls: ['./flight-add.component.scss']
})
export class FlightAddComponent implements OnInit, OnDestroy {
  flightForm = new FormGroup({});

  flightAddSubsciption$?: Subscription;

  constructor(private fb: FormBuilder, private adminFlightsService: AdminFlightService) { }

  ngOnInit(): void {
    this.buildForm();
  }

  ngOnDestroy(): void {
      this.flightAddSubsciption$?.unsubscribe();
  }

  buildForm():void {

    this.flightForm = this.fb.group({
      from: this.fb.group({
        country: ['', Validators.required],
        city: ['', Validators.required],
        airport: ['', Validators.required]
      }),

      to: this.fb.group({
        country: ['', Validators.required],
        city: ['', Validators.required],
        airport: ['', Validators.required]
      }),

      carrier: ['', Validators.required],
      departureTime: ['', Validators.required],
      arrivalTime: ['', Validators.required]
    })
  }

  submitForm(): void {
    if (this.flightForm.valid) {
      this.flightAddSubsciption$ = this.adminFlightsService.addFlight(this.flightForm.value).subscribe();
    }
  }
}
