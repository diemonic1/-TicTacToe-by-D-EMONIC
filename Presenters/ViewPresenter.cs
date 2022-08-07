public class ViewPresenter
{
    private readonly Menu menu;
    private readonly ServerWork serverWork;
    private readonly NicknameHandler nicknameHandler;
    private readonly PlayingField playingField;
    private readonly ButtonOnField[] buttonsOnField;

    public ViewPresenter(Menu menu, ServerWork serverWork, NicknameHandler nicknameHandler, PlayingField playingField, ButtonOnField[] buttonsOnField)
    {
        this.menu = menu;
        this.serverWork = serverWork;
        this.nicknameHandler = nicknameHandler;
        this.playingField = playingField;
        this.buttonsOnField = buttonsOnField;
    }

    public void Enable()
    {
        menu.OnCreateRoom += CreateNetworkRoom;
        menu.OnRestartGame += RestartGame;
        menu.OnLeaveRoom += LeaveRoom;
        menu.OnJoinRoom += JoinRoom;

        nicknameHandler.OnNicknameChanged += ChangeNickname;

        for (int i = 0; i < 9; i++)
            buttonsOnField[i].OnButtonPressed += ButtonPressed;
    }

    public void Disable()
    {
        menu.OnCreateRoom -= CreateNetworkRoom;
        menu.OnRestartGame -= RestartGame;
        menu.OnLeaveRoom -= LeaveRoom;
        menu.OnJoinRoom -= JoinRoom;

        nicknameHandler.OnNicknameChanged -= ChangeNickname;

        for (int i = 0; i < 9; i++)
            buttonsOnField[i].OnButtonPressed -= ButtonPressed;
    }

    private void CreateNetworkRoom()
    {
        serverWork.CreateNetworkRoom();
    }

    private void RestartGame()
    {
        serverWork.RestartGameForEveryone();
    }

    private void LeaveRoom()
    {
        serverWork.LeaveRoom();
    }

    private void JoinRoom(string roomName)
    {
        menu.PrepareForTheGame();
        serverWork.TryJoinRoom(roomName);
    }

    private void ChangeNickname(string nickname)
    {
        serverWork.SetNickname(nickname);
    }

    private void ButtonPressed(int numberOfActivated)
    {
        bool result = TryPressButton(numberOfActivated);

        if (result)
            buttonsOnField[numberOfActivated].DiactivateButton();
    }

    private bool TryPressButton(int numberOfActivated)
    {
        if (playingField.CanButtonBePressed(serverWork.IsThisClientHost))
        {
            serverWork.SendNumberOfPressedButton(numberOfActivated);
            return true;
        }

        return false;
    }
}
