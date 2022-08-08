using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [Header("Links to instances")]
    [SerializeField] private ButtonsInitializer buttonsInitializer;
    [SerializeField] private ServerInitializer serverInitializer;

    [SerializeField] private Menu menu;
    [SerializeField] private FieldView fieldView;
    [SerializeField] private NicknameSaver nicknameSaver;

    private FieldLogic fieldLogic;
    private ListOfRoomsHandler listOfRoomsHandler;

    private FieldViewPresenter fieldViewPresenter;
    private MenuPresenter menuPresenter;
    private NicknameSaverPresenter nicknameSaverPresenter;
    private ButtonsPresenter buttonsPresenter;

    private void OnEnable()
    {
        fieldLogic = new FieldLogic();
        listOfRoomsHandler = new ListOfRoomsHandler(menu);

        fieldViewPresenter = new FieldViewPresenter(fieldView, fieldLogic, serverInitializer.GetServerContainer());
        menuPresenter = new MenuPresenter(menu, fieldLogic, serverInitializer.GetServerContainer(), listOfRoomsHandler);
        nicknameSaverPresenter = new NicknameSaverPresenter(nicknameSaver, serverInitializer.GetServerContainer());
        buttonsPresenter = new ButtonsPresenter(buttonsInitializer.GetButtonsHandler(), fieldLogic, serverInitializer.GetServerContainer());

        fieldViewPresenter.Enable();
        menuPresenter.Enable();
        nicknameSaverPresenter.Enable();
        buttonsPresenter.Enable();
    }

    private void OnDisable()
    {
        fieldViewPresenter.Disable();
        menuPresenter.Disable();
        nicknameSaverPresenter.Disable();
        buttonsPresenter.Disable();
    }
}
