using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic.Api.DTOS
{
    public class ClientCreate
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string RFC { get; set; }
    }
}
