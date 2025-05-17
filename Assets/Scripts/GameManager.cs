using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public FoodCollector collector;
    public Text resultText;         // Надпись "Победа / Поражение"
    public Button restartButton;    // Кнопка "Заново"

    private bool gameEnded = false;

    void Start()
    {
        // Скрыть текст и кнопку при старте
        if (resultText != null)
            resultText.gameObject.SetActive(false);

        if (restartButton != null)
            restartButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameEnded || collector == null) return;

        // Условие проигрыша
        if (collector.junk >= 3f)
        {
            Lose("Zu viel ungesundes Essen");
            return;
        }

        // Условие победы
        if (collector.junk == 0f &&
            collector.vitamin == 5f &&
            collector.calcium == 5f &&
            collector.iron == 5f)
        {
            Win();
        }
    }

    void Win()
    {
        gameEnded = true;
        ShowMessage("🎉 You win!! FoodBalance ist perfeckt.");
    }

    void Lose(string reason)
    {
        gameEnded = true;
        ShowMessage("❌ Game over " + reason);
    }

    void ShowMessage(string message)
    {
        if (resultText != null)
        {
            resultText.text = message;
            resultText.gameObject.SetActive(true);
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
    }

    // Метод для перезапуска сцены
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


