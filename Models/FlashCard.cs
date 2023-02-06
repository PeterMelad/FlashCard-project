using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class FlashCard
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Chapter number")]
        public int ChapterNumber { get; set; }
        public string SubjectName { get; set; }
        [Required(ErrorMessage ="Enter the date")]
        public DateTime Date { get; set; }
        public virtual IEnumerable<ModelQuestion> Questions { get; set; } = new HashSet<ModelQuestion>();
    }

}
        
 