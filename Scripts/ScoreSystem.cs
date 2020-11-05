using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField]
    private Text _scores;

    public Text Coins;

    public int Scores = 0;

    private void Start()
    {
        Sounds.PlayCoinSound();
        Coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void Score()
    {
        Scores++;

        _scores.text = Scores.ToString();
    }

    public void AddScoreToCoins()
    {
        int currentCoinsCount = PlayerPrefs.GetInt("Coins");

        int newCoinsCount = currentCoinsCount + (int)(Scores / 2);

        PlayerPrefs.SetInt("Coins", newCoinsCount);
    }
}
