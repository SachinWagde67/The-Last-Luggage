using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    public void NextBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
