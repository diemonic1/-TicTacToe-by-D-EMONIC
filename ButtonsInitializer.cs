using UnityEngine;

public class ButtonsInitializer : MonoBehaviour
{
    [SerializeField] private CreateRoomButton createRoomButton;
    [SerializeField] private RestartGameButton restartGameButton;
    [SerializeField] private LeaveRoomButton leaveRoomButton;
    [SerializeField] private FieldButton[] buttonsOnField;

    private ButtonsHandler buttonsHandler;

    public ButtonsHandler GetButtonsHandler()
    {
        buttonsHandler = new ButtonsHandler(createRoomButton, restartGameButton, leaveRoomButton, buttonsOnField);

        return buttonsHandler;
    }
}
