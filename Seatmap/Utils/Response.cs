namespace Seatmap.Utils
{
    public class Response<T>: Response
    {
        public Response(T value)
        {
            Value = value;
        }

        public Response(string errorMessage) 
            : base(errorMessage) { }

        public T Value { get; set; }


    }

    public class Response
    {
        public Response() { }

        public Response(string errorMessage)
        {
            ErrorMessage = errorMessage;
            HasError = true;
        }
        
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
    }
}
