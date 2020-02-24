using System;
using System.Security.Cryptography;

namespace Z05.Models
{
    public class Filter
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? Category { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public Filter()
        {
            
        }
    }
}