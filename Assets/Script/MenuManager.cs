using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Campos de Nome")]
    public TMP_InputField inputP1;
    public TMP_InputField inputP2;

    [Header("Campos de Cor")]
    public TMP_Dropdown dropdownP1;
    public TMP_Dropdown dropdownP2;

    [Header("Exibição de Recorde")]
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        UpdateHighScoreDisplay();
        LoadSavedInputFields();
    }

    public void StartGame()
    {
        // Passo 8: Salvar os nomes digitados
        string name1 = string.IsNullOrEmpty(inputP1.text) ? "Jogador 1" : inputP1.text;
        string name2 = string.IsNullOrEmpty(inputP2.text) ? "Jogador 2" : inputP2.text;
        PlayerPrefs.SetString("NameP1", name1);
        PlayerPrefs.SetString("NameP2", name2);

        // Passo 7: Salvar as cores selecionadas
        string color1 = dropdownP1.options[dropdownP1.value].text;
        string color2 = dropdownP2.options[dropdownP2.value].text;
        PlayerPrefs.SetString("ColorP1", color1);
        PlayerPrefs.SetString("ColorP2", color2);

        PlayerPrefs.Save();

        // Carregar a cena do jogo
        SceneManager.LoadScene("GameScene");
    }

    public void ResetSavedData()
    {
        // Passo 10: Reseta tudo do PlayerPrefs
        GameManager.ResetAllData();
        UpdateHighScoreDisplay();
        inputP1.text = "";
        inputP2.text = "";
        dropdownP1.value = 0;
        dropdownP2.value = 0;
    }

    void UpdateHighScoreDisplay()
    {
        // Passo 9: Exibe as pontuações máximas gravadas
        int maxP1 = PlayerPrefs.GetInt("HighScore_P1", 0);
        int maxP2 = PlayerPrefs.GetInt("HighScore_P2", 0);
        if (highScoreText != null)
        {
            highScoreText.text = $"Recorde Maximo:\n{PlayerPrefs.GetString("NameP1", "P1")}: {maxP1} pts  |  {PlayerPrefs.GetString("NameP2", "P2")}: {maxP2} pts";
        }
    }

    void LoadSavedInputFields()
    {
        if (inputP1 != null) inputP1.text = PlayerPrefs.GetString("NameP1", "");
        if (inputP2 != null) inputP2.text = PlayerPrefs.GetString("NameP2", "");
    }
}