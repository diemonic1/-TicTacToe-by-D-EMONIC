using UnityEngine;
using UnityEngine.UI;

public class RoomInListButton : MonoBehaviour
{
    private string _roomName;

    public delegate void ButtonHandler(string roomName);

    public event ButtonHandler OnJoinRoom;

    private void Start()
    {
        _roomName = gameObject.name;
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        OnJoinRoom?.Invoke(_roomName);
    }
}
