public class ButtonsPresenter
{
    private readonly FieldButton[] buttonsOnField;
    private readonly CreateRoomButton createRoomButton;
    private readonly RestartGameButton restartGameButton;
    private readonly LeaveRoomButton leaveRoomButton;

    private readonly FieldLogic fieldLogic;
    private readonly ServerWork serverWork;

    public ButtonsPresenter(ButtonsHandler buttonsHandler, FieldLogic fieldLogic, ServerWork serverWork)
    {
        createRoomButton = buttonsHandler.CreateRoomButton;
        buttonsOnField = buttonsHandler.ButtonsOnField;
        restartGameButton = buttonsHandler.RestartGameButton;
        leaveRoomButton = buttonsHandler.LeaveRoomButton;

        this.fieldLogic = fieldLogic;
        this.serverWork = serverWork;
    }

    public void Enable()
    {
        createRoomButton.OnCreateRoom += CreateNetworkRoom;
        restartGameButton.OnRestartGame += RestartGame;
        leaveRoomButton.OnLeaveRoom += LeaveRoom;

        for (int i = 0; i < 9; i++)
            buttonsOnField[i].OnButtonPressed += ButtonPressed;

        fieldLogic.OnFieldChanged += DiactivateButton;
    }

    public void Disable()
    {
        createRoomButton.OnCreateRoom -= CreateNetworkRoom;
        restartGameButton.OnRestartGame -= RestartGame;
        leaveRoomButton.OnLeaveRoom -= LeaveRoom;

        for (int i = 0; i < 9; i++)
            buttonsOnField[i].OnButtonPressed -= ButtonPressed;

        fieldLogic.OnFieldChanged -= DiactivateButton;
    }

    private void CreateNetworkRoom()
    {
        serverWork.CreateNetworkRoom();
    }

    private void LeaveRoom()
    {
        serverWork.LeaveRoom();
    }

    private void RestartGame()
    {
        serverWork.RestartGameForEveryone();
    }

    private void ButtonPressed(int numberOfActivated)
    {
        bool result = TryPressButton(numberOfActivated);

        if (result)
            buttonsOnField[numberOfActivated].DiactivateButton();
    }

    private bool TryPressButton(int numberOfActivated)
    {
        if (fieldLogic.CanButtonBePressed())
        {
            serverWork.SendNumberOfPressedButton(numberOfActivated);
            return true;
        }

        return false;
    }

    private void DiactivateButton(int numberOfActivated, char sign, string message)
    {
        buttonsOnField[numberOfActivated].DiactivateButton();
    }
}
