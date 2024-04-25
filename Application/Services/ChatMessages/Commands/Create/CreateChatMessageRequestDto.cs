using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatMessages.Commands.Create
{
    public class CreateChatMessageRequestDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
