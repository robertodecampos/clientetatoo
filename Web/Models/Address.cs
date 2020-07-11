using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatooReport.Models
{
    public class Address : IDisposable
    {
        public string PostalCode { get; set; }
        public string StreetType { get; set; }
        public string Street { get; set; }
        public string Complement { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public void Dispose() { }
    }
}
