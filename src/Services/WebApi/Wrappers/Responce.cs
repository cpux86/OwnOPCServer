using System.Text.Json.Serialization;

namespace WebApi.Wrappers
{
    public class Response<T>
    {
        public Response(T data, string message = "succeeded")
        {
            Succeeded = true;
            this.Message = message;
            this.Data = data;
        }
        
        public Response(string message)
        {
            Succeeded = false;
            this.Message = message;
        }

        public Response()
        {
            Succeeded = false;
            Message = "error";
        }

        [JsonPropertyName("succeeded")]
        public bool Succeeded { get; private set; }
        [JsonPropertyName("message")]
        public string Message { get; private set; }
        [JsonPropertyName("data")]
        public T Data { get; set; }
    }
}
