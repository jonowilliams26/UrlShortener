namespace ShorteningService.Application.Models
{
    public class CQRSResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public static CQRSResponse<T> Success<T>(T data) => new CQRSResponse<T> { StatusCode = 200, Data = data };
    }

    public class CQRSResponse<T> : CQRSResponse
    {
        public T Data { get; set; }
    }
}
