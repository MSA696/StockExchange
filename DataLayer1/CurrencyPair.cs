using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer1
{
    public class CurrencyPair
    {
        [Key]
        public int PairId { get; set; }
        //[ForeignKey("Currency1")]
        public int CurrencyId1 { get; set; }
        //[ForeignKey("Currency2")]
        public int CurrencyId2 { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }

        // Navigation properties for relationships (optional)
        //public Currency Currency1 { get; set; }
        //public Currency Currency2 { get; set; }
    }

}
