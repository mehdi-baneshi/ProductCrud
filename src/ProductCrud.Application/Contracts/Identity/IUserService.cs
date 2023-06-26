namespace ProductCrud.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<string> GetCurrentUserName();
    }
}
