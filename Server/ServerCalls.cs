using UnityEngine;

public class ServerCalls : Photon.MonoBehaviour
{
    [Header("Links to instances")]
    [SerializeField] private ServerTransmitter serverTransmitter;

    public delegate void ServerFieldHandler(int numberOfActivated);

    public delegate void PlayerHandler(string message);

    public delegate void GameHandler(bool isThisClientHost);

    public event ServerFieldHandler OnServerFieldUpdated;

    public event PlayerHandler OnPlayerJoined;

    public event GameHandler OnGameStarted;

    [PunRPC]
    public void UpdatePlayingFieldForEveryone(int numberOfActivated)
    {
        OnServerFieldUpdated?.Invoke(numberOfActivated);
    }

    [PunRPC]
    public void StartGameForEveryone(string nicknameOfJoinedPlayer)
    {
        serverTransmitter.WriteDebugMessage("Game started.");

        OnGameStarted?.Invoke(serverTransmitter.IsThisClientHost);

        if (serverTransmitter.IsThisClientHost)
        {
            OnPlayerJoined?.Invoke(nicknameOfJoinedPlayer);

            photonView.RPC("ShowNameHostPlayer", PhotonTargets.Others, serverTransmitter.Nickname);
        }
    }

    [PunRPC]
    public void ShowNameHostPlayer(string nicknameOfHostPlayer)
    {
        OnPlayerJoined?.Invoke(nicknameOfHostPlayer);
    }

    [PunRPC]
    public void RestartLocalGame()
    {
        serverTransmitter.WriteDebugMessage("Game restarted.");

        OnGameStarted?.Invoke(serverTransmitter.IsThisClientHost);
    }
}
