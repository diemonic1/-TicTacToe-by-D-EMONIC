public class LeaveRoomButton : ButtonRoot
{
    public delegate void ButtonHandler();

    public event ButtonHandler OnLeaveRoom;

    protected override void OnClick()
    {
        OnLeaveRoom?.Invoke();
    }
}
