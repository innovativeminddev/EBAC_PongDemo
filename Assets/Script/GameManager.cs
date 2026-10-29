using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Referências da Cena")]
    public BallController ball;
    public TextMeshProUGUI scoreP1Text;
    public TextMeshProUGUI scoreP2Text;
    public TextMeshProUGUI nameP1Text;
    public TextMeshProUGUI nameP2Text;
    public SpriteRenderer paddleLeftRenderer;
    public SpriteRenderer paddleRightRenderer;

    [Header("Regras do Jogo")]
    public int maxScoreToWin = 5;

    private int scoreP1 = 0;
    private int scoreP2 = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        LoadCustomizations();
        StartNewRound();
    }

    void Update()
    {
        // Passo 5: Atalho para reiniciar a jogada/rodada a qualquer momento (Tecla R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartNewRound();
        }
    }

    public void OnGoalScored(int playerWhoScored)
    {
        if (playerWhoScored == 1) scoreP1++;
        else if (playerWhoScored == 2) scoreP2++;

        UpdateUI();
        CheckHighScore();

        if (scoreP1 >= maxScoreToWin || scoreP2 >= maxScoreToWin)
        {
            // Salva quem venceu e vai para a GameOverScene
            string winner = (scoreP1 >= maxScoreToWin) ? PlayerPrefs.GetString("NameP1", "Jogador 1") : PlayerPrefs.GetString("NameP2", "Jogador 2");
            PlayerPrefs.SetString("LastWinner", winner);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GameOverScene");
        }
        else
        {
            StartNewRound();
        }
    }

    public void StartNewRound()
    {
        ball.ResetBall();
        Invoke(nameof(LaunchBallWithDelay), 1.5f); // Atraso de 1.5s antes de soltar a bola
    }

    void LaunchBallWithDelay()
    {
        ball.LaunchBall();
    }

    void UpdateUI()
    {
        if (scoreP1Text != null) scoreP1Text.text = scoreP1.ToString();
        if (scoreP2Text != null) scoreP2Text.text = scoreP2.ToString();
    }

    void CheckHighScore()
    {
        int p1Max = PlayerPrefs.GetInt("HighScore_P1", 0);
        int p2Max = PlayerPrefs.GetInt("HighScore_P2", 0);

        if (scoreP1 > p1Max) PlayerPrefs.SetInt("HighScore_P1", scoreP1);
        if (scoreP2 > p2Max) PlayerPrefs.SetInt("HighScore_P2", scoreP2);

        PlayerPrefs.Save();
    }

    void LoadCustomizations()
    {
        // Carregar Nomes
        if (nameP1Text != null) nameP1Text.text = PlayerPrefs.GetString("NameP1", "Jogador 1");
        if (nameP2Text != null) nameP2Text.text = PlayerPrefs.GetString("NameP2", "Jogador 2");

        // Carregar Cores
        if (paddleLeftRenderer != null)
            paddleLeftRenderer.color = StringToColor(PlayerPrefs.GetString("ColorP1", "White"));
        if (paddleRightRenderer != null)
            paddleRightRenderer.color = StringToColor(PlayerPrefs.GetString("ColorP2", "White"));
    }

    Color StringToColor(string colorName)
    {
        switch (colorName)
        {
            case "Red": return Color.red;
            case "Green": return Color.green;
            case "Blue": return Color.blue;
            case "Yellow": return Color.yellow;
            default: return Color.white;
        }
    }

    // Passo 10: Função de Reset Geral das Informações
    public static void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Todas as informações salvas foram limpas.");
    }
}