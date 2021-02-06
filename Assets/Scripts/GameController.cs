using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player {
    public Image panel;
    public Text text;
}

[System.Serializable]
public class PlayerColor {
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Button restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    private string[] _playerSides = { "X", "O" };
    private string _playerSide;
    private int _moveCount;

    private void Awake() {
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        _playerSide = _playerSides[0];
        _moveCount = 0;
        ToggleRestartButton(false);
    }

    private void SetGameControllerReferenceOnButtons() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<GridButtonController>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide() { return _playerSide; }

    public void ChangeSides() {
        _playerSide = (_playerSide == _playerSides[0])
                        ? _playerSides[1]
                        : _playerSides[0];
    }

    public void EndTurn() {

        _moveCount++;

        // Rows check
        if(buttonList[0].text == _playerSide && buttonList[1].text == _playerSide && buttonList[2].text == _playerSide) {
            GameOver(_playerSide);
        }
        if (buttonList[3].text == _playerSide && buttonList[4].text == _playerSide && buttonList[5].text == _playerSide) {
            GameOver(_playerSide);
        }
        if (buttonList[6].text == _playerSide && buttonList[7].text == _playerSide && buttonList[8].text == _playerSide) {
            GameOver(_playerSide);
        }
        // Rows check ends

        // Diagonals Check
        if (buttonList[0].text == _playerSide && buttonList[4].text == _playerSide && buttonList[8].text == _playerSide) {
            GameOver(_playerSide);
        }
        if (buttonList[2].text == _playerSide && buttonList[4].text == _playerSide && buttonList[6].text == _playerSide) {
            GameOver(_playerSide);
        }
        // Diagonals Check ends

        // Columns Check
        if (buttonList[0].text == _playerSide && buttonList[3].text == _playerSide && buttonList[6].text == _playerSide) {
            GameOver(_playerSide);
        }
        if (buttonList[1].text == _playerSide && buttonList[4].text == _playerSide && buttonList[7].text == _playerSide) {
            GameOver(_playerSide);
        }
        if (buttonList[2].text == _playerSide && buttonList[5].text == _playerSide && buttonList[8].text == _playerSide) {
            GameOver(_playerSide);
        }
        // Columns Check ends

        if(_moveCount >= 9) {
            GameOver("draw");
        }

        ChangeSides();
    }

    private void GameOver(string winningPlayer) {
        SetBoardInteractable(false);        

        if(winningPlayer == "draw") {
            SetGameOverText("It's a draw!!");
        } else {
            SetGameOverText(winningPlayer + " Wins!!!");
        }

        ToggleRestartButton(true);
    }

    private void SetGameOverText(string value) {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
        ToggleRestartButton(true);
    }

    private void SetBoardInteractable(bool toggle) {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public void RestartGame() {
        _playerSide = _playerSides[0];
        _moveCount = 0;
        gameOverPanel.SetActive(false);
        SetBoardInteractable(true);

        for (int i = 0; i < buttonList.Length; i++) {            
            buttonList[i].text = "";
        }

        ToggleRestartButton(false);
    }

    private void ToggleRestartButton(bool toggle) {
        restartButton.gameObject.SetActive(toggle);
    }
}
