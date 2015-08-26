using System.Collections.Generic;
using DotLiquid;

namespace NancyDemo.Owin.Models
{
    public class Dude : Drop
    {
        public string Name { get; set; }
        public IList<string> Things { get; set; }

        public Dude()
        {
            Things = new List<string>();
        }
    }
}