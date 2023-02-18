using System;
namespace GroupFourLibrary.ViewModels
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }

        public string ReservationNumber { get; set; }
        public int? BookCopyId { get; set; }

        public string BookTitle { get; set; }

        public string author { get; set; }
        public string publisher { get; set; }
        public string DateReservedString { get; set; }

        public string DateReturnedString { get; set; }

        public DateTime? DateReserved { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
