public class ServerContainer
{
    public ServerContainer(ServerTransmitter serverTransmitter, ServerCalls serverCalls, ServerEvents serverEvents)
    {
        ServerTransmitter = serverTransmitter;
        ServerCalls = serverCalls;
        ServerEvents = serverEvents;
    }

    public ServerTransmitter ServerTransmitter { get; private set; }

    public ServerCalls ServerCalls { get; private set; }

    public ServerEvents ServerEvents { get; private set; }
}
