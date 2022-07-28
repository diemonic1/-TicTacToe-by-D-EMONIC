using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Links to instances")]
    [SerializeField] private PlayingField playingField;
    [SerializeField] private Menu menu;
    [SerializeField] private ServerWork serverWork;

    #region calls from ServerWork
    public string GetLocalNickname()
    {
        return menu.Nickname;
    }

    public void BackToListOfRooms()
    {
        menu.BackToListOfRooms();
    }

    public void ShowNameOfSecondPlayer(string nicknameOfSeconddPlayer)
    {
        menu.ShowNameOfSecondPlayer(nicknameOfSeconddPlayer);
    }

    public void RefreshListOfRooms()
    {
        menu.RefreshListOfRooms();
    }

    public void PrepareRoom(bool isThisClientHost)
    {
        menu.ClearNameOfSecondPlayer();
        playingField.PrepareRoom(isThisClientHost);
    }

    public void UpdatePlayingField(int numberOfActivated, bool isThisClientHost)
    {
        playingField.UpdatePlayingField(numberOfActivated, isThisClientHost);
    }

    public void StartGame(bool isThisClientHost)
    {
        playingField.StartGame(isThisClientHost);
    }

    public void RestartLocalGame(bool isThisClientHost)
    {
        playingField.RestartLocalGame(isThisClientHost);
    }
    #endregion

    #region calls from Menu
    public void JoinRoom(string roomName)
    {
        menu.JoinRoom();
        serverWork.JoinRoom(roomName);
    }

    public void CreateRoom(string nickname)
    {
        serverWork.CreateRoom(nickname);
    }

    public void RestartGame()
    {
        serverWork.RestartGame();
    }

    public void LeaveRoom()
    {
        serverWork.LeaveRoom();
    }

    public int GetCountOfRooms()
    {
        return serverWork.GetCountOfRooms();
    }

    public string GetRoomName(int numberInList)
    {
        return serverWork.GetRoomName(numberInList);
    }

    public int GetCountOfPlayersInRoom(int numberInList)
    {
        return serverWork.GetCountOfPlayersInRoom(numberInList);
    }
    #endregion

    #region calls from GameLogicAndPlayingField
    public void SendNumberOfPressedButton(int numberOfActivated)
    {
        serverWork.SendNumberOfPressedButton(numberOfActivated);
    }

    public bool IsGameStart()
    {
        return serverWork.IsGameStarted;
    }

    public bool IsThisClientHost()
    {
        return serverWork.IsThisClientHost;
    }

    public void SetVisibilityOfRestartButton(bool parametr)
    {
        menu.SetVisibilityOfRestartButton(parametr);
    }
    #endregion
}
