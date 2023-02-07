using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TicTacToeController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform buttonsParent, endPanel;
    [SerializeField] Image[] buttonImages;
    [SerializeField] TextMeshProUGUI playerTurnTextBox;
    [SerializeField] AudioClip endGameClip;
    int turn, playerNum;
    List<int> buttonsClicked = new List<int>();

    public void UserClicked()
    {
        playerNum = turn % 2;
        Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        buttonsClicked.Add(clickedButton.transform.GetSiblingIndex());
        UpdateButton(clickedButton, sprites[playerNum], false);
        turn++;
        UpdatePlayerTurn();
        if (turn > 5)
            CheckWinner(sprites[playerNum]);
    }

    void UpdateButton(Button clickedButton, Sprite sprite, bool undo)
    {
        clickedButton.enabled = undo;
        clickedButton.GetComponent<Image>().sprite = sprite;
    }
    void UpdatePlayerTurn()
    {
        playerTurnTextBox.text = $"Player {(turn % 2)+1} turn";
        playerTurnTextBox.color = new Color(0, 1, playerNum);
    }
    void CheckWinner(Sprite sprite)
    {
        if (buttonImages[0].sprite == sprite && buttonImages[1].sprite == sprite && buttonImages[2].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (buttonImages[0].sprite == sprite && buttonImages[3].sprite == sprite && buttonImages[6].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (buttonImages[3].sprite == sprite && buttonImages[4].sprite == sprite && buttonImages[5].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (buttonImages[6].sprite == sprite && buttonImages[7].sprite == sprite && buttonImages[8].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (buttonImages[0].sprite == sprite && buttonImages[3].sprite == sprite && buttonImages[6].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (buttonImages[1].sprite == sprite && buttonImages[4].sprite == sprite && buttonImages[7].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (buttonImages[2].sprite == sprite && buttonImages[5].sprite == sprite && buttonImages[8].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (buttonImages[0].sprite == sprite && buttonImages[4].sprite == sprite && buttonImages[8].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (buttonImages[2].sprite == sprite && buttonImages[4].sprite == sprite && buttonImages[6].sprite == sprite)
            EndGame($"Player {playerNum + 1} Wins");
        else if (turn == 9)
            EndGame("It is a tie!");
    }
    void EndGame(string message)
    {
        endPanel.gameObject.SetActive(true);
        endPanel.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
        AudioSource source = buttonsParent.GetComponent<AudioSource>();
        source.clip = endGameClip;
        source.Play();
    }

    public void Undo()
    {
        if (turn <= 0)
            return;
        playerNum = turn % 2;
        turn--;
        UpdateButton(buttonsParent.GetChild(buttonsClicked[buttonsClicked.Count-1]).GetComponent<Button>(), null, true);
        buttonsClicked.RemoveAt(buttonsClicked.Count - 1);
        UpdatePlayerTurn();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }
}
