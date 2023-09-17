using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataScraper
{
    public class moviedata
    {
        [Key]
        public string? id {  get; set; }
        public string? name { get; set; }
        public string? score { get; set; }
        public string? time { get; set; }
        public string? views { get; set; } = "0";
        public string? imgurl { get; set; }
    }

    public class movielink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? movieid { get; set; }
        public string? fsize { get; set; }
        public string? quality { get; set; }
        public string? language { get; set; }
        public string? seed { get; set; }
        public string? leech { get; set; }
        public string? magnet { get; set; }
    }
}
