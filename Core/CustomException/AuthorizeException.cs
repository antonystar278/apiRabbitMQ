using System;

namespace Core.CustomException
{
    public class AuthorizeException: Exception
    { 
        public AuthorizeException(string message): base(message)
        {

        }
    }
}
