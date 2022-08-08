public class FieldViewPresenter
{
    private readonly FieldView fieldView;

    private readonly FieldLogic fieldLogic;
    private readonly ServerWork serverWork;

    public FieldViewPresenter(FieldView fieldView, FieldLogic fieldLogic, ServerWork serverWork)
    {
        this.fieldView = fieldView;
        this.fieldLogic = fieldLogic;
        this.serverWork = serverWork;
    }

    public void Enable()
    {
        fieldLogic.OnFieldChanged += UpdateFieldView;
        fieldLogic.OnGameOver += GameOver;

        serverWork.OnPlayerLeftRoom += PrepareRoom;
        serverWork.OnGameStarted += StartGame;
        serverWork.OnServerFieldUpdated += UpdateFieldState;
    }

    public void Disable()
    {
        fieldLogic.OnFieldChanged -= UpdateFieldView;
        fieldLogic.OnGameOver -= GameOver;

        serverWork.OnPlayerLeftRoom -= PrepareRoom;
        serverWork.OnGameStarted -= StartGame;
        serverWork.OnServerFieldUpdated -= UpdateFieldState;
    }

    private void UpdateFieldView(int numberOfActivated, char sign, string message)
    {
        fieldView.UpdateFieldView(numberOfActivated, sign, message);
    }

    private void GameOver(string message)
    {
        fieldView.ShowGameOverScreen(message);
    }

    private void PrepareRoom()
    {
        fieldView.PrepareRoomForNewPlayer();
    }

    private void StartGame(bool isThisClientHost)
    {
        fieldLogic.RestartGame(isThisClientHost);
        fieldView.RestartGame(fieldLogic.GetNextStepMessage());
    }

    private void UpdateFieldState(int numberOfActivated)
    {
        fieldLogic.UpdateFieldState(numberOfActivated);
    }
}
