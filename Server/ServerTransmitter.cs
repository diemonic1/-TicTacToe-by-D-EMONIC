using System;
using UnityEngine;

public class ServerTransmitter : Photon.MonoBehaviour
{
    [SerializeField] private bool _writeDebugMessages;

    public delegate void PlayerHandler();

    public event PlayerHandler OnPlayerLeftRoom;

    public event PlayerHandler OnHostLeftRoom;

    public bool IsThisClientHost { get; private set; }

    public string Nickname { get; private set; }

    public void SetNickname(string nickname)
    {
        if (nickname.Length <= 12)
            Nickname = nickname;
        else
            throw new Exception("Nickname must be no more than 12 characters!");
    }

    public void TryCreateNetworkRoom()
    {
        WriteDebugMessage("Trying to Create Room.");

        try
        {
            RoomOptions roomOptions = new () { IsVisible = true, MaxPlayers = 2 };
            PhotonNetwork.CreateRoom(Nickname, roomOptions, TypedLobby.Default);
        }
        catch (Exception e)
        {
            WriteDebugMessage("Exception caught: " + e);
        }

        WriteDebugMessage("Room created. This is first (host) player.");
        IsThisClientHost = true;
    }

    public void TryJoinRoom(string roomName)
    {
        WriteDebugMessage("Trying to Join Room...");

        try
        {
            PhotonNetwork.JoinRoom(roomName);
        }
        catch (Exception e)
        {
            WriteDebugMessage("Exception caught: " + e);
        }

        WriteDebugMessage("Successfully connected to the room. This is second (slave) player.");
        IsThisClientHost = false;
    }

    public void LeaveRoom()
    {
        OnPlayerLeftRoom?.Invoke();
        PhotonNetwork.LeaveRoom();
    }

    public void PlayerIsLeftRoom()
    {
        WriteDebugMessage("Player is left room.");

        OnPlayerLeftRoom?.Invoke();

        bool hostLeftRoom = !IsThisClientHost;

        if (hostLeftRoom)
        {
            PhotonNetwork.LeaveRoom();

            OnHostLeftRoom?.Invoke();
        }
    }

    public void SendNumberOfPressedButton(int numberOfActivated)
    {
        photonView.RPC("UpdatePlayingFieldForEveryone", PhotonTargets.All, numberOfActivated);
    }

    public void RestartGameForEveryone()
    {
        photonView.RPC("RestartLocalGame", PhotonTargets.All);
    }

    public void WriteDebugMessage(string message)
    {
        if (_writeDebugMessages)
            Debug.Log(message);
    }

    private void Start()
    {
        ConnectToPhoton();
    }

    private void ConnectToPhoton()
    {
        WriteDebugMessage("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings("1");
    }
}
