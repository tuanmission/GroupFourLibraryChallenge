using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;




#nullable disable

namespace GroupFourLibrary.Models
{
    public partial class Reservation
    {
        
        
        public int ReservationId { get; set; }
        public int? BookCopyId { get; set; }
        public DateTime? DateReserved { get; set; }
        public DateTime? DateReturned { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId)) ]
        public virtual IdentityUser user { get; set; }
        public virtual BookCopy BookCopy { get; set; }
    }
}
