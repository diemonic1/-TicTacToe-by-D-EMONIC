public class RestartGameButton : ButtonRoot
{
    public delegate void ButtonHandler();

    public event ButtonHandler OnRestartGame;

    protected override void OnClick()
    {
        OnRestartGame?.Invoke();
    }
}
