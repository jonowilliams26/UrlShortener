namespace ShorteningService.Application.Models
{
    public class CQRSResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess => StatusCode == 200;

        public static CQRSResponse<T> Success<T>(T data) => new CQRSResponse<T> { StatusCode = 200, Data = data };
        public static CQRSResponse<T> NotFound<T>() => new CQRSResponse<T> { StatusCode = 404 };
    }

    public class CQRSResponse<T> : CQRSResponse
    {
        public T Data { get; set; }
    }
}
