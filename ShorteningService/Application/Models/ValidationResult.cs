namespace ShorteningService.Application.Models
{
    public class ValidationResult
    {
        public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; set; }

        public static ValidationResult Success => new ValidationResult();
        public static ValidationResult Error(string message) => new ValidationResult(message);

        public ValidationResult()
        {
        }

        public ValidationResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
