public class PlayingFieldPresenter
{
    private readonly Menu menu;
    private readonly FieldView fieldView;
    private readonly PlayingField playingField;
    private readonly ButtonOnField[] buttonsOnField;

    public PlayingFieldPresenter(Menu menu, FieldView fieldView, PlayingField playingField, ButtonOnField[] buttonsOnField)
    {
        this.menu = menu;
        this.fieldView = fieldView;
        this.playingField = playingField;
        this.buttonsOnField = buttonsOnField;
    }

    public void Enable()
    {
        playingField.OnFieldChanged += UpdateFieldView;
        playingField.OnGameOver += GameOver;
    }

    public void Disable()
    {
        playingField.OnFieldChanged -= UpdateFieldView;
        playingField.OnGameOver -= GameOver;
    }

    private void UpdateFieldView(int numberOfActivated, int sign, string message)
    {
        fieldView.UpdateFieldView(numberOfActivated, sign, message);
        buttonsOnField[numberOfActivated].DiactivateButton();
    }

    private void GameOver(string message)
    {
        menu.SetVisibilityOfRestartButton(true);
        fieldView.ShowGameOverScreen(message);
    }
}
