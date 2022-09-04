using UnityEngine;
using UnityEngine.UI;

public class FieldView : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonsOnField;
    [SerializeField] private GameObject[] _playingFieldImages_zeros;
    [SerializeField] private GameObject[] _playingFieldImages_crosses;

    [SerializeField] private Text _message;

    public void PutOnFieldZero(int numberOfActivated)
    {
        _playingFieldImages_zeros[numberOfActivated].SetActive(true);
    }

    public void PutOnFieldCross(int numberOfActivated)
    {
        _playingFieldImages_crosses[numberOfActivated].SetActive(true);
    }

    public void ShowMessage(string message)
    {
        _message.text = message;
    }

    public void ClearField()
    {
        for (int i = 0; i < 9; i++)
        {
            _buttonsOnField[i].GetComponent<Button>().interactable = true;
            _playingFieldImages_zeros[i].SetActive(false);
            _playingFieldImages_crosses[i].SetActive(false);
        }
    }

    public void DisablePlayingField()
    {
        for (int i = 0; i < 9; i++)
            _buttonsOnField[i].GetComponent<Button>().interactable = false;
    }
}
