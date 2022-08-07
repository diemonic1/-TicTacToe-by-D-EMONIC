using UnityEngine;
using UnityEngine.UI;

public class NicknameHandler : MonoBehaviour
{
    [SerializeField] private InputField _nicknameTextInputField;
    private string _nickname;

    public delegate void ChangeNickname(string nickname);

    public event ChangeNickname OnNicknameChanged;

    public void SaveNickname()
    {
        _nickname = _nicknameTextInputField.text;
        PlayerPrefs.SetString("Nickname", _nickname);
        OnNicknameChanged?.Invoke(_nickname);
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("Nickname") != string.Empty)
            _nicknameTextInputField.text = PlayerPrefs.GetString("Nickname");
        else
            _nicknameTextInputField.text = "player";
    }
}
