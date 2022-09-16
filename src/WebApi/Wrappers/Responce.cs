namespace WebApi.Wrappers
{
    public class Response<T>
    {
        public Response(T data, string message = null)
        {
            succeeded = true;
            this.message = message;
            this.data = data;
        }
        public Response(string message)
        {
            succeeded = false;
            this.message = message;
        }

        public Response()
        {
            succeeded = false;
            message = "error";
        }

        public bool succeeded { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
