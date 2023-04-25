﻿using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BUS
{
    public class BUS_Flight
    {
        DAL_Flight flights = new DAL_Flight();
        public List<Flight> GetListFlights() => flights.LoadFlights();

        public List<Location> GetLocations() => flights.LoadLocationsToBox();

        public bool AddFlights(Flight flight)
        {
            return flights.AddFlight(flight.PlaneID, flight.Departure, flight.Destination, flight.DateOfDeparture, flight.Airline);
        }

        public bool UpdateFlights(Flight flight)
        {
            if (flights.checkValidFlight(flight))
            {
                return flights.UpdateFlight(flight.FlightID, flight.PlaneID, flight.Departure, flight.Destination,
                    flight.DateOfDeparture, flight.Airline);
            }
            else
            {
                throw new Exception("Invalid plane registration detected!!!");
            }
        }

        public bool DeleteFlights(Flight flight) => flights.RemoveFlight(flight.FlightID);
    }
}