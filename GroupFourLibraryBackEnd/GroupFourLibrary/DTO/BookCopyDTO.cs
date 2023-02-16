using System;
using System.Collections.Generic;
using GroupFourLibrary.Models;


namespace GroupFourLibrary.DTO
{
    public class BookCopyDTO
    {

        public int BookCopyId { get; set; }
        public int? BookId { get; set; }
        public int? BookStoreId { get; set; }
        public int? CopiesAvailable { get; set; }
        public int? TotalCopies { get; set; }

        public virtual Book Book { get; set; }
        public virtual BookStore BookStore { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
