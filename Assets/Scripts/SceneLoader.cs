using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float timeDelay = 4f;
    [SerializeField] Animator transitionAnim;

    private const int MAIN_MENU_SCENE = 0;
    private const int GAME_SCENE = 1;
    private const int GAME_OVER_SCENE = 2;
    private const int WIN_SCENE = 3;

    private void LoadSceneWithDelay(int sceneIndex)
    {

        StartCoroutine(WaitAndLoad(sceneIndex));
    }

    IEnumerator WaitAndLoad(int sceneIndex)
    {
        yield return new WaitForSeconds(timeDelay);

        if (IsValidSceneIndex(sceneIndex))
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index: " + sceneIndex);
        }
    }

    private bool IsValidSceneIndex(int sceneIndex)
    {
        return sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings;
    }

    public void LoadMainMenu()
    {
        transitionAnim.SetTrigger("Load(CloseScene)");
        LoadSceneWithDelay(MAIN_MENU_SCENE);
    }

    public void LoadGame()
    {
        transitionAnim.SetTrigger("Load(CloseScene)");
        LoadSceneWithDelay(GAME_SCENE);
    }

    public void LoadGameoverScene()
    {
        transitionAnim.SetTrigger("Load(CloseScene)");
        LoadSceneWithDelay(GAME_OVER_SCENE);
    }

    public void LoadGiveUpGameOverScene()
    {
        transitionAnim.SetTrigger("Load(CloseScene)");
        LoadSceneWithDelay(GAME_OVER_SCENE);
    }

    public void LoadWinScene()
    {
        transitionAnim.SetTrigger("Load(CloseScene)");
        LoadSceneWithDelay(WIN_SCENE);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
