export interface SearchFlightQuery {
  from: string;
  to: string;
  departureDate: string;
}

export interface Flight {
  from: Airport;
  to: Airport;
  carrier: string;
  departureTime: string;
  arrivalTime: string;
}

interface Airport {
    country: string;
    city: string;
    airport: string;
}
