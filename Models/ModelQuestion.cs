using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ModelQuestion
    {
        public int id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public string Operator { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
       [ForeignKey("FlashCard")]
        public int? FlashCardID { get; set; } 
        [NotMapped]
        public virtual FlashCard FlashCard { get; set; }
    }
}
