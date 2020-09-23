namespace ShorteningService.Application.Models
{
    public class CQRSResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess => StatusCode == 200;
        public bool IsBadRequest => StatusCode == 400;
        public bool IsUnsuccessful => !IsSuccess;
        public virtual bool HasData => false;
        public virtual object GetData() => null;

        public static CQRSResponse<T> Success<T>(T data) => new CQRSResponse<T> { StatusCode = 200, Data = data };
        public static CQRSResponse<T> NotFound<T>(string message) => new CQRSResponse<T> { StatusCode = 404, ErrorMessage = message };

        public void BadRequest(ValidationResult validationResult)
        {
            StatusCode = 400;
            ErrorMessage = validationResult.ErrorMessage;
        }

        public void ServerError()
        {
            StatusCode = 500;
            ErrorMessage = "Sorry, an unexpected error occurred.";
        }
    }

    public class CQRSResponse<T> : CQRSResponse
    {
        public T Data { get; set; }
        public override bool HasData => true;
        public override object GetData() => Data;
    }
}
