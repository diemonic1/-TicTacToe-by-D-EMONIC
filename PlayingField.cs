using UnityEngine;
using UnityEngine.UI;

public class PlayingField : Photon.MonoBehaviour
{
    public int StepOfGame { get; private set; }

    [SerializeField] private GameObject[] _buttonsOnField;

    private int[,] _playingField;
    private bool _isWin;

    [Header("Links to instances")]
    [SerializeField] private Manager manager;
    [SerializeField] private UpdateVisualisation updateVisualisation;

    private void Start() 
    {
        clearField();
    }

    public void prepareRoom(bool isThisClientHost)
    {
        restartLocalGame(isThisClientHost);
        updateVisualisation.waitForPlayer();
    }

    public void restartLocalGame(bool isThisClientHost) 
    {
        StepOfGame = 0;
        _isWin = false;
        manager.setVisibilityOfRestartButton(false);
        clearField();

        for (int i = 0; i < 9; i++)
            _buttonsOnField[i].GetComponent<Button>().interactable = true;
        
        updateVisualisation.restartGameImages();
        updateVisualisation.startGameVisual(isThisClientHost);
    }

    private void clearField()
    {
        _playingField = new int[3, 3] { { -10, -10, -10 }, { -10, -10, -10 }, { -10, -10, -10 } };
    }

    public void startGame(bool isThisClientHost) 
    {
        updateVisualisation.startGameVisual(isThisClientHost);
    }

    public void sendNumberOfPressedButton(int numberOfActivate)
    {
        if (_buttonsOnField[numberOfActivate].GetComponent<Button>().interactable && manager.isGameStart() && !_isWin && ((manager.isThisClientHost() && StepOfGame % 2 == 0) || (!manager.isThisClientHost() && StepOfGame % 2 == 1)))
            manager.sendNumberOfPressedButton(numberOfActivate);
    }

    public void updatePlayingField(int numberOfActivate, bool isThisClientHost)
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

            updateVisualisation.updateImages(numberOfActivate, sign, StepOfGame, isThisClientHost);

            StepOfGame += 1;

            checkFieldOnWinLine();
        }
    }

    private void checkFieldOnWinLine() 
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
            gameOver(winnerSign);
        else if (StepOfGame == 9 && _isWin == false)
            gameOver(-1);
    }

    private void gameOver(int winnerSign)
    {
        _isWin = true;
        manager.setVisibilityOfRestartButton(true);

        if (winnerSign == 0)
            updateVisualisation.showZeroWinScreen();
        else if (winnerSign == 1)
            updateVisualisation.showCrossWinScreen();
        else
            updateVisualisation.showDrawScreen();
    }
}
