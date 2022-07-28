using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _gameFolder, _listOfRooms, _roomMenuElement, _restartButton;
    [SerializeField] private Text _secondPlayerNicknameText;
    [SerializeField] private InputField _nicknameTextInputField;

    private int _countOfRooms;
    private GameObject[] _roomsList = new GameObject[10];

    [Header("Links to instances")]
    [SerializeField] private Manager manager;

    public string Nickname { get; private set; }

    public void SetVisibilityOfRestartButton(bool parametr)
    {
        _restartButton.SetActive(parametr);
    }

    public void OpenListOfRooms()
    {
        Nickname = _nicknameTextInputField.text;
        PlayerPrefs.SetString("Nickname", Nickname);
    }

    public void JoinRoom()
    {
        _listOfRooms.SetActive(false);
        _gameFolder.SetActive(true);
    }

    public void CreateRoom()
    {
        manager.CreateRoom(Nickname);
    }

    public void RestartGame()
    {
        manager.RestartGame();
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

        manager.LeaveRoom();
    }

    public void RefreshListOfRooms()
    {
        ClearListOfRooms();

        _countOfRooms = manager.GetCountOfRooms();

        for (int i = 0; i < _countOfRooms; i++)
        {
            GameObject currentRoom = Instantiate(_roomMenuElement, new Vector3(0, 0, 0), Quaternion.identity);

            currentRoom.transform.SetParent(_listOfRooms.transform, true);
            currentRoom.transform.localPosition = new Vector3(0, 525f - (i * 110), 0);
            currentRoom.transform.localScale = new Vector3(1, 1, 1);

            _roomsList[i] = currentRoom;

            string currentName = manager.GetRoomName(i);
            int currentCountOfPlayersInRoom = manager.GetCountOfPlayersInRoom(i);

            currentRoom.name = currentName;
            currentRoom.GetComponent<Text>().text = currentName + " " + currentCountOfPlayersInRoom + "/2";

            if (currentCountOfPlayersInRoom == 2)
                currentRoom.GetComponent<Button>().interactable = false;
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("Nickname") != string.Empty)
            _nicknameTextInputField.text = PlayerPrefs.GetString("Nickname");
        else
            _nicknameTextInputField.text = "player";
    }

    private void ClearListOfRooms()
    {
        for (int i = 0; i < _countOfRooms; i++)
        {
            if (_roomsList[i] != null)
                Destroy(_roomsList[i]);
        }
    }
}
