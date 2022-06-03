using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public string Nickname { get; private set; }

    [SerializeField] private GameObject _gameFolder, _listOfRooms, _roomMenuElement, _restartButton;
    [SerializeField] private Text _secondPlayerNicknameText;
    [SerializeField] private InputField _nicknameTextInputField;

    private int _countOfRooms;
    private GameObject[] _roomsList = new GameObject[10];

    [Header("Links to instances")]
    [SerializeField] private Manager manager;

    private void Start()
    {
        if (PlayerPrefs.GetString("Nickname") != "")
            _nicknameTextInputField.text = PlayerPrefs.GetString("Nickname");
        else
            _nicknameTextInputField.text = "player";
    }

    public void setVisibilityOfRestartButton(bool parametr)
    {
        _restartButton.SetActive(parametr);
    }

    public void openListOfRooms()
    {
        Nickname = _nicknameTextInputField.text;
        PlayerPrefs.SetString("Nickname", Nickname);
    }

    public void joinRoom()
    {
        _listOfRooms.SetActive(false);
        _gameFolder.SetActive(true);
    }

    public void createRoom()
    {
        manager.createRoom(Nickname);
    }

    public void restartGame()
    {
        manager.restartGame();
    }

    public void showNameOfSecondPlayer(string nicknameOfSecondPlayer)
    {
        _secondPlayerNicknameText.text = nicknameOfSecondPlayer + "\nс вами!";
    }

    public void clearNameOfSecondPlayer()
    {
        _secondPlayerNicknameText.text = "";
    }

    public void backToListOfRooms()
    {
        _gameFolder.SetActive(false);
        _listOfRooms.SetActive(true);
        _secondPlayerNicknameText.text = "";

        manager.leaveRoom();
    }

    public void refreshListOfRooms() 
    {
        clearListOfRooms();

        _countOfRooms = manager.getCountOfRooms();

        for (int i = 0; i < _countOfRooms; i++)
        {
            GameObject currentRoom = Instantiate(_roomMenuElement, new Vector3(0, 0, 0), Quaternion.identity);

            currentRoom.transform.SetParent(_listOfRooms.transform, true);
            currentRoom.transform.localPosition = new Vector3(0, 525f - i * 110, 0);
            currentRoom.transform.localScale = new Vector3(1, 1, 1);

            _roomsList[i] = currentRoom;

            string currentName = manager.getRoomName(i);
            int currentCountOfPlayersInRoom = manager.getCountOfPlayersInRoom(i);

            currentRoom.name = currentName;
            currentRoom.GetComponent<Text>().text = currentName + " " + currentCountOfPlayersInRoom + "/2";

            if (currentCountOfPlayersInRoom == 2)
                currentRoom.GetComponent<Button>().interactable = false;
        }
    }

    private void clearListOfRooms() 
    {
        for (int i = 0; i < _countOfRooms; i++)
        {
            if (_roomsList[i] != null) 
                Destroy(_roomsList[i]);
        }
    }
}
