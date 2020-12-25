using System;

namespace Core.CustomException
{
    public class BadRequest: Exception
    {
        public BadRequest(string message) : base(message)
        {

        }
    }
}
