using UnityEngine;
using UnityEngine.UI;

public class FieldView : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonsOnField;
    [SerializeField] private GameObject[] _playingFieldImages_zeros;
    [SerializeField] private GameObject[] _playingFieldImages_crosses;

    [SerializeField] private Text _message;

    public void PrepareRoomForNewPlayer()
    {
        ClearField();

        _message.text = "ожидаем второго игрока";
    }

    public void RestartGame(string message)
    {
        ClearField();

        _message.text = message;
    }

    public void UpdateFieldView(int numberOfActivated, char sign, string message)
    {
        if (sign == '0')
            _playingFieldImages_zeros[numberOfActivated].SetActive(true);
        else
            _playingFieldImages_crosses[numberOfActivated].SetActive(true);

        _message.text = message;
    }

    public void ShowGameOverScreen(string message)
    {
        _message.text = message;
        DisablePlayingField();
    }

    private void DisablePlayingField()
    {
        for (int i = 0; i < 9; i++)
            _buttonsOnField[i].GetComponent<Button>().interactable = false;
    }

    private void ClearField()
    {
        for (int i = 0; i < 9; i++)
        {
            _buttonsOnField[i].GetComponent<Button>().interactable = true;
            _playingFieldImages_zeros[i].SetActive(false);
            _playingFieldImages_crosses[i].SetActive(false);
        }
    }
}
