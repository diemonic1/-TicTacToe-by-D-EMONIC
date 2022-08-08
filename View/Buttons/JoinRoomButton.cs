public class JoinRoomButton : ButtonRoot
{
    private string _roomName;

    public delegate void ButtonHandler(string roomName);

    public event ButtonHandler OnJoinRoom;

    protected override void Start()
    {
        _roomName = gameObject.name;
        base.Start();
    }

    protected override void OnClick()
    {
        OnJoinRoom?.Invoke(_roomName);
    }
}
