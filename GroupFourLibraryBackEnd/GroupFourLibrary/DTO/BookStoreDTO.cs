using System.Collections.Generic;
using GroupFourLibrary.Models;

namespace GroupFourLibrary.DTO
{
    public class BookStoreDTO
    {
        public int BookStoreId { get; set; }
        public string BookStoreAddress { get; set; }
        public int? BookStorePostCode { get; set; }
        public string BookStoreName { get; set; }

        public virtual ICollection<BookCopy> BookCopies { get; set; }
    }
}
