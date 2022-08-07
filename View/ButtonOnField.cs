using UnityEngine;
using UnityEngine.UI;

public class ButtonOnField : MonoBehaviour
{
    [SerializeField] private int _buttonNumber;
    private Button button;

    public delegate void ButtonHandler(int numberOfActivated);

    public event ButtonHandler OnButtonPressed;

    public void DiactivateButton()
    {
        button.interactable = false;
    }

    private void Start()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        OnButtonPressed?.Invoke(_buttonNumber);
    }
}
