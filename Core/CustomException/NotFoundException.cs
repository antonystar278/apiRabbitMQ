namespace Core.CustomException
{
    public class NotFoundException : BadRequest
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
