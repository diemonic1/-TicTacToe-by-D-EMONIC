public class MenuPresenter
{
    private readonly Menu menu;
    private readonly ListOfRoomsHandler listOfRoomsHandler;

    private readonly FieldLogic fieldLogic;
    private readonly ServerTransmitter serverTransmitter;
    private readonly ServerEvents serverEvents;
    private readonly ServerCalls serverCalls;

    public MenuPresenter(Menu menu, FieldLogic fieldLogic, ServerContainer serverContainer, ListOfRoomsHandler listOfRoomsHandler)
    {
        this.menu = menu;
        this.fieldLogic = fieldLogic;
        this.serverTransmitter = serverContainer.ServerTransmitter;
        this.serverEvents = serverContainer.ServerEvents;
        this.serverCalls = serverContainer.ServerCalls;
        this.listOfRoomsHandler = listOfRoomsHandler;
    }

    public void Enable()
    {
        menu.OnJoinRoom += JoinRoom;

        fieldLogic.OnGameOver += EnableRestartButton;

        serverTransmitter.OnPlayerLeftRoom += ClearRoom;
        serverTransmitter.OnHostLeftRoom += BackToListOfRooms;

        serverEvents.OnListUpdated += RefreshListOfRooms;

        serverCalls.OnPlayerJoined += ShowNameOfSecondPlayer;
        serverCalls.OnGameStarted += DisableRestartButton;
    }

    public void Disable()
    {
        menu.OnJoinRoom -= JoinRoom;

        fieldLogic.OnGameOver -= EnableRestartButton;

        serverTransmitter.OnPlayerLeftRoom -= ClearRoom;
        serverTransmitter.OnHostLeftRoom -= BackToListOfRooms;

        serverEvents.OnListUpdated -= RefreshListOfRooms;

        serverCalls.OnPlayerJoined -= ShowNameOfSecondPlayer;
        serverCalls.OnGameStarted -= DisableRestartButton;
    }

    private void JoinRoom(string roomName)
    {
        menu.PrepareForTheGame();
        serverTransmitter.TryJoinRoom(roomName);
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
        menu.ShowNameOfSecondPlayer(nickname + "\nс вами!");
    }

    private void DisableRestartButton(bool isThisClientHost)
    {
        menu.SetVisibilityOfRestartButton(false);
    }
}
