namespace EcWebapi.Dto
{
    public class ApiResponseDto<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public ApiResponseErrorDto Error { get; set; }
    }

    public class ApiResponseErrorDto
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public string Detail { get; set; }
    }
}