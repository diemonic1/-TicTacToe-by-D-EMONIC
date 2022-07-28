using UnityEngine;
using UnityEngine.UI;

public class PlayingField : Photon.MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonsOnField;

    private int[,] _playingField;
    private bool _isWin;

    [Header("Links to instances")]
    [SerializeField] private Manager manager;
    [SerializeField] private UpdateVisualisation updateVisualisation;

    public int StepOfGame { get; private set; }

    public void PrepareRoom(bool isThisClientHost)
    {
        RestartLocalGame(isThisClientHost);
        updateVisualisation.WaitForPlayer();
    }

    public void RestartLocalGame(bool isThisClientHost)
    {
        StepOfGame = 0;
        _isWin = false;
        manager.SetVisibilityOfRestartButton(false);
        ClearField();

        for (int i = 0; i < 9; i++)
            _buttonsOnField[i].GetComponent<Button>().interactable = true;

        updateVisualisation.RestartGameImages();
        updateVisualisation.StartGameVisual(isThisClientHost);
    }

    public void StartGame(bool isThisClientHost)
    {
        updateVisualisation.StartGameVisual(isThisClientHost);
    }

    public void SendNumberOfPressedButton(int numberOfActivate)
    {
        if (_buttonsOnField[numberOfActivate].GetComponent<Button>().interactable && manager.IsGameStart() && !_isWin && ((manager.IsThisClientHost() && StepOfGame % 2 == 0) || (!manager.IsThisClientHost() && StepOfGame % 2 == 1)))
            manager.SendNumberOfPressedButton(numberOfActivate);
    }

    public void UpdatePlayingField(int numberOfActivate, bool isThisClientHost)
    {
        _buttonsOnField[numberOfActivate].GetComponent<Button>().interactable = false;

        if (!_isWin)
        {
            int yPosition = numberOfActivate / 3;
            int xPosition = numberOfActivate % 3;

            int sign;

            if (StepOfGame % 2 == 0)
                sign = 1;
            else
                sign = 0;

            _playingField[yPosition, xPosition] = sign;

            updateVisualisation.UpdateImages(numberOfActivate, sign, StepOfGame, isThisClientHost);

            StepOfGame += 1;

            CheckFieldOnWinLine();
        }
    }

    private void Start()
    {
        ClearField();
    }

    private void ClearField()
    {
        _playingField = new int[3, 3]
        {
            { -10, -10, -10 },
            { -10, -10, -10 },
            { -10, -10, -10 },
        };
    }

    private void CheckFieldOnWinLine()
    {
        // 0 - zeros, 1 - crosses, -1 - draw
        int winnerSign = -10;

        for (int i = 0; i < 3; i++)
        {
            if (_playingField[i, 0] + _playingField[i, 1] + _playingField[i, 2] == 0)
                winnerSign = 0;
            else if (_playingField[i, 0] + _playingField[i, 1] + _playingField[i, 2] == 3)
                winnerSign = 1;
        }

        for (int i = 0; i < 3; i++)
        {
            if (_playingField[0, i] + _playingField[1, i] + _playingField[2, i] == 0)
                winnerSign = 0;
            else if (_playingField[0, i] + _playingField[1, i] + _playingField[2, i] == 3)
                winnerSign = 1;
        }

        if (_playingField[0, 0] + _playingField[1, 1] + _playingField[2, 2] == 0)
            winnerSign = 0;
        else if (_playingField[0, 0] + _playingField[1, 1] + _playingField[2, 2] == 3)
            winnerSign = 1;
        else if (_playingField[0, 2] + _playingField[1, 1] + _playingField[2, 0] == 0)
            winnerSign = 0;
        else if (_playingField[0, 2] + _playingField[1, 1] + _playingField[2, 0] == 3)
            winnerSign = 1;

        if (winnerSign >= 0)
            GameOver(winnerSign);
        else if (StepOfGame == 9 && _isWin == false)
            GameOver(-1);
    }

    private void GameOver(int winnerSign)
    {
        _isWin = true;
        manager.SetVisibilityOfRestartButton(true);

        if (winnerSign == 0)
            updateVisualisation.ShowZeroWinScreen();
        else if (winnerSign == 1)
            updateVisualisation.ShowCrossWinScreen();
        else
            updateVisualisation.ShowDrawScreen();
    }
}
