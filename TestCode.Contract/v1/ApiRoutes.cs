using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCode.Contract.v1
{
    public static class ApiRoutes 
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;       

        public static class Identity
        {
            public const string Login = Base + "/identity/login";            
        }

        public static class Post
        {
            public const string GetPosts = Base + "/posts";
            public const string GetPostById = Base + "/posts/{Id}";
            public const string CreatePost = Base + "/posts";
            public const string UpdatePost = Base + "/posts/{Id}";
            public const string DeletePost = Base + "/posts/{Id}"; 
        }
    }
}
