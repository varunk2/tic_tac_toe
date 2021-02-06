using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;

    private string _playerSide;

    private void Awake() {
        SetGameControllerReferenceOnButtons();
        _playerSide = "X";
    }

    private void SetGameControllerReferenceOnButtons() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<GridButtonController>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide() { return _playerSide; }

    public void EndTurn() {
        if(buttonList[0].text == _playerSide && buttonList[1].text == _playerSide && buttonList[2].text == _playerSide) {
            GameOver();
        }
    }

    private void GameOver() {
        for (int i = 0; i < buttonList.Length; i++) {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
    }
}
