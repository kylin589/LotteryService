namespace Lottery.Entities.Extend.Validation
{
    public class ValidationError
    {
        public string Message { get; set; }
        public ValidationError(string message)
        {
            Message = message;
        }

        public override string ToString()
        {
            return Message;
        }
    }
}