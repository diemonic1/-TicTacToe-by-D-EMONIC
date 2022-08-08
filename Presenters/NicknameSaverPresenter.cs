public class NicknameSaverPresenter
{
    private readonly NicknameSaver nicknameSaver;

    private readonly ServerTransmitter serverTransmitter;

    public NicknameSaverPresenter(NicknameSaver nicknameSaver, ServerContainer serverContainer)
    {
        this.nicknameSaver = nicknameSaver;
        this.serverTransmitter = serverContainer.ServerTransmitter;
    }

    public void Enable()
    {
        nicknameSaver.OnNicknameChanged += ChangeNickname;
    }

    public void Disable()
    {
        nicknameSaver.OnNicknameChanged -= ChangeNickname;
    }

    private void ChangeNickname(string nickname)
    {
        serverTransmitter.SetNickname(nickname);
    }
}
