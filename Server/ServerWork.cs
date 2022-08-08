using UnityEngine;

public class ServerWork : Photon.MonoBehaviour
{
    private bool _isThisClientHost;
    private string _nickname;
    private RoomInfo[] _roomsList;

    public delegate void UpdateListOfRoomsHandler(RoomInfo[] roomsList);

    public delegate void PlayerLeftHandler();

    public delegate void ServerFieldHandler(int numberOfActivated);

    public delegate void ServerCallHandler(string message);

    public delegate void ServerStartGame(bool isThisClientHost);

    public event UpdateListOfRoomsHandler OnListUpdated;

    public event PlayerLeftHandler OnPlayerLeftRoom;

    public event PlayerLeftHandler OnHostLeftRoom;

    public event ServerFieldHandler OnServerFieldUpdated;

    public event ServerCallHandler OnPlayerJoined;

    public event ServerStartGame OnGameStarted;

    public void SetNickname(string nickname)
    {
        _nickname = nickname;
    }

    public void CreateNetworkRoom()
    {
        Debug.Log("Trying to Create Room.");

        RoomOptions roomOptions = new () { IsVisible = true, MaxPlayers = 2 };
        PhotonNetwork.CreateRoom(_nickname, roomOptions, TypedLobby.Default);

        Debug.Log("Room created. This is first (host) player.");
        _isThisClientHost = true;
    }

    public void TryJoinRoom(string roomName)
    {
        Debug.Log("Trying to Join Room...");

        PhotonNetwork.JoinRoom(roomName);

        Debug.Log("Successfully connected to the room. This is second (slave) player.");
        _isThisClientHost = false;
    }

    public void LeaveRoom()
    {
        OnPlayerLeftRoom?.Invoke();
        PhotonNetwork.LeaveRoom();
    }

    public void PlayerIsLeftRoom()
    {
        Debug.Log("Player is left room.");

        OnPlayerLeftRoom?.Invoke();

        bool hostLeftRoom = !_isThisClientHost;

        if (hostLeftRoom)
        {
            PhotonNetwork.LeaveRoom();

            OnHostLeftRoom?.Invoke();
        }
    }

    public void OnReceivedRoomListUpdate()
    {
        _roomsList = PhotonNetwork.GetRoomList();

        OnListUpdated?.Invoke(_roomsList);
    }

    public void OnJoinedRoom()
    {
        if (!_isThisClientHost)
            photonView.RPC("StartGameForEveryone", PhotonTargets.All, _nickname);
    }

    public void SendNumberOfPressedButton(int numberOfActivated)
    {
        photonView.RPC("UpdatePlayingFieldForEveryone", PhotonTargets.All, numberOfActivated);
    }

    public void RestartGameForEveryone()
    {
        photonView.RPC("RestartLocalGame", PhotonTargets.All);
    }

    [PunRPC]
    public void UpdatePlayingFieldForEveryone(int numberOfActivated)
    {
        OnServerFieldUpdated?.Invoke(numberOfActivated);
    }

    [PunRPC]
    public void StartGameForEveryone(string nicknameOfJoinedPlayer)
    {
        Debug.Log("Game started.");

        OnGameStarted?.Invoke(_isThisClientHost);

        if (_isThisClientHost)
        {
            OnPlayerJoined?.Invoke(nicknameOfJoinedPlayer);

            photonView.RPC("ShowNameHostPlayer", PhotonTargets.Others, _nickname);
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
        Debug.Log("Game restarted.");

        OnGameStarted?.Invoke(_isThisClientHost);
    }

    private void Start()
    {
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
    {
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings("1");
    }
}
