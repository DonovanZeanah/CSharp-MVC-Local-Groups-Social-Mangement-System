using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WorkshopGroup.Models;

public class FlightSearchModel
{
    public FlightSearchModel(string? origin, string destination, string departureDate, int adults, string carrier,
        string departureTime, string arrivalTime, int price, string name, string country, string returnDate)
    {
        Origin = origin;
        Destination = destination;
        DepartureDate = departureDate;
        ReturnDate = returnDate;
        Adults = adults;
        Carrier = carrier;
        DepartureTime = departureTime;
        ArrivalTime = arrivalTime;
        Price = price;
        Name = name;
        Country = country;
    }

    public FlightSearchModel()
    {
    }
    public string ReturnDate { get; set; }

    public string Name { get; set; }
    public string Country { get; set; }

    public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public int Adults { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public string Id { get; set; }
}
