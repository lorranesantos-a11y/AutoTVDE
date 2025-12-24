using AutoTvde.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTvde.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Nif { get; set; } = default!;
        public DateTime BirthDate { get; set; }
    }
}
