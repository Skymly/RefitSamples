using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Refit;

using RefitSamples.Models;

namespace RefitSamples.SharedApi
{
    public interface IWeatherApi
    {
        [Get("/WeatherForecast")]
        Task<IEnumerable<WeatherForecast>> Get();
    }
}
