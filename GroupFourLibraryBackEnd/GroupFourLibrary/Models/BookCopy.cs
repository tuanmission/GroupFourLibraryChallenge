using System;
using System.Collections.Generic;

#nullable disable

namespace GroupFourLibrary.Models
{
    public partial class BookCopy
    {
        public BookCopy()
        {
            Reservations = new HashSet<Reservation>();
        }

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
