using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    [SerializeField] private Image winBackground;
    [SerializeField] private Image loseBackground;
    [SerializeField] private TextMeshProUGUI coinText; 
    

    private void Start()
    {
        GameManager.Instance.OnPlayerWon += ShowWinUI;
        GameManager.Instance.OnPlayerLost += ShowLoseUI;
        GameManager.Instance.OnCoinCountChanged += UpdateCoinText;
    }

    private void UpdateCoinText(int coinCount)
    {
        coinText.text = coinCount.ToString();
    }
 
    private void OnDisable()
    {
        GameManager.Instance.OnPlayerLost -= ShowLoseUI;
        GameManager.Instance.OnPlayerWon -= ShowWinUI;
    }

    private void ShowWinUI()
    {
        winBackground.gameObject.SetActive(true);
        winBackground.DOColor(new Color(winBackground.color.r, winBackground.color.g, winBackground.color.b, 0.8f),
            0.5f);
    }
    private void ShowLoseUI()
    {
        loseBackground.gameObject.SetActive(true);
        loseBackground.DOColor(new Color(loseBackground.color.r, loseBackground.color.g, loseBackground.color.b, 0.8f),
            0.5f);
    }
    

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}