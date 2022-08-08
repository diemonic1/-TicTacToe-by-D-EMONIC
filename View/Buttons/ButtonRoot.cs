using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonRoot : MonoBehaviour
{
    protected virtual void Start()
    {
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
    }
}
