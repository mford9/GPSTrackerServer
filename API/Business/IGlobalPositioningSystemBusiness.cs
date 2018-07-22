using System.Threading.Tasks;

namespace Ford.Tracker.Api.Business
{
    public interface IGlobalPositioningSystemBusiness
    {
        Task MapAndSendToMessengerAsync(string gpsInformation);
    }
}
