using Application.Services.Products.ChatMessages.Queries.Get;
using Domain.Dtos;
using Microsoft.AspNetCore.SignalR;
using RestSharp;

namespace WebSite.EndPoint.Hubs
{
    public class SiteChatHub : Hub
    {
        public async Task SendNewMessage(string Sender, string Message)
        {
            var client = new RestClient("https://localhost:7239");
            var getRoomIdRequest = new RestRequest("/api/v1/ChatRoom/" + Context.ConnectionId, Method.Get);
            getRoomIdRequest.AddParameter("ConnectionId", Context.ConnectionId);
            BaseDto<Guid>? getRoomIdResult = client.Get<BaseDto<Guid>>(getRoomIdRequest);

            var SaveChatMessageRequest = new RestRequest("/api/v1/ChatMessage/" + getRoomIdResult.Data, Method.Get);
            SaveChatMessageRequest.AddJsonBody(new ChatMessageDto(Sender, Message));
            client.Post(SaveChatMessageRequest);

            await Clients.Groups(getRoomIdResult.Data.ToString())
                .SendAsync("getNewMessage", Sender, Message, DateTime.Now.ToShortTimeString());
        }

        public async Task LeaveRoom(Guid roomId)
        {
            var client = new RestClient("https://localhost:7239");
            var LeaveRoomRequest = new RestRequest("/api/v1/ChatRoom", Method.Delete);
            LeaveRoomRequest.AddHeader("ConnectionId", Context.ConnectionId);
            LeaveRoomRequest.AddHeader("RoomId", roomId);
            client.Delete<BaseDto<Guid>>(LeaveRoomRequest);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }
        public async Task JoinRoom(Guid roomId)
        {
            var client = new RestClient("https://localhost:7239");
            var JoinRoomRequest = new RestRequest("/api/v1/ChatRoom", Method.Put);
            JoinRoomRequest.AddHeader("ConnectionId", Context.ConnectionId);
            JoinRoomRequest.AddHeader("RoomId", roomId);
            client.Put<BaseDto<Guid>>(JoinRoomRequest);

            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }
        public async Task ShowChatBox(string roomId)
        {
            var client = new RestClient("https://localhost:7239");
            if (roomId == null)
            {
                var getRoomIdRequest = new RestRequest("/api/v1/ChatRoom", Method.Post);
                getRoomIdRequest.AddHeader("ConnectionId", Context.ConnectionId);
                BaseDto<Guid>? getRoomIdResult = client.Post<BaseDto<Guid>>(getRoomIdRequest);
                roomId = getRoomIdResult.Data.ToString();
                await Clients.Caller.SendAsync("ReceiveRoomId", roomId);
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                var getChatMessageRequest = new RestRequest("/api/v1/ChatMessage/" + roomId, Method.Get);
                BaseDto<List<GetChatMessageDto>>? getChatMessageResult = client.Get<BaseDto<List<GetChatMessageDto>>>(getChatMessageRequest);

                foreach (var item in getChatMessageResult.Data)
                    await Clients.Caller.SendAsync("getNewMessage", item.Sender, item.Message, item.Time.ToShortTimeString());
            }

        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
    public class ChatMessageDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public ChatMessageDto(string Sender, string Message)
        {
            this.Sender = Sender;
            this.Message = Message;
        }
        public ChatMessageDto()
        {

        }
    }
}
