using UnityEngine;

public class ServerInitializer : MonoBehaviour
{
    [SerializeField] private ServerTransmitter serverTransmitter;
    [SerializeField] private ServerCalls serverCalls;
    [SerializeField] private ServerEvents serverEvents;

    private ServerContainer serverContainer;

    public ServerContainer GetServerContainer()
    {
        serverContainer = new ServerContainer(serverTransmitter, serverCalls, serverEvents);

        return serverContainer;
    }
}
