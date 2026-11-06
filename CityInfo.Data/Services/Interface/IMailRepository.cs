namespace CityInfo.Data.Services.Interface
{
    public interface IMailRepository
    {
        void Send(string subject, string message);
    }
}
