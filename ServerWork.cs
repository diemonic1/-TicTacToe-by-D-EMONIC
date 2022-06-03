using UnityEngine;

public class ServerWork : Photon.MonoBehaviour
{
    public bool IsThisClientHost { get; private set; }
    public bool IsGameStarted { get; private set; }

    private RoomInfo[] _roomsList;

    [Header("Links to instances")]
    [SerializeField] private Manager manager;

    private void Start()
    {
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings("1");
    }

    public string getRoomName(int numberInList)
    {
        return _roomsList[numberInList].Name;
    }

    public int getCountOfPlayersInRoom(int numberInList)
    {
        return _roomsList[numberInList].PlayerCount;
    }

    public int getCountOfRooms()
    {
        return PhotonNetwork.GetRoomList().Length;
    }

    public void createRoom(string roomName)
    {
        Debug.Log("Trying to Create Room");

        RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);

        Debug.Log("this is first player");
        IsThisClientHost = true;
    }

    public void joinRoom(string roomName)
    {
        Debug.Log("Trying to Join Room");

        PhotonNetwork.JoinRoom(roomName);

        Debug.Log("this is second player");
        restartGame();
        IsThisClientHost = false;
    }

    public void leaveRoom()
    {
        photonView.RPC("playerIsLeftRoom", PhotonTargets.All, IsThisClientHost);
        PhotonNetwork.LeaveRoom();
    }

    public void sendNumberOfPressedButton(int numberOfActivated)
    {
        photonView.RPC("updatePlayingFieldForEveryone", PhotonTargets.All, numberOfActivated);
    }

    public void OnJoinedRoom()
    {
        if (IsThisClientHost == false)
            photonView.RPC("startGameForEveryone", PhotonTargets.All, manager.getLocalNickname());
    }

    public void restartGame()
    {
        photonView.RPC("restartGameForEveryone", PhotonTargets.All);
    }

    public void OnReceivedRoomListUpdate()
    {
        _roomsList = PhotonNetwork.GetRoomList();
        manager.refreshListOfRooms();
    }

    [PunRPC]
    public void playerIsLeftRoom(bool HostLeftRoom)
    {
        Debug.Log("player is left room");

        IsGameStarted = false;
        manager.prepareRoom(IsThisClientHost);

        if (HostLeftRoom && IsThisClientHost == false)
        {
            PhotonNetwork.LeaveRoom();
            manager.backToListOfRooms();
        }
    }

    [PunRPC]
    public void updatePlayingFieldForEveryone(int numberOfActivated)
    {
        manager.updatePlayingField(numberOfActivated, IsThisClientHost);
    }

    [PunRPC]
    public void startGameForEveryone(string nicknameOfJoinedPlayer)
    {
        Debug.Log("game started");

        IsGameStarted = true;
        manager.startGame(IsThisClientHost);
        if (IsThisClientHost)
        {
            manager.showNameOfSecondPlayer(nicknameOfJoinedPlayer);
            photonView.RPC("showNameHostPlayer", PhotonTargets.Others, manager.getLocalNickname());
        }
    }

    [PunRPC]
    public void showNameHostPlayer(string nicknameOfHostPlayer)
    {
        manager.showNameOfSecondPlayer(nicknameOfHostPlayer);
    }

    [PunRPC]
    public void restartGameForEveryone()
    {
        Debug.Log("game restarted");
        manager.restartLocalGame(IsThisClientHost);
    }
}
