using Refit;

using RefitSamples.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace RefitSamples.SharedApi
{
    public interface IAppUserApi
    {
        [Get("/api/AppUsers")]
        IObservable<List<AppUser>> Get(); 
    }
}
