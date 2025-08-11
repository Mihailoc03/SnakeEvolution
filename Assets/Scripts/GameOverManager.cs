using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Snake snake;
    public ScoreManager scoreManager;
    public FoodSpawner foodSpawner;

    private float initialMoveDelay;

    private void Start()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        initialMoveDelay = snake.moveDelay;

        MusicManager.instance.PlayMusicFromStart();

        ClearAllFoodAndObstacles();
        foodSpawner.ClearFoodList();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        MusicManager.instance.PauseMusic();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        scoreManager.ResetScore();
        snake.ResetState();

        ClearAllFoodAndObstacles();
        foodSpawner.ClearFoodList();
        gameOverPanel.SetActive(false);

        MusicManager.instance.PlayMusicFromStart();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ClearAllFoodAndObstacles()
    {
        foreach (var food in GameObject.FindGameObjectsWithTag("NormalFood"))
            Destroy(food);
        foreach (var food in GameObject.FindGameObjectsWithTag("BonusFood"))
            Destroy(food);
        foreach (var food in GameObject.FindGameObjectsWithTag("SlowFood"))
            Destroy(food);
        foreach (var obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
            Destroy(obstacle);
    }
}
