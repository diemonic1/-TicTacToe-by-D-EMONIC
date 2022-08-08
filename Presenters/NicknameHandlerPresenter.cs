public class NicknameHandlerPresenter
{
    private readonly NicknameHandler nicknameHandler;

    private readonly ServerWork serverWork;

    public NicknameHandlerPresenter(NicknameHandler nicknameHandler, ServerWork serverWork)
    {
        this.nicknameHandler = nicknameHandler;
        this.serverWork = serverWork;
    }

    public void Enable()
    {
        nicknameHandler.OnNicknameChanged += ChangeNickname;
    }

    public void Disable()
    {
        nicknameHandler.OnNicknameChanged -= ChangeNickname;
    }

    private void ChangeNickname(string nickname)
    {
        serverWork.SetNickname(nickname);
    }
}
