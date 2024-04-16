using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    [SerializeField] float sceneDelay = 0.5f;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(BackToMainMenuScene());
        }
    }
    
    IEnumerator BackToMainMenuScene()
    {
        yield return new WaitForSeconds(sceneDelay);

        SceneManager.LoadScene(0);
    }
}
