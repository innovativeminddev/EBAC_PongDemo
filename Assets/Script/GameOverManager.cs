using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText;

    void Start()
    {
        string winnerName = PlayerPrefs.GetString("LastWinner", "Ninguém");
        if (winnerText != null)
        {
            winnerText.text = $"VITÓRIA DE\n{winnerName}!";
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}