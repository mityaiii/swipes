// using Swipes.Bll.Entities;
// using Swipes.Bll.Entities.Enums;
// using Swipes.Bll.Repositories;
// using Swipes.Bll.Services.Interfaces;
//
// namespace Swipes.Bll.Services;
//
// public class VoteService : IVoteService
// {
//     private readonly ILobbyRepository _lobbyRepository;
//     private readonly IVoteRepository _voteRepository;
//
//     public VoteService(ILobbyRepository lobbyRepository, IVoteRepository voteRepository)
//     {
//         _lobbyRepository = lobbyRepository;
//         _voteRepository = voteRepository;
//     }
//
//     public async Task AddAsync(VoteEntityV1 voteEntityV1, CancellationToken token)
//     {
//         await _voteRepository.UpdateAsync(voteEntityV1.TaskId, 
//             voteEntityV1.UserId,
//             voteEntityV1.LobbyId,
//             voteEntityV1.Opinion,
//             token);
//
//         var votesOnTasks = await _voteRepository.GetAsync(voteEntityV1.TaskId, voteEntityV1.LobbyId, token);
//         var votesWithPositiveOpinion = votesOnTasks.Select(v => v.Opinion == Opinion.Like)
//             .ToArray();
//
//         var lobby = await _lobbyRepository.GetAsync(voteEntityV1.LobbyId, token);
//         
//         if (votesWithPositiveOpinion.Length > lobby.UserIds.Length / 2)
//         {
//             await NotifyUsers(lobby.UserIds, voteEntityV1.TaskId, token);
//         }
//     }
//
//     private async Task NotifyUsers(long[] userIds, long taskId, CancellationToken token)
//     {
//         
//     }
// }