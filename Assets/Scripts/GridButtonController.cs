using UnityEngine;
using UnityEngine.UI;

public class GridButtonController : MonoBehaviour {

    public Button button;
    public Text buttonText;

    private GameController _gameController;

    public void SetGameControllerReference(GameController gamecontroller) {
        this._gameController = gamecontroller;
    }
    
    public void SetSpace() {
        buttonText.text = _gameController.GetPlayerSide();
        button.interactable = false;
        _gameController.EndTurn();
    }
}
