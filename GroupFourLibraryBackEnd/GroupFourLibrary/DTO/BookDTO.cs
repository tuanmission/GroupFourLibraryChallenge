using System.Collections.Generic;
using GroupFourLibrary.Models;
namespace GroupFourLibrary.DTO
{
    public class BookDTO
    {
        public int BookKeyId { get; set; }
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }

        public virtual ICollection<BookCopy> BookCopies { get; set; }
    }
}
