using AutoMapper;
using GroupFourLibrary.Models;
using GroupFourLibrary.DTO;
namespace GroupFourLibrary
{
    public class MappingProfile:Profile
    {
       public MappingProfile()
        {
            CreateMap<BookCopy,BookCopyDTO>();
            CreateMap<BookCopyDTO, BookCopy>();
            CreateMap<BookStoreDTO, BookStore>();
            CreateMap<BookStore, BookStoreDTO>();
            CreateMap<BookDTO, Book>();
            CreateMap<Book, BookDTO>();
            CreateMap<Reservation, ReservationDTO>();
            CreateMap<ReservationDTO, Reservation>();

        }
    }
}
