using Microsoft.AspNetCore.SignalR;
using Swipes.Bll.Entities;
using Swipes.Bll.Entities.Enums;
using Swipes.Bll.Repositories;

namespace Swipes.Api.Hubs;

public sealed class LobbyHub : Hub
{
    private readonly ILobbyRepository _lobbyRepository;
    private readonly IVoteRepository _voteRepository;

    public LobbyHub(ILobbyRepository lobbyRepository, IVoteRepository voteRepository)
    {
        _lobbyRepository = lobbyRepository;
        _voteRepository = voteRepository;
    }

    public async Task SendNotification(string content)
    {
        await Clients.All.SendAsync("ReceiveNotification", content);
    }

    public async Task<string> CreateLobby(long ownerId)
    {
        var connectionId = Context.ConnectionId;
        
        var lobby = new LobbyEntityV1
        {
            OwnerId = ownerId,
            LobbyStatus = LobbyStatus.Waiting,
            Connections =
            [
                new LobbyEntityV1.ConnectionIds
                {
                    ConnectionId = connectionId,
                    UserId = ownerId
                }
            ]
        };
        
        var id = await _lobbyRepository.AddAsync(lobby);
        return id;
    }

    public async Task JoinInLobby(string lobbyId, long userId)
    {
        var lobby = await _lobbyRepository.GetAsync(lobbyId);
        if (lobby is null)
        {
            await Clients.Caller.SendAsync("Error", "Lobby not found");
            return;
        }

        bool isUpdated = false;
        var connectionId = Context.ConnectionId;
        foreach (var connection in lobby.Connections)
        {
            if (connection.UserId != userId) continue;
            connection.ConnectionId = connectionId;
            isUpdated = true;

            break;
        }

        if (!isUpdated)
        {
            lobby.Connections.Add(new LobbyEntityV1.ConnectionIds()
            {
                ConnectionId = connectionId,
                UserId = userId
            });
        }

        await _lobbyRepository.UpdateAsync(lobby);

        Console.WriteLine("???");
        var connections = lobby.Connections.Select(c => c.ConnectionId)
            .ToArray();
        foreach (var con in connections)
        {
            Console.WriteLine(con);
        }
        await Clients.Clients(connections).SendAsync("ReceiveMessage", $"New connection: {connectionId}");
    }

    public async Task DestroyLobby(string lobbyId)
    {
        var lobby = await _lobbyRepository.GetAsync(lobbyId);
        if (lobby is null)
        {
            await Clients.Caller.SendAsync("Error", "Lobby not found");

            return;
        }

        var connections = lobby.Connections.Select(c => c.ConnectionId);

        await _lobbyRepository.RemoveAsync(lobbyId);
        await Clients.Groups(connections).SendAsync("Lobby has destroyed");
    }

    public async Task LeaveLobby(string lobbyId)
    {
        var lobby = await _lobbyRepository.GetAsync(lobbyId);
        if (lobby is null)
        {
            await Clients.Caller.SendAsync("Error", "Lobby not found");
            return;
        }

        for (var i = 0; i < lobby.Connections.Count; ++i)
        {
            if (!lobby.Connections[i].ConnectionId.Equals(Context.ConnectionId))
                continue;

            lobby.Connections.RemoveAt(i);
            break;
        }

        await _lobbyRepository.UpdateAsync(lobby);
    }

    public async Task AddVote(long userId, long taskId, string opinionString)
    {
        var lobby = await _lobbyRepository.GetUserLobbyAsync(userId);
        if (lobby is null)
        {
            await Clients.Caller.SendAsync("Error", "You are not connected to any lobby");
            return;
        }

        var opinion = Enum.Parse<Opinion>(opinionString);
        
        await _voteRepository.AddAsync(new VoteEntityV1
        {
            TaskId = taskId,
            Opinion = opinion,
            UserId = userId,
            LobbyId = lobby.Id
        });

        if (opinion != Opinion.Like)
            return;

        var lobbyVotes = await _voteRepository.GetLobbyVotesAboutTask(taskId, lobby.Id);
        var positiveOpinions = lobbyVotes.Select(v => v.Opinion is Opinion.Like)
            .Count();

        if (positiveOpinions <= lobbyVotes.Length / 2)
            return;

        var lobbyConnections = lobby.Connections.Select(c => c.ConnectionId);

        await Clients.Groups(lobbyConnections)
            .SendAsync("ReceiveMessage", $"Lobby choose task with id {taskId}");
    }
}
