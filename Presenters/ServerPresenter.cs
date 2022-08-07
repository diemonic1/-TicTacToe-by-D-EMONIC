public class ServerPresenter
{
    private readonly Menu menu;
    private readonly FieldView fieldView;
    private readonly ServerWork serverWork;
    private readonly PlayingField playingField;
    private readonly ListOfRoomsHandler listOfRoomsHandler;

    public ServerPresenter(Menu menu, FieldView fieldView, ServerWork serverWork, PlayingField playingField, ListOfRoomsHandler listOfRoomsHandler)
    {
        this.menu = menu;
        this.fieldView = fieldView;
        this.serverWork = serverWork;
        this.playingField = playingField;
        this.listOfRoomsHandler = listOfRoomsHandler;
    }

    public void Enable()
    {
        serverWork.OnListUpdated += RefreshListOfRooms;
        serverWork.OnPlayerLeftRoom += ClearRoom;
        serverWork.OnHostLeftRoom += BackToListOfRooms;
        serverWork.OnPlayerJoined += ShowNameOfSecondPlayer;
        serverWork.OnGameStarted += StartGame;
        serverWork.OnServerFieldUpdated += UpdatePlayingField;
    }

    public void Disable()
    {
        serverWork.OnListUpdated -= RefreshListOfRooms;
        serverWork.OnPlayerLeftRoom -= ClearRoom;
        serverWork.OnHostLeftRoom -= BackToListOfRooms;
        serverWork.OnPlayerJoined -= ShowNameOfSecondPlayer;
        serverWork.OnGameStarted -= StartGame;
        serverWork.OnServerFieldUpdated -= UpdatePlayingField;
    }

    private void RefreshListOfRooms(RoomInfo[] roomsList)
    {
        listOfRoomsHandler.RefreshListOfRooms(roomsList);
    }

    private void ClearRoom()
    {
        menu.ClearNameOfSecondPlayer();
        menu.SetVisibilityOfRestartButton(false);

        fieldView.PrepareRoomForNewPlayer();
    }

    private void BackToListOfRooms()
    {
        menu.BackToListOfRooms();
    }

    private void ShowNameOfSecondPlayer(string nickname)
    {
        menu.ShowNameOfSecondPlayer(nickname);
    }

    private void StartGame(bool isThisClientHost)
    {
        playingField.RestartGame();
        fieldView.RestartGame(playingField.GetNextStepMessage(isThisClientHost));
        menu.SetVisibilityOfRestartButton(false);
    }

    private void UpdatePlayingField(int numberOfActivated, bool isThisClientHost)
    {
        playingField.UpdatePlayingField(numberOfActivated, isThisClientHost);
    }
}
