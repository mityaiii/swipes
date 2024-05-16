// using Swipes.Bll.Entities;
// using Swipes.Bll.Entities.Enums;
// using Swipes.Bll.Models;
// using Swipes.Bll.Services.Interfaces;
//
// namespace Swipes.Bll.Services;
//
// public class LobbyService : ILobbyService
// {
//     public Task<WebSocketReply> CreateLobbyAsync(HubCall, long ownerId, CancellationToken token)
//     {
//         var connectionId = Context.ConnectionId;
//
//         var lobby = new LobbyEntityV1
//         {
//             OwnerId = ownerId,
//             LobbyStatus = LobbyStatus.Waiting,
//             Connections =
//             [
//                 new LobbyEntityV1.ConnectionIds()
//                 {
//                     ConnectionId = connectionId,
//                     UserId = ownerId
//                 }
//             ]
//         };
//         
//         await _lobbyRepository.AddAsync(lobby, token);
//     }
//
//     public Task<WebSocketReply> DestroyLobbyAsync(CancellationToken token)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<WebSocketReply> JoinInLobby(string lobbyId, CancellationToken token)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<WebSocketReply> LeftLobby(string lobbyId, CancellationToken token)
//     {
//         throw new NotImplementedException();
//     }
// }