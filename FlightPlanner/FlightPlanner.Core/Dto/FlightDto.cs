namespace FlightPlanner.Core.Dto
{
    public class FlightDto
    {
        public int Id { get; set; }
        public AirportDto From { get; set; }
        public AirportDto To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}