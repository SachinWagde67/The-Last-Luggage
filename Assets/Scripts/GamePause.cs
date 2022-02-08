using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    [SerializeField] private GameObject GamePauseCanvas;

    public void ResumeBtn()
    {
        Time.timeScale = 1f;
        GamePauseCanvas.SetActive(false);
    }

    public void ExitBtn()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void RestartBtn(string level)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GamePauseCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
