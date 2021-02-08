using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Player {
    public Image panel;
    public Text text;
    public Button button;
}

[Serializable]
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
    public GameObject startInfo;

    private string[] _playerSides = { "X", "O" };
    private string _playerSide;
    private int _moveCount;

    private void Awake() {
        _moveCount = 0;
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        ToggleRestartButton(false);
    }

    private void SetGameControllerReferenceOnButtons() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<GridButtonController>().SetGameControllerReference(this);
        }
    }

    public void SetStartingSides(string startingSide) {
        _playerSide = startingSide;

        if (_playerSide == _playerSides[0]) {
            SetPlayerColors(playerX, playerO);
        } else {
            SetPlayerColors(playerO, playerX);
        }

        StartGame();
    }

    private void StartGame() {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }    

    public string GetPlayerSide() { return _playerSide; }

    public void EndTurn() {

        _moveCount++;

        // Rows check
        if(buttonList[0].text == _playerSide && buttonList[1].text == _playerSide && buttonList[2].text == _playerSide) {
            GameOver(_playerSide);
        }
        else if (buttonList[3].text == _playerSide && buttonList[4].text == _playerSide && buttonList[5].text == _playerSide) {
            GameOver(_playerSide);
        }
        else if (buttonList[6].text == _playerSide && buttonList[7].text == _playerSide && buttonList[8].text == _playerSide) {
            GameOver(_playerSide);
        }
        // Rows check ends

        // Diagonals Check
        else if (buttonList[0].text == _playerSide && buttonList[4].text == _playerSide && buttonList[8].text == _playerSide) {
            GameOver(_playerSide);
        }
        else if (buttonList[2].text == _playerSide && buttonList[4].text == _playerSide && buttonList[6].text == _playerSide) {
            GameOver(_playerSide);
        }
        // Diagonals Check ends

        // Columns Check
        else if (buttonList[0].text == _playerSide && buttonList[3].text == _playerSide && buttonList[6].text == _playerSide) {
            GameOver(_playerSide);
        }
        else if (buttonList[1].text == _playerSide && buttonList[4].text == _playerSide && buttonList[7].text == _playerSide) {
            GameOver(_playerSide);
        }
        else if (buttonList[2].text == _playerSide && buttonList[5].text == _playerSide && buttonList[8].text == _playerSide) {
            GameOver(_playerSide);
        }
        // Columns Check ends

        else if(_moveCount >= 9) {
            GameOver("draw");
        } else {
            ChangeSides();
        }
    }

    public void ChangeSides() {
        _playerSide = (_playerSide == _playerSides[0]) ? _playerSides[1] : _playerSides[0];

        if (_playerSide == _playerSides[0]) {
            SetPlayerColors(playerX, playerO);
        } else {
            SetPlayerColors(playerO, playerX);
        }
    }

    public void SetPlayerColors(Player newPlayer, Player oldPlayer) {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;

        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    private void GameOver(string winningPlayer) {
        SetBoardInteractable(false);

        if (winningPlayer == "draw") {
            SetGameOverText("It's a draw!!");
            SetPlayerColorsInactive();
        } else {
            SetGameOverText(winningPlayer + " Wins!!!");
        }

        ToggleRestartButton(true);
    }

    private void SetGameOverText(string value) {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame() {
        _moveCount = 0;
        gameOverPanel.SetActive(false);
        startInfo.SetActive(true);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        ToggleRestartButton(true);

        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].text = "";
        }
    }

    private void SetBoardInteractable(bool toggle) {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    private void SetPlayerButtons(bool toggle) {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    private void SetPlayerColorsInactive() {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;

        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

    private void ToggleRestartButton(bool toggle) {
        restartButton.gameObject.SetActive(toggle);
    }
}
