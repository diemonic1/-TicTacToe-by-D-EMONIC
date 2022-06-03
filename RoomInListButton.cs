using UnityEngine;
using UnityEngine.UI;

public class RoomInListButton : MonoBehaviour
{
    private string _name;

    [Header("Links to instances")]
    private Manager manager;

    private void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
    }
    //
    private void Start()
    {
        _name = gameObject.name;
        Button button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        manager.joinRoom(_name);
    }
}
