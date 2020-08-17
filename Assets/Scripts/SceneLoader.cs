using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const int START_SCREEN_INDEX = 0;

    public void loadNextScene()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIndex + 1);
    }
    public void loadStartMenu()
    {
        FindObjectOfType<GameSession>().destroyGameSession();
        SceneManager.LoadScene(START_SCREEN_INDEX);
    }
    public void quitHandler()
    {
        Application.Quit();
    }
    public void gameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
