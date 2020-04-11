using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;
    private int health = 0;

    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }


    public void AddScore(int scoreAmount)
    {
        score += scoreAmount;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}