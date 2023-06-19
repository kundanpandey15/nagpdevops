using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace climateapi
{
    public class WeatherForecast
    {
        [JsonIgnore]
        public ObjectId _id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF { get; set; }

        public string? Summary { get; set; }
    }
}

//{"date":"2023-06-17","temperatureC":-12,"temperatureF":11,"summary":"Hot"}