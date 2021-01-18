using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIbuttonsHandler : MonoBehaviour
{
    // handle button click of UI
    public GameObject pauseScreen;

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseBtn()
    {
        if (pauseScreen.activeSelf)
            return;

        pauseScreen.SetActive(true);
        StartCoroutine(pauseBtn());
    }
    IEnumerator pauseBtn()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
    }
    public void ResumeBtn()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 2f;
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
   
}
