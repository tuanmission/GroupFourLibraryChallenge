using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.SqlClient;
using GroupFourLibrary.Models;
using GroupFourLibrary.DTO;

namespace GroupFourLibrary.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class BookLibraryController : ControllerBase
    {
        private GroupFourLibraryContext ctxt;
        private readonly IMapper _mapper;
        public BookLibraryController(GroupFourLibraryContext context, IMapper mapper)
        {
            this.ctxt = context;
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

    }
}
