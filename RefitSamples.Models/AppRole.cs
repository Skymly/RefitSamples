using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Text;

namespace RefitSamples.Models
{
    public class AppRole : IdentityRole<Guid>
    { 
    

    }

    //public class AppRole
    //{
    //    public string Name { get; set; }
    //    public bool IsDefault { get; set; }
    //    public bool IsStatic { get; set; }
    //    public bool IsPublic { get; set; }
    //    public DateTime? ConcurrencyStamp { get; set; }
    //    public Guid Id { get; set; }
    //    public Dictionary<string, string> ExtraProperties { get; set; }
    //}

}
