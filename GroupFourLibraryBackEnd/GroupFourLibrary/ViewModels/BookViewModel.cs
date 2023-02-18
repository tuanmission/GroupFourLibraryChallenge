namespace GroupFourLibrary.ViewModels
{
    public class BookViewModel
    {
        public string BookTitle { get; set; }
        public int BookStoreId { get; set; }

        public string BookStoreName { get; set; }

        public int TotalCopies { get; set; }

        public int CopiesAvailable { get; set; }    

        public int BookCopyId { get; set; }

    }
}
