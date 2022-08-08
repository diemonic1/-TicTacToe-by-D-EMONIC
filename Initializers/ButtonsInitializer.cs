using UnityEngine;

public class ButtonsInitializer : MonoBehaviour
{
    [SerializeField] private CreateRoomButton createRoomButton;
    [SerializeField] private RestartGameButton restartGameButton;
    [SerializeField] private LeaveRoomButton leaveRoomButton;
    [SerializeField] private FieldButton[] buttonsOnField;

    private ButtonsContainer buttonsContainer;

    public ButtonsContainer GetButtonsHandler()
    {
        buttonsContainer = new ButtonsContainer(createRoomButton, restartGameButton, leaveRoomButton, buttonsOnField);

        return buttonsContainer;
    }
}
