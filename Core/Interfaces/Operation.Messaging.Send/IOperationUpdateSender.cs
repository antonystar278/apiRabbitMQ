using Core.Entities;

namespace Core.Interfaces.Operations.Messaging.Send
{
    public interface IOperationUpdateSender
    {
        void SendOperation(Operation operation);
    }
}
