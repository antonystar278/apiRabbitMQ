using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces.Operations.Messaging.Send
{
    public interface IOperationUpdateSender
    {
        Task SendOperation(Operation operation);
    }
}
