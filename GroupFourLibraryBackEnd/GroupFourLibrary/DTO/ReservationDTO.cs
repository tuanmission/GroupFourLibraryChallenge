using System;
using Microsoft.AspNetCore.Identity;
using GroupFourLibrary.Models;
namespace GroupFourLibrary.DTO
{
    public class ReservationDTO
    {

        public int ReservationId { get; set; }
        public int? BookCopyId { get; set; }
        public DateTime? DateReserved { get; set; }
        public DateTime? DateReturned { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser user { get; set; }
        public virtual BookCopy BookCopy { get; set; }
    }
}
