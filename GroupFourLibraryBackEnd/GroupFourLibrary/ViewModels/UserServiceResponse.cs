using System.Collections.Generic;
using System;
namespace GroupFourLibrary.ViewModels
{
    public class UserServiceResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }


        public DateTime? ExpireDate { get; set; }
    }
}
