using UnityEngine;
using UnityEngine.UI;

public class FieldButton : ButtonRoot
{
    [SerializeField] private int _buttonNumber;

    public delegate void ButtonHandler(int numberOfActivated);

    public event ButtonHandler OnButtonPressed;

    public void DiactivateButton()
    {
        this.GetComponent<Button>().interactable = false;
    }

    protected override void OnClick()
    {
        OnButtonPressed?.Invoke(_buttonNumber);
    }
}
