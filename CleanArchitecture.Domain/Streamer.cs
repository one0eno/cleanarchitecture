using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public class Streamer: BaseDomainModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Url { get; set; } 

        public ICollection<Video>? Videos { get; set; }




    }
}
