using UnityEngine;

public class ServerCalls : Photon.PunBehaviour
{
    [Header("Links to instances")]
    [SerializeField] private ServerWork serverWork;

    public override void OnConnectedToPhoton()
    {
        Debug.Log("Successfully connected to Photon.");
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        serverWork.PlayerIsLeftRoom();
    }
}
