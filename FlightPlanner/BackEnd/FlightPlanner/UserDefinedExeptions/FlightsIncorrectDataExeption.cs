using System;

namespace FlightPlanner.UserDefinedExeptions
{
    public class FlightsIncorrectDataException : Exception
    {
        public FlightsIncorrectDataException() { }

        public FlightsIncorrectDataException(string message) : base(message)
        {

        }

        public FlightsIncorrectDataException(string message, FlightsIncorrectDataException inner) : base(message, inner)
        {

        }
    }
}