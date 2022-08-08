using UnityEngine;

public class Initializer : MonoBehaviour
{
    [Header("Links to instances")]
    [SerializeField] private ButtonsInitializer buttonsInitializer;

    [SerializeField] private Menu menu;
    [SerializeField] private ServerWork serverWork;
    [SerializeField] private FieldView fieldView;
    [SerializeField] private NicknameHandler nicknameHandler;

    private FieldLogic fieldLogic;
    private ListOfRoomsHandler listOfRoomsHandler;

    private FieldViewPresenter fieldViewPresenter;
    private MenuPresenter menuPresenter;
    private NicknameHandlerPresenter nicknameHandlerPresenter;
    private ButtonsPresenter buttonsPresenter;

    private void OnEnable()
    {
        fieldLogic = new FieldLogic();
        listOfRoomsHandler = new ListOfRoomsHandler(menu);

        fieldViewPresenter = new FieldViewPresenter(fieldView, fieldLogic, serverWork);
        menuPresenter = new MenuPresenter(menu, fieldLogic, serverWork, listOfRoomsHandler);
        nicknameHandlerPresenter = new NicknameHandlerPresenter(nicknameHandler, serverWork);
        buttonsPresenter = new ButtonsPresenter(buttonsInitializer.GetButtonsHandler(), fieldLogic, serverWork);

        fieldViewPresenter.Enable();
        menuPresenter.Enable();
        nicknameHandlerPresenter.Enable();
        buttonsPresenter.Enable();
    }

    private void OnDisable()
    {
        fieldViewPresenter.Disable();
        menuPresenter.Disable();
        nicknameHandlerPresenter.Disable();
        buttonsPresenter.Disable();
    }
}
