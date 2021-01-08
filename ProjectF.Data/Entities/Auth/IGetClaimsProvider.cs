namespace ProjectF.Data.Entities.Auth
{
    public interface IGetClaimsProvider
    {
        string CompanyId { get; }
        string UserId { get;}
    }
}
