public class PlayingField
{
    private int[,] _playingField;
    private int _stepOfGame;

    public PlayingField()
    {
        ClearPlayingField();
    }

    public delegate void FieldHandler(int numberOfActivated, int sign, string message);

    public delegate void GameHandler(string message);

    public event FieldHandler OnFieldChanged;

    public event GameHandler OnGameOver;

    public bool CanButtonBePressed(bool isThisClientHost) => (isThisClientHost && _stepOfGame % 2 == 0) || (!isThisClientHost && _stepOfGame % 2 == 1);

    public void RestartGame()
    {
        _stepOfGame = 0;
        ClearPlayingField();
    }

    public string GetNextStepMessage(bool isThisClientHost)
    {
        int nextStepOfGame = _stepOfGame + 1;

        if (nextStepOfGame % 2 == 0 && isThisClientHost)
            return "ход ноликов (не ваш)";
        else if (nextStepOfGame % 2 == 0 && !isThisClientHost)
            return "ход ноликов (ваш)";
        else if (nextStepOfGame % 2 == 1 && isThisClientHost)
            return "ход крестиков (ваш)";
        else
            return "ход крестиков (не ваш)";
    }

    public void UpdatePlayingField(int numberOfActivated, bool isThisClientHost)
    {
        _stepOfGame += 1;

        int yPosition = numberOfActivated / 3;
        int xPosition = numberOfActivated % 3;

        int sign;

        if (_stepOfGame % 2 == 0)
            sign = 0;
        else
            sign = 1;

        _playingField[yPosition, xPosition] = sign;

        string message = GetNextStepMessage(isThisClientHost);

        OnFieldChanged?.Invoke(numberOfActivated, sign, message);

        CheckFieldOnWinLine();
    }

    private void ClearPlayingField()
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
        int winnerSign = -1;

        for (int i = 0; i < 3; i++)
        {
            if (_playingField[i, 0] + _playingField[i, 1] + _playingField[i, 2] == 0)
                winnerSign = 0;
            else if (_playingField[i, 0] + _playingField[i, 1] + _playingField[i, 2] == 3)
                winnerSign = 1;
            else if (_playingField[0, i] + _playingField[1, i] + _playingField[2, i] == 0)
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
            WinnerFound(winnerSign);
        else if (_stepOfGame == 9)
            WinnerFound(winnerSign);
    }

    private void WinnerFound(int winnerSign)
    {
        string message;

        if (winnerSign == 0)
            message = "нолики победили!";
        else if (winnerSign == 1)
            message = "крестики победили!";
        else
            message = "ничья!";

        OnGameOver?.Invoke(message);
    }
}
