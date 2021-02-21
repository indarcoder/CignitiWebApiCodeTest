using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TestCode.Domain.Posts
{
    public class Post
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
    }
}
