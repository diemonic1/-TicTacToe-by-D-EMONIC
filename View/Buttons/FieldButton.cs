using System;
using UnityEngine;
using UnityEngine.UI;

public class FieldButton : ButtonRoot
{
    [SerializeField] private int _buttonNumber;

    public FieldButton()
    {
        if (_buttonNumber < 0 || _buttonNumber > 8)
            throw new Exception("The button number on the field must be from 0 to 8!");
    }

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
