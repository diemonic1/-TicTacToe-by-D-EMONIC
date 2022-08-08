using UnityEngine;

public class ServerEvents : Photon.PunBehaviour
{
    [Header("Links to instances")]
    [SerializeField] private ServerTransmitter serverTransmitter;

    public delegate void ListHandler(RoomInfo[] roomsList);

    public event ListHandler OnListUpdated;

    public override void OnConnectedToPhoton()
    {
        serverTransmitter.WriteDebugMessage("Successfully connected to Photon.");
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        serverTransmitter.PlayerIsLeftRoom();
    }

    public override void OnJoinedRoom()
    {
        if (!serverTransmitter.IsThisClientHost)
            photonView.RPC("StartGameForEveryone", PhotonTargets.All, serverTransmitter.Nickname);
    }

    public override void OnReceivedRoomListUpdate()
    {
        OnListUpdated?.Invoke(PhotonNetwork.GetRoomList());
    }
}
