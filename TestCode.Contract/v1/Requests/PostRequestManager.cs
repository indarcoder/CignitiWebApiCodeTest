using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestCode.Contract.v1.Requests
{
    public class PostInputModel
    {
        [Required(ErrorMessage ="Please enter a post title")]
        public string Title
        {
            get;set;
        }
    }
}
