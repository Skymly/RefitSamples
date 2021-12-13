using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace RefitSamples.Models
{

    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class HelloModelCreateOrUpdateDto : HelloModel
    {


    }
}
