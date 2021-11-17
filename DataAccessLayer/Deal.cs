using System;

namespace DataAccessLayer
{
    public class Deal
    {
        public int DealNumber { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column(TypeName = "jsonb")]
        public string Data { get; set; }
    }
}
