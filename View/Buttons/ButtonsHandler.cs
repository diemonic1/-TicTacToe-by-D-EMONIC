public class ButtonsHandler
{
    public ButtonsHandler(CreateRoomButton createRoomButton, RestartGameButton restartGameButton, LeaveRoomButton leaveRoomButton, FieldButton[] buttonsOnField)
    {
        CreateRoomButton = createRoomButton;
        RestartGameButton = restartGameButton;
        LeaveRoomButton = leaveRoomButton;
        ButtonsOnField = buttonsOnField;
    }

    public CreateRoomButton CreateRoomButton { get; private set; }

    public RestartGameButton RestartGameButton { get; private set; }

    public LeaveRoomButton LeaveRoomButton { get; private set; }

    public FieldButton[] ButtonsOnField { get; private set; }
}
