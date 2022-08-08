public class CreateRoomButton : ButtonRoot
{
    public delegate void ButtonHandler();

    public event ButtonHandler OnCreateRoom;

    protected override void OnClick()
    {
        OnCreateRoom?.Invoke();
    }
}
