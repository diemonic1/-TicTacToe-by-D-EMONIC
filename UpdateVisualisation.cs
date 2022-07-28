using UnityEngine;
using UnityEngine.UI;

public class UpdateVisualisation : MonoBehaviour
{
    [SerializeField] private GameObject[] _playingFieldImages_zeros;
    [SerializeField] private GameObject[] _playingFieldImages_crosses;

    [SerializeField] private Text _title;

    public void WaitForPlayer()
    {
        _title.text = "ожидаем второго игрока";
    }

    public void RestartGameImages()
    {
        for (int i = 0; i < 9; i++)
        {
            _playingFieldImages_zeros[i].SetActive(false);
            _playingFieldImages_crosses[i].SetActive(false);
        }
    }

    public void UpdateImages(int numberOfActivated, int sign, int stepOfGame, bool isThisClientHost)
    {
        if (sign == 0)
            _playingFieldImages_zeros[numberOfActivated].SetActive(true);
        else
            _playingFieldImages_crosses[numberOfActivated].SetActive(true);

        if (stepOfGame % 2 == 0 && isThisClientHost)
            _title.text = "ход ноликов (не ваш)";
        else if (stepOfGame % 2 == 0 && isThisClientHost == false)
            _title.text = "ход ноликов (ваш)";
        else if (stepOfGame % 2 == 1 && isThisClientHost)
            _title.text = "ход крестиков (ваш)";
        else if (stepOfGame % 2 == 1 && isThisClientHost == false)
            _title.text = "ход крестиков (не ваш)";
    }

    public void StartGameVisual(bool isThisClientHost)
    {
        if (isThisClientHost)
            _title.text = "ход крестиков (ваш)";
        else
            _title.text = "ход крестиков (не ваш)";
    }

    public void ShowZeroWinScreen()
    {
        _title.text = "нолики победили!";
    }

    public void ShowCrossWinScreen()
    {
        _title.text = "крестики победили!";
    }

    public void ShowDrawScreen()
    {
        _title.text = "ничья!";
    }
}
