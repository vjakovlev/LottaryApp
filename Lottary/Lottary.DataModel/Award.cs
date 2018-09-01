using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lottary.DataModel
{
    [Table("Awards")]
    public class Award : IEntity
    {
        [Key]
        [Column("AwardID", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string AwardName { get; set; }
        public string AwardDescription { get; set; }
        public int Quantity { get; set; }
        public byte RuffleType { get; set; } //Enum values: Immidiate/PerDay/Final
    }
}
