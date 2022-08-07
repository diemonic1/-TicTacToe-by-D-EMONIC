using UnityEngine;

public class ServerWork : Photon.MonoBehaviour
{
    private bool _isGameStarted;
    private string _nickname;
    private RoomInfo[] _roomsList;

    public delegate void UpdateListOfRoomsHandler(RoomInfo[] roomsList);

    public delegate void PlayerLeftHandler();

    public delegate void ServerFieldHandler(int numberOfActivated, bool isThisClientHost);

    public delegate void ServerCallHandler(string message);

    public delegate void ServerStartGame(bool isThisClientHost);

    public event UpdateListOfRoomsHandler OnListUpdated;

    public event PlayerLeftHandler OnPlayerLeftRoom;

    public event PlayerLeftHandler OnHostLeftRoom;

    public event ServerFieldHandler OnServerFieldUpdated;

    public event ServerCallHandler OnPlayerJoined;

    public event ServerStartGame OnGameStarted;

    public bool IsThisClientHost { get; private set; }

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
        IsThisClientHost = true;
    }

    public void TryJoinRoom(string roomName)
    {
        Debug.Log("Trying to Join Room...");

        PhotonNetwork.JoinRoom(roomName);

        Debug.Log("Successfully connected to the room. This is second (slave) player.");
        IsThisClientHost = false;
    }

    public void LeaveRoom()
    {
        OnPlayerLeftRoom?.Invoke();
        PhotonNetwork.LeaveRoom();
    }

    public void PlayerIsLeftRoom()
    {
        Debug.Log("Player is left room.");

        _isGameStarted = false;

        OnPlayerLeftRoom?.Invoke();

        bool hostLeftRoom = !IsThisClientHost;

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
        if (!IsThisClientHost)
            photonView.RPC("StartGameForEveryone", PhotonTargets.All, _nickname);
    }

    public void SendNumberOfPressedButton(int numberOfActivated)
    {
        if (_isGameStarted)
            photonView.RPC("UpdatePlayingFieldForEveryone", PhotonTargets.All, numberOfActivated);
    }

    public void RestartGameForEveryone()
    {
        photonView.RPC("RestartLocalGame", PhotonTargets.All);
    }

    [PunRPC]
    public void UpdatePlayingFieldForEveryone(int numberOfActivated)
    {
        OnServerFieldUpdated?.Invoke(numberOfActivated, IsThisClientHost);
    }

    [PunRPC]
    public void StartGameForEveryone(string nicknameOfJoinedPlayer)
    {
        Debug.Log("Game started.");

        _isGameStarted = true;

        OnGameStarted?.Invoke(IsThisClientHost);

        if (IsThisClientHost)
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

        OnGameStarted?.Invoke(IsThisClientHost);
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
