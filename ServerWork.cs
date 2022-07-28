using UnityEngine;

public class ServerWork : Photon.MonoBehaviour
{
    private RoomInfo[] _roomsList;

    [Header("Links to instances")]
    [SerializeField] private Manager manager;

    public bool IsThisClientHost { get; private set; }

    public bool IsGameStarted { get; private set; }

    public string GetRoomName(int numberInList)
    {
        return _roomsList[numberInList].Name;
    }

    public int GetCountOfPlayersInRoom(int numberInList)
    {
        return _roomsList[numberInList].PlayerCount;
    }

    public int GetCountOfRooms()
    {
        return PhotonNetwork.GetRoomList().Length;
    }

    public void CreateRoom(string roomName)
    {
        Debug.Log("Trying to Create Room.");

        RoomOptions roomOptions = new () { isVisible = true, maxPlayers = 2 };
        PhotonNetwork.CreateRoom(roomName, roomOptions, TypedLobby.Default);

        Debug.Log("Room created. This is first (host) player.");
        IsThisClientHost = true;
    }

    public void JoinRoom(string roomName)
    {
        Debug.Log("Trying to Join Room...");

        PhotonNetwork.JoinRoom(roomName);

        Debug.Log("Successfully connected to the room. This is second (slave) player.");
        RestartGame();
        IsThisClientHost = false;
    }

    public void LeaveRoom()
    {
        photonView.RPC("PlayerIsLeftRoom", PhotonTargets.All, IsThisClientHost);
        PhotonNetwork.LeaveRoom();
    }

    public void SendNumberOfPressedButton(int numberOfActivated)
    {
        photonView.RPC("UpdatePlayingFieldForEveryone", PhotonTargets.All, numberOfActivated);
    }

    public void RestartGame()
    {
        photonView.RPC("RestartGameForEveryone", PhotonTargets.All);
    }

    public void OnJoinedRoom()
    {
        if (!IsThisClientHost)
            photonView.RPC("StartGameForEveryone", PhotonTargets.All, manager.GetLocalNickname());
    }

    public void OnReceivedRoomListUpdate()
    {
        _roomsList = PhotonNetwork.GetRoomList();
        manager.RefreshListOfRooms();
    }

    public void PlayerDisconnected()
    {
        Debug.Log("Photon player disconnected.");

        PlayerIsLeftRoom(!IsThisClientHost);
    }

    [PunRPC]
    public void PlayerIsLeftRoom(bool hostLeftRoom)
    {
        Debug.Log("Player is left room.");

        IsGameStarted = false;
        manager.PrepareRoom(IsThisClientHost);

        if (hostLeftRoom && !IsThisClientHost)
        {
            PhotonNetwork.LeaveRoom();
            manager.BackToListOfRooms();
        }
    }

    [PunRPC]
    public void UpdatePlayingFieldForEveryone(int numberOfActivated)
    {
        manager.UpdatePlayingField(numberOfActivated, IsThisClientHost);
    }

    [PunRPC]
    public void StartGameForEveryone(string nicknameOfJoinedPlayer)
    {
        Debug.Log("Game started.");

        IsGameStarted = true;
        manager.StartGame(IsThisClientHost);
        if (IsThisClientHost)
        {
            manager.ShowNameOfSecondPlayer(nicknameOfJoinedPlayer);
            photonView.RPC("ShowNameHostPlayer", PhotonTargets.Others, manager.GetLocalNickname());
        }
    }

    [PunRPC]
    public void ShowNameHostPlayer(string nicknameOfHostPlayer)
    {
        manager.ShowNameOfSecondPlayer(nicknameOfHostPlayer);
    }

    [PunRPC]
    public void RestartGameForEveryone()
    {
        Debug.Log("Game restarted.");
        manager.RestartLocalGame(IsThisClientHost);
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
