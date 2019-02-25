using System.Threading.Tasks;

namespace AspCoreAngular
{
    public interface IMessageHub
    {
        Task SendMessage(string user, string message);
    }
}