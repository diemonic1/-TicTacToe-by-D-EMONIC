using UnityEngine;
using UnityEngine.UI;

public class UpdateVisualisation : MonoBehaviour
{
    [SerializeField] private GameObject[] _playingFieldImages_zeros;
    [SerializeField] private GameObject[] _playingFieldImages_crosses;

    [SerializeField] private Text _title;

    public void waitForPlayer()
    {
        _title.text = "ожидаем второго игрока";
    }

    public void restartGameImages() 
    {
        for (int i = 0; i < 9; i++) 
        {
            _playingFieldImages_zeros[i].SetActive(false);
            _playingFieldImages_crosses[i].SetActive(false);
        }
    }

    public void updateImages(int numberOfActivated, int sign, int stepOfGame, bool isThisClientHost) 
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

    public void startGameVisual(bool isThisClientHost)
    {
        if (isThisClientHost)
            _title.text = "ход крестиков (ваш)";
        else 
            _title.text = "ход крестиков (не ваш)";
    }

    public void showZeroWinScreen()
    {
        _title.text = "нолики победили!";
    }

    public void showCrossWinScreen()
    {
        _title.text = "крестики победили!";
    }

    public void showDrawScreen()
    {
        _title.text = "ничья!";
    }
}
