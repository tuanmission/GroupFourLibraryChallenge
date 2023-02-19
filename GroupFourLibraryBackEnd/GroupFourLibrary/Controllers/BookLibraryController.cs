using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.SqlClient;
using GroupFourLibrary.Models;
using GroupFourLibrary.DTO;
using Microsoft.AspNetCore.Authorization;
using GroupFourLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace GroupFourLibrary.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class BookLibraryController : ControllerBase
    {
        private GroupFourLibraryContext ctxt;
        private readonly IMapper _mapper;
        private UserManager<IdentityUser> _usermgr;
        
        public BookLibraryController(GroupFourLibraryContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.ctxt = context;
            this._usermgr = userManager;
           
        }

        [HttpGet]
        [Route("booklist")]
        public IEnumerable<BookDTO> ListBooks()
        {
           List <BookDTO> books = new List <BookDTO>(); 
           using(SqlConnection conn = new SqlConnection(ctxt.Database.GetConnectionString()))
           {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetBookList",conn);
                cmd.CommandType = CommandType.StoredProcedure;
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    BookDTO bkDTO = new BookDTO
                    {
                        BookKeyId= result.GetInt32(0),
                        BookId = result.GetString(1),
                        BookTitle = result.GetString(2),
                        Author = result.GetString(3),
                        Publisher = result.GetString(4)
                    };

                    books.Add(bkDTO);
                }
                conn.Close();
                return books;

           }

        }

        [HttpGet]
        [Route("search/{type}/{query}")]
        public IEnumerable<BookDTO> searchBooks(string type, string query)
        {
            string storedProcedureName = "";
            string sqlParameterName = "";
            if (type == "bookId")
            {
                storedProcedureName = "SearchBookByBookId";
                sqlParameterName = "@BookId";
            }
            else
            {
                storedProcedureName = "SearchBookByTitle";
                sqlParameterName = "@Title";
            }

            List<BookDTO> books = new List<BookDTO>();
            using (SqlConnection conn = new SqlConnection(ctxt.Database.GetConnectionString()))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(storedProcedureName, conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter(sqlParameterName, query));
                var result = comm.ExecuteReader();
                while (result.Read())
                {
                    BookDTO bkDTO = new BookDTO
                    {
                        BookId = result.GetString(0),
                        BookTitle = result.GetString(1),
                        Author = result.GetString(2),
                        Publisher = result.GetString(3)
                    };

                    books.Add(bkDTO);
                }

                conn.Close();
                return books;
            }

          
        }

        [HttpGet]
        [Route("bookDetails/{bookKeyId}")]
        public IEnumerable<BookViewModel> getBookDetails(int bookKeyId)
        {
            List<BookViewModel> books = new List<BookViewModel>();
            using (SqlConnection conn = new SqlConnection(ctxt.Database.GetConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetBooksAvailable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BookKeyId", bookKeyId));
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    BookViewModel bkMdl = new BookViewModel
                    {

                        BookTitle= result.GetString(0),
                        BookStoreId = result.GetInt32(1),
                        BookStoreName = result.GetString(2),
                        TotalCopies = result.GetInt32(3),
                        CopiesAvailable = result.GetInt32(4),
                        BookCopyId = result.GetInt32(5)
                    };

                    books.Add( bkMdl);
                }
                conn.Close();
                return books;

            }
        }

        [HttpGet]
        [Route("myreservations/{UserName}")]
        [Authorize]
        public async Task<IActionResult> myReservations(string UserName)
        {
           
            var user = await _usermgr.FindByNameAsync(UserName);
            if (user == null)
            {
                return BadRequest("User not found, server error");
            }

            string userId = user.Id;

            List <ReservationListViewModel> reservations = new List<ReservationListViewModel>();
            using(SqlConnection conn = new SqlConnection(ctxt.Database.GetConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("MyReservations", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userId", userId));
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    string returnedDate = "NOT RETURNED";
                    DateTime ReserveRetrieve = result.GetDateTime(4);
                    DateTime DueDateRetrieve = result.GetDateTime(5);
                    
                    if (!result.IsDBNull(6))
                    {
                        DateTime ReturnDate = result.GetDateTime(6);
                        returnedDate = ReturnDate.ToString("yyyy-MM-dd");
                    }
  
                    string retrieveDate = ReserveRetrieve.ToString("yyyy-MM-dd");
                    string dueDate = DueDateRetrieve.ToString("yyyy-MM-dd");

                    ReservationListViewModel rsViewMdl = new ReservationListViewModel
                    {
                        BookTitle = result.GetString(0),
                        Author = result.GetString(1),
                        Publisher = result.GetString(2),
                        ReservationNumber = result.GetString(3),
                        DateReserved = retrieveDate,
                        DueDate = dueDate,
                        DateReturned =returnedDate
                    };

                    reservations.Add(rsViewMdl);
                }
                conn.Close();
                return Ok(reservations);
            }


        }



        private string GenerateReservationNumber()
        {
            StringBuilder sb = new StringBuilder();
            char[] chars = {'1','2','3','4','5','6','7','8','9','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p',
            'q','r','s','t','u','v','w','x','y','z'};
            int charArrayLength = chars.Length;
            for(int i =0; i < 20; i++)
            {
                Random rdM = new Random();
                int charSelection = rdM.Next(0,charArrayLength);
                sb.Append(chars[charSelection]);
            }

            for(int s=8; s<17; s +=4)
            {
                sb.Insert(s,'-');
            }
            
            return sb.ToString();

        }

        [HttpPost]
        [Route("reserve")]
        [Authorize]
        public async Task<IActionResult> ReserveBook(createReservationViewModel rsViewModel)
        {
            string username = rsViewModel.userName;
            var user = await _usermgr.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("User not found, server error");
            }

            string userId = user.Id;

            string reservationNumber = GenerateReservationNumber();
            DateTime reserVationDate = DateTime.Parse(rsViewModel.reservationDate);
            DateTime dueDate = DateTime.Parse(rsViewModel.expectedReturnDate);

            using (SqlConnection conn = new SqlConnection(ctxt.Database.GetConnectionString()))
            {
                conn.Open();
                SqlCommand bookCopyAvailableCommand = new SqlCommand("GetBookCopyAvailable", conn);
                bookCopyAvailableCommand.CommandType = CommandType.StoredProcedure;
                bookCopyAvailableCommand.Parameters.Add(new SqlParameter("@BookCopyID",rsViewModel.bookcopyId));
                int copiesAvailable = 0;
                var bookCopyAvailableReader = bookCopyAvailableCommand.ExecuteReader();
                while (bookCopyAvailableReader.Read())
                {
                    copiesAvailable = bookCopyAvailableReader.GetInt32(1);
                }

                if(copiesAvailable==0)
                {
                    return BadRequest("No More Copies Available");
                }
                copiesAvailable--;
                SqlCommand upDateBookCopiesAvailableCommand = new SqlCommand("updateCopiesAvailable", conn);
                upDateBookCopiesAvailableCommand.CommandType =CommandType.StoredProcedure;
                SqlParameter upDateBookID = new SqlParameter("@BookCopyID", rsViewModel.bookcopyId);
                SqlParameter copiesAvailableParam = new SqlParameter("@CopiesAvailable", copiesAvailable);
                upDateBookCopiesAvailableCommand.Parameters.Add(copiesAvailableParam);
                upDateBookCopiesAvailableCommand.Parameters.Add(upDateBookID);
                var updateResult = upDateBookCopiesAvailableCommand.ExecuteNonQuery();
                if (updateResult == 0)
                {
                    return BadRequest("Failed to update copiesAvailable");

                }
                SqlCommand createReservationCommand = new SqlCommand("CreateReservation", conn);
                createReservationCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter bookCopyid = new SqlParameter("@BookCopyID",rsViewModel.bookcopyId);
                SqlParameter dateReserved = new SqlParameter("@DateReserved", reserVationDate);
                SqlParameter dueDateParam = new SqlParameter("@DueDate", dueDate);

                SqlParameter userID = new SqlParameter("@UserId", userId);
                SqlParameter reservationNumberParam = new SqlParameter("@ReservationNumber", reservationNumber);
                createReservationCommand.Parameters.Add(bookCopyid);
                createReservationCommand.Parameters.Add(dateReserved);
                createReservationCommand.Parameters.Add(userID);
                createReservationCommand.Parameters.Add(reservationNumberParam);
                createReservationCommand.Parameters.Add(dueDateParam);

                var insertReservationResult = createReservationCommand.ExecuteNonQuery();
                if(insertReservationResult == 0)
                {
                    return BadRequest("Failed to create a reservation");
                }
                conn.Close();



            }

            return Ok(reservationNumber);
        }



        [HttpPut]
        [Route("unreserve/{reservationNumber}")]
        [Authorize]
        public IActionResult unReserveBook(string reservationNumber)
        {
       
            ReservationViewModel rmViewModel = new ReservationViewModel();
            using (SqlConnection conn = new SqlConnection(ctxt.Database.GetConnectionString())) 
            { 
                conn.Open();
                SqlCommand reserveDetailsCommand = new SqlCommand("GetReservationDetails",conn);
                reserveDetailsCommand.CommandType = CommandType.StoredProcedure;
                reserveDetailsCommand.Parameters.Add(new SqlParameter("@Reservationnumber",reservationNumber));
                var reserveDetailsResult = reserveDetailsCommand.ExecuteReader();
                
                while (reserveDetailsResult.Read())
                {
                    rmViewModel.BookCopyId = reserveDetailsResult.GetInt32(0);
                    rmViewModel.BookTitle = reserveDetailsResult.GetString(1);
                    rmViewModel.author = reserveDetailsResult.GetString(2);
                    rmViewModel.publisher = reserveDetailsResult.GetString(3);
                    rmViewModel.DateReserved = reserveDetailsResult.GetDateTime(5);
                }

                SqlCommand bookCopyAvailableCommand = new SqlCommand("GetBookCopyAvailable", conn);
                bookCopyAvailableCommand.CommandType = CommandType.StoredProcedure;
                bookCopyAvailableCommand.Parameters.Add(new SqlParameter("@BookCopyID", rmViewModel.BookCopyId));
                int copiesAvailable = 0;
                int totalCopiesAvailable = 0;
                var bookCopyAvailableReader = bookCopyAvailableCommand.ExecuteReader();
                while (bookCopyAvailableReader.Read())
                {
                    totalCopiesAvailable = bookCopyAvailableReader.GetInt32(0);
                    copiesAvailable = bookCopyAvailableReader.GetInt32(1);
                }

                

                copiesAvailable++;
                if (copiesAvailable > totalCopiesAvailable)
                {
                    return BadRequest("Copies Available exceed total copies");
                }
                SqlCommand upDateBookCopiesAvailableCommand = new SqlCommand("updateCopiesAvailable", conn);
                SqlParameter upDateBookID = new SqlParameter("@BookCopyID", rmViewModel.BookCopyId);
                SqlParameter copiesAvailableParam = new SqlParameter("@CopiesAvailable", copiesAvailable);
                upDateBookCopiesAvailableCommand.CommandType = CommandType.StoredProcedure;
                upDateBookCopiesAvailableCommand.Parameters.Add(copiesAvailableParam);
                upDateBookCopiesAvailableCommand.Parameters.Add(upDateBookID);
                var updateResult = upDateBookCopiesAvailableCommand.ExecuteNonQuery();
                if (updateResult == 0)
                {
                    return BadRequest("Failed to update copiesAvailable");

                }

                DateTime returnDate = DateTime.Now;
                SqlCommand returnCommand = new SqlCommand("ReturnBook", conn);
                returnCommand.CommandType = CommandType.StoredProcedure;
                returnCommand.Parameters.Add(new SqlParameter("@Reservationnumber", reservationNumber));
                returnCommand.Parameters.Add(new SqlParameter("@DateReturned", returnDate));
                var returnResult = returnCommand.ExecuteNonQuery();
                if(returnResult == 0)
                {
                    return BadRequest("Failed to return Book");
                }

            }



            return Ok(rmViewModel);
        }




    }
}
