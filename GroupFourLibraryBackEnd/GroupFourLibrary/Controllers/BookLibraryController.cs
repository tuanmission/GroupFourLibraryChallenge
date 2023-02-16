using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System;
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
        public IEnumerable<BookDTO> ListBooks()
        {

        }
    }
}
