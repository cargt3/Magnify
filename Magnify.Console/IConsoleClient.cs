using System.Threading.Tasks;

namespace Magnify.Console
{
    public interface IConsoleClient
    {
        Task ShowCarrierInterface();
        Task ShowShipperInterface();
    }
}