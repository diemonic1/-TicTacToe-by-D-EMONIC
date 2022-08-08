using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _gameFolder, _listOfRooms, _roomMenuElement, _restartButton;
    [SerializeField] private Text _secondPlayerNicknameText;

    public delegate void ButtonHandler(string roomName);

    public event ButtonHandler OnJoinRoom;

    public void SetVisibilityOfRestartButton(bool parametr)
    {
        _restartButton.SetActive(parametr);
    }

    public void PrepareForTheGame()
    {
        _listOfRooms.SetActive(false);
        _gameFolder.SetActive(true);
    }

    public void ShowNameOfSecondPlayer(string nicknameOfSecondPlayer)
    {
        _secondPlayerNicknameText.text = nicknameOfSecondPlayer + "\nс вами!";
    }

    public void ClearNameOfSecondPlayer()
    {
        _secondPlayerNicknameText.text = string.Empty;
    }

    public void BackToListOfRooms()
    {
        _gameFolder.SetActive(false);
        _listOfRooms.SetActive(true);
        _secondPlayerNicknameText.text = string.Empty;
    }

    public GameObject CreateRoomForList(int number, string name, int countOfPlayersInRoom)
    {
        GameObject room = Instantiate(_roomMenuElement, new Vector3(0, 0, 0), Quaternion.identity);

        room.transform.SetParent(_listOfRooms.transform, true);
        room.transform.localPosition = new Vector3(0, 525f - (number * 110), 0);
        room.transform.localScale = new Vector3(1, 1, 1);

        room.name = name;
        room.GetComponent<Text>().text = name + " " + countOfPlayersInRoom + "/2";

        room.GetComponent<JoinRoomButton>().OnJoinRoom += JoinRoom;

        return room;
    }

    public void DisableJoinToRoom(GameObject room)
    {
        room.GetComponent<Button>().interactable = false;
    }

    public void DestroyRoom(GameObject room)
    {
        Destroy(room);
    }

    private void JoinRoom(string roomName)
    {
        OnJoinRoom?.Invoke(roomName);
    }
}
