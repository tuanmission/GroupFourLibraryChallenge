using System;
using System.Collections.Generic;

#nullable disable

namespace GroupFourLibrary.Models
{
    public partial class BookStore
    {
        public BookStore()
        {
            BookCopies = new HashSet<BookCopy>();
        }

        public int BookStoreId { get; set; }
        public string BookStoreAddress { get; set; }
        public int? BookStorePostCode { get; set; }
        public string BookStoreName { get; set; }

        public virtual ICollection<BookCopy> BookCopies { get; set; }
    }
}
