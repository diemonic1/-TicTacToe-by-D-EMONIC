using UnityEngine;

public class Initializer : MonoBehaviour
{
    [Header("Links to instances")]
    [SerializeField] private Menu menu;
    [SerializeField] private ServerWork serverWork;
    [SerializeField] private FieldView fieldView;
    [SerializeField] private NicknameHandler nicknameHandler;
    [SerializeField] private ButtonOnField[] buttonsOnField;
    private ListOfRoomsHandler listOfRoomsHandler;
    private PlayingField playingField;
    private ViewPresenter viewPresenter;
    private ServerPresenter serverPresenter;
    private PlayingFieldPresenter playingFieldPresenter;

    private void OnEnable()
    {
        playingField = new PlayingField();
        listOfRoomsHandler = new ListOfRoomsHandler(menu);
        viewPresenter = new ViewPresenter(menu, serverWork, nicknameHandler, playingField, buttonsOnField);
        serverPresenter = new ServerPresenter(menu, fieldView, serverWork, playingField, listOfRoomsHandler);
        playingFieldPresenter = new PlayingFieldPresenter(menu, fieldView, playingField, buttonsOnField);

        viewPresenter.Enable();
        serverPresenter.Enable();
        playingFieldPresenter.Enable();
    }

    private void OnDisable()
    {
        viewPresenter.Disable();
        serverPresenter.Disable();
        playingFieldPresenter.Disable();
    }
}
