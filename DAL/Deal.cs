using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Index(nameof(DealNumber), IsUnique = true)]
    public class Deal
    {
        public int DealID { get; set; }

        [ForeignKey("FK_DealName")]
        [Column(TypeName = "numeric(50)")]
        public decimal  DealNumber { get; set; }

        [Column(TypeName = "jsonb")]
        public string Data { get; set; }
    }
}
