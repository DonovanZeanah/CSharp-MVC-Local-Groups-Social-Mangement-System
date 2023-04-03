using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WorkshopGroup.Models
{
    public class FlightsResponse
    {
        public string Search_id { get; set; }
        public string Currency { get; set; }
        public decimal Fx_rate { get; set; }
        public List<Item> Data { get; set; }
    }

    public class Item
    {
        public string Id { get; set; }
        public string Fly_from { get; set; }
        public string Fly_to { get; set; }
        public string City_from { get; set; }
        public string City_code_from { get; set; }
        public string City_to { get; set; }
        public string City_code_to { get; set; }
        public Country Country_from { get; set; }
        public Country Country_to { get; set; }
        public int? Nights_in_dest { get; set; }
        public decimal? Quality { get; set; }
        public decimal? Distance { get; set; }
        public Duration Duration { get; set; }
        public decimal? Price { get; set; }
        public Conversion Conversion { get; set; }
        public BagsPrice Bags_price { get; set; }
        public Baglimit Bag_limit { get; set; }
        public Availability Availability { get; set; }
        public List<string> Airlines { get; set; }
        public List<Route> Route { get; set; }
    }

    /*public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }*/

    public class Duration
    {
        public int Departure { get; set; }
        public int Return { get; set; }
        public int Total { get; set; }
    }

    public class Conversion
    {
        public decimal EUR { get; set; }
    }

    public class BagsPrice
    {
        public decimal _1 { get; set; }
        public decimal _2 { get; set; }
    }

    public class Baglimit
    {
        public int Hand_height { get; set; }
        public int Hand_length { get; set; }
        public int Hand_weight { get; set; }
        public int Hand_width { get; set; }
        public int Hold_dimensions_sum { get; set; }
        public int Hold_height { get; set; }
        public int Hold_length { get; set; }
        public int Hold_weight { get; set; }
        public int Hold_width { get; set; }
        public int Personal_item_height { get; set; }
        public int Personal_item_length { get; set; }
        public int Personal_item_weight { get; set; }
        public int Personal_item_width { get; set; }
    }

    public class Availability
    {
        public int? Seats { get; set; }
    }

    public class Route
    {
        public string Id { get; set; }
        public string Combination_id { get; set; }
        public string FlyFrom { get; set; }
        public string FlyTo { get; set; }
        public string CityFrom { get; set; }
        public string CityCodeFrom { get; set; }
        public string CityTo { get; set; }
        public string CityCodeTo { get; set; }
        public string Airline { get; set; }
        public int Flight_no { get; set; }
        public string Operating_carrier { get; set; }
        public string Operatng_flight_no { get; set; }
        public string Fare_basis { get; set; }
        public string Fare_category { get; set; }
        public string Fare_classes { get; set; }
        public string Fare_family { get; set; }
        public string Return { get; set; }
        public bool Bags_recheck_required { get; set; }
        public bool Vi_connection { get; set; }
        public bool Guarantee { get; set; }
        public bool? Equipment { get; set; }
    }
}
