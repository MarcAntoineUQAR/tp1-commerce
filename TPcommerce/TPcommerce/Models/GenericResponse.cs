namespace TPcommerce.Models
{
    public class GenericResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }

        public bool Success { get; set; }

        public GenericResponse(T data, string message, bool success)
        {
            Data = data;
            Message = message;
            Success = success;
        }

        public GenericResponse(string message, bool success)
        {
            Data = default;
            Message = message;
            Success = success;
        }
    }
}