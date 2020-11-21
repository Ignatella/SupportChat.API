using SignalR.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Interfaces
{
    interface IMessageRepository
    {
        void AddMessage(Message message);

        Task<Message> GetMessage(int id);

        Task<IEnumerable<Message>> GetMessageThead(int userId, int recipientId);

        Task<bool> SaveAllAsync();
    }
}
