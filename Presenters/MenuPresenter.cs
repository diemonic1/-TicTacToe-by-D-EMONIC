public class MenuPresenter
{
    private readonly Menu menu;
    private readonly ListOfRoomsHandler listOfRoomsHandler;

    private readonly FieldLogic fieldLogic;
    private readonly ServerWork serverWork;

    public MenuPresenter(Menu menu, FieldLogic fieldLogic, ServerWork serverWork, ListOfRoomsHandler listOfRoomsHandler)
    {
        this.menu = menu;
        this.fieldLogic = fieldLogic;
        this.serverWork = serverWork;
        this.listOfRoomsHandler = listOfRoomsHandler;
    }

    public void Enable()
    {
        menu.OnJoinRoom += JoinRoom;

        fieldLogic.OnGameOver += EnableRestartButton;

        serverWork.OnListUpdated += RefreshListOfRooms;
        serverWork.OnPlayerLeftRoom += ClearRoom;
        serverWork.OnHostLeftRoom += BackToListOfRooms;
        serverWork.OnPlayerJoined += ShowNameOfSecondPlayer;
        serverWork.OnGameStarted += DisableRestartButton;
    }

    public void Disable()
    {
        menu.OnJoinRoom -= JoinRoom;

        fieldLogic.OnGameOver -= EnableRestartButton;

        serverWork.OnListUpdated -= RefreshListOfRooms;
        serverWork.OnPlayerLeftRoom -= ClearRoom;
        serverWork.OnHostLeftRoom -= BackToListOfRooms;
        serverWork.OnPlayerJoined -= ShowNameOfSecondPlayer;
        serverWork.OnGameStarted -= DisableRestartButton;
    }

    private void JoinRoom(string roomName)
    {
        menu.PrepareForTheGame();
        serverWork.TryJoinRoom(roomName);
    }

    private void EnableRestartButton(string message)
    {
        menu.SetVisibilityOfRestartButton(true);
    }

    private void RefreshListOfRooms(RoomInfo[] roomsList)
    {
        listOfRoomsHandler.RefreshListOfRooms(roomsList);
    }

    private void ClearRoom()
    {
        menu.ClearNameOfSecondPlayer();
        menu.SetVisibilityOfRestartButton(false);
    }

    private void BackToListOfRooms()
    {
        menu.BackToListOfRooms();
    }

    private void ShowNameOfSecondPlayer(string nickname)
    {
        menu.ShowNameOfSecondPlayer(nickname);
    }

    private void DisableRestartButton(bool isThisClientHost)
    {
        menu.SetVisibilityOfRestartButton(false);
    }
}
