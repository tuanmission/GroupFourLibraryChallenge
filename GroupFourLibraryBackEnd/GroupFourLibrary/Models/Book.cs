using System;
using System.Collections.Generic;

#nullable disable

namespace GroupFourLibrary.Models
{
    public partial class Book
    {
        public Book()
        {
            BookCopies = new HashSet<BookCopy>();
        }

        public int BookKeyId { get; set; }
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }

        public virtual ICollection<BookCopy> BookCopies { get; set; }
    }
}
