using Core.Entities;
using Core.Models.Operations;
using System.Threading.Tasks;

namespace Core.Interfaces.Operations.Messaging.Send
{
    public interface IOperationUpdateSender
    {
        Task SendOperationToQueue(OperationCreateMessageModel model);
    }
}
