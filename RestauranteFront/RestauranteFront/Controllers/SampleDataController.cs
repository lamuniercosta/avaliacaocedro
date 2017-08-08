using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteFront.Controllers
{
    [Route("api/[controller]/{id}")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Frio","Calor"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = index,
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
        [HttpGet]
        public WeatherForecast GetWeatherForecast(int id)
        {
            return this.WeatherForecasts().Where(w => w.Id == id).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Add([FromBody]WeatherForecast model)
        {
            try
            {
                Summaries.Append(model.DateFormatted);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok("Ok");
        }


        public class WeatherForecast
        {
            public int Id { get; set; }
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
