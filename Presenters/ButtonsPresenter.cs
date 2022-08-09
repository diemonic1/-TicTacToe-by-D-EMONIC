public class ButtonsPresenter
{
    private readonly FieldButton[] buttonsOnField;
    private readonly CreateRoomButton createRoomButton;
    private readonly RestartGameButton restartGameButton;
    private readonly LeaveRoomButton leaveRoomButton;

    private readonly FieldLogic fieldLogic;
    private readonly ServerTransmitter serverTransmitter;

    public ButtonsPresenter(ButtonsContainer buttonsContainer, FieldLogic fieldLogic, ServerContainer serverContainer)
    {
        createRoomButton = buttonsContainer.CreateRoomButton;
        restartGameButton = buttonsContainer.RestartGameButton;
        leaveRoomButton = buttonsContainer.LeaveRoomButton;
        buttonsOnField = buttonsContainer.ButtonsOnField;

        this.fieldLogic = fieldLogic;
        this.serverTransmitter = serverContainer.ServerTransmitter;
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
        serverTransmitter.TryCreateNetworkRoom();
    }

    private void LeaveRoom()
    {
        serverTransmitter.LeaveRoom();
    }

    private void RestartGame()
    {
        serverTransmitter.RestartGameForEveryone();
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
            serverTransmitter.SendNumberOfPressedButton(numberOfActivated);
            return true;
        }

        return false;
    }

    private void DiactivateButton(int numberOfActivated, char sign, string message)
    {
        buttonsOnField[numberOfActivated].DiactivateButton();
    }
}
