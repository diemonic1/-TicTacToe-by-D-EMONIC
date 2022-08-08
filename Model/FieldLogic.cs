public class FieldLogic
{
    private char[,] _playingField;

    private int _stepOfGame;
    private bool _isThisMyStep;
    private bool _isGameStarted;

    public FieldLogic()
    {
        ClearPlayingField();
    }

    public delegate void FieldHandler(int numberOfActivated, char sign, string message);

    public delegate void GameHandler(string message);

    public event FieldHandler OnFieldChanged;

    public event GameHandler OnGameOver;

    public bool CanButtonBePressed() => _isGameStarted && _isThisMyStep;

    public void RestartGame(bool isThisClientHost)
    {
        _stepOfGame = 0;
        _isGameStarted = true;
        _isThisMyStep = isThisClientHost;
        ClearPlayingField();
    }

    public string GetNextStepMessage()
    {
        int nextStepOfGame = _stepOfGame + 1;

        string message;

        if (nextStepOfGame % 2 == 0)
            message = "ход ноликов ";
        else
            message = "ход крестиков ";

        if (_isThisMyStep)
            message += "(ваш)";
        else
            message += "(не ваш)";

        return message;
    }

    public void UpdateFieldState(int numberOfActivated)
    {
        _stepOfGame += 1;
        _isThisMyStep = !_isThisMyStep;

        int yPosition = numberOfActivated / 3;
        int xPosition = numberOfActivated % 3;

        char sign;

        if (_stepOfGame % 2 == 0)
            sign = '0';
        else
            sign = 'X';

        _playingField[yPosition, xPosition] = sign;

        OnFieldChanged?.Invoke(numberOfActivated, sign, GetNextStepMessage());

        CheckFieldOnWinLine();
    }

    private void ClearPlayingField()
    {
        _playingField = new char[3, 3]
        {
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
        };
    }

    private void CheckFieldOnWinLine()
    {
        if (IsThisSignWinner('0'))
            WinnerFound('0');
        else if (IsThisSignWinner('X'))
            WinnerFound('X');
        else if (_stepOfGame == 9)
            WinnerFound('-');
    }

    private bool IsThisSignWinner(char sign)
    {
        return
                (_playingField[0, 0] == sign && _playingField[0, 1] == sign && _playingField[0, 2] == sign) ||
                (_playingField[1, 0] == sign && _playingField[1, 1] == sign && _playingField[1, 2] == sign) ||
                (_playingField[2, 0] == sign && _playingField[2, 1] == sign && _playingField[2, 2] == sign) ||
                (_playingField[0, 0] == sign && _playingField[1, 0] == sign && _playingField[2, 0] == sign) ||
                (_playingField[0, 1] == sign && _playingField[1, 1] == sign && _playingField[2, 1] == sign) ||
                (_playingField[0, 2] == sign && _playingField[1, 2] == sign && _playingField[2, 2] == sign) ||
                (_playingField[0, 0] == sign && _playingField[1, 1] == sign && _playingField[2, 2] == sign) ||
                (_playingField[0, 2] == sign && _playingField[1, 1] == sign && _playingField[2, 0] == sign)
            ;
    }

    private void WinnerFound(char sign)
    {
        _isGameStarted = false;

        string message;

        if (sign == '0')
            message = "нолики победили!";
        else if (sign == 'X')
            message = "крестики победили!";
        else
            message = "ничья!";

        OnGameOver?.Invoke(message);
    }
}
