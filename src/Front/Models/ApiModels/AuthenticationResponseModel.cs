namespace Front.Models.ApiModels
{
    public class AuthenticationResponseModel
    {
        public AuthenticationResult Result { get; set; }
        public int? AuthenticatedUserId { get; set; }
    }
    public enum AuthenticationResult
    {
        Success,
        NoUserError,
        IncorrectPasswordError,
        UnknownError
    }
}

