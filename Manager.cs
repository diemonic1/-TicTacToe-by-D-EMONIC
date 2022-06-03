using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Links to instances")]
    [SerializeField] private PlayingField playingField;
    [SerializeField] private Menu menu;
    [SerializeField] private ServerWork serverWork;

    #region calls from ServerWork
    public string getLocalNickname()
    {
        return menu.Nickname;
    }

    public void backToListOfRooms()
    {
        menu.backToListOfRooms();
    }

    public void showNameOfSecondPlayer(string nicknameOfSeconddPlayer)
    {
        menu.showNameOfSecondPlayer(nicknameOfSeconddPlayer);
    }

    public void refreshListOfRooms()
    {
        menu.refreshListOfRooms();
    }

    public void prepareRoom(bool isThisClientHost)
    {
        menu.clearNameOfSecondPlayer();
        playingField.prepareRoom(isThisClientHost);
    }

    public void updatePlayingField(int numberOfActivated, bool isThisClientHost)
    {
        playingField.updatePlayingField(numberOfActivated, isThisClientHost);
    }

    public void startGame(bool isThisClientHost)
    {
        playingField.startGame(isThisClientHost);
    }

    public void restartLocalGame(bool isThisClientHost)
    {
        playingField.restartLocalGame(isThisClientHost);
    }
    #endregion

    #region calls from Menu
    public void joinRoom(string roomName)
    {
        menu.joinRoom();
        serverWork.joinRoom(roomName);
    }

    public void createRoom(string nickname)
    {
        serverWork.createRoom(nickname);
    }

    public void restartGame()
    {
        serverWork.restartGame();
    }

    public void leaveRoom()
    {
        serverWork.leaveRoom();
    }

    public int getCountOfRooms()
    {
        return serverWork.getCountOfRooms();
    }

    public string getRoomName(int numberInList)
    {
        return serverWork.getRoomName(numberInList);
    }

    public int getCountOfPlayersInRoom(int numberInList)
    {
        return serverWork.getCountOfPlayersInRoom(numberInList);
    }
    #endregion

    #region calls from GameLogicAndPlayingField
    public void sendNumberOfPressedButton(int numberOfActivated)
    {
        serverWork.sendNumberOfPressedButton(numberOfActivated);
    }

    public bool isGameStart()
    {
        return serverWork.IsGameStarted;
    }

    public bool isThisClientHost()
    {
        return serverWork.IsThisClientHost;
    }

    public void setVisibilityOfRestartButton(bool parametr)
    {
        menu.setVisibilityOfRestartButton(parametr);
    }
    #endregion
}
