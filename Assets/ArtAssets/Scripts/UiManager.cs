using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Button nextLevelButton;
    public Button restartButton;
    public TextMeshProUGUI text;
    public GameObject uiPanel;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        ClosePanel();
        nextLevelButton.onClick.AddListener(ClosePanel);
    }

    public void WinScreen()
    {
        uiPanel.SetActive(true);
        text.text = "You Win";
    }
    public void LoseScreen()
    {
        uiPanel.SetActive(true);
        text.text = "You Lose";
        nextLevelButton.interactable = false;
    }
    public void ClosePanel()
    {
        uiPanel.SetActive(false);
    }
}
