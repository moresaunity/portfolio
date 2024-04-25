using Admin.EndPoint.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RestSharp;
using ChatMessageDto = Admin.EndPoint.Models.Dtos.ChatMessageDto;

namespace Admin.EndPoint.Hubs
{
    [Authorize(Roles = "Admin")]
    public class ChatRoomHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var client = new RestClient("https://localhost:7239");
            var getChatRoomRequest = new RestRequest("/api/v1/ChatRoom", Method.Get);
            BaseDto<List<GetChatRoomDto>>? getChatRoomResult = client.Get<BaseDto<List<GetChatRoomDto>>>(getChatRoomRequest);
            List<Guid> ChatRoomIds = new List<Guid>();
            foreach (var item in getChatRoomResult.Data) ChatRoomIds.Add(item.Id);
            await Clients.Caller.SendAsync("GetRooms", ChatRoomIds);

            await base.OnConnectedAsync();
        }
        public async Task LoadMessage(Guid roomId)
        {
            var client = new RestClient("https://localhost:7239");
            var getChatMessageRequest = new RestRequest("/api/v1/ChatMessage/" + roomId, Method.Get);
            BaseDto<List<GetChatMessageDto>>? getChatMessageResult = client.Get<BaseDto<List<GetChatMessageDto>>>(getChatMessageRequest);

            foreach (var item in getChatMessageResult.Data)
                await Clients.Caller.SendAsync("getNewMessage", item.Sender, item.Message, item.Time.ToShortTimeString());
        }
        public async Task SendMessage(Guid roomId, string text)
        {
            var client = new RestClient("https://localhost:7239");
            var SaveChatMessageRequest = new RestRequest("/api/v1/ChatMessage/" + roomId, Method.Get);
            SaveChatMessageRequest.AddJsonBody(new ChatMessageDto(Context.User.Identity.Name, text));
            client.Post(SaveChatMessageRequest);

            //await siteChatHub.Clients.Group(roomId.ToString()).SendAsync("getNewMessage", Context.User.Identity.Name, text, DateTime.Now);
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
        public async Task LeaveRoom(Guid roomId)
        {
            var client = new RestClient("https://localhost:7239");
            var LeaveRoomRequest = new RestRequest("/api/v1/ChatRoom", Method.Delete);
            LeaveRoomRequest.AddHeader("ConnectionId", Context.ConnectionId);
            LeaveRoomRequest.AddHeader("RoomId", roomId);
            client.Delete<BaseDto<Guid>>(LeaveRoomRequest);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            //var client = new RestClient("https://localhost:7239");
            //var LeaveRoomRequest = new RestRequest("/api/v1/ChatRoom/" + Context.ConnectionId, Method.Delete);
            //client.Delete<BaseDto>(LeaveRoomRequest);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
