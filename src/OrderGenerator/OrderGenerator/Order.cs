using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderGenerator
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]
        public string OrderId { get; set; }
        public string Item { get; set; }
        public double Price { get; set; }
    }
}
