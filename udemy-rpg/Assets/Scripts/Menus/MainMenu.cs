using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] float loadingSpeed = 1f;
    [SerializeField] float loadingDelay = 2f;
    [SerializeField] string newGameScene = null;

    [SerializeField] GameObject essentialsLoader = null;
    [SerializeField] GameObject continueButton = null;
    [SerializeField] Image loadingScreen = null;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

        loadingScreen.gameObject.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Continue()
    {
        loadingScreen.gameObject.SetActive(true);
        StartCoroutine("LoadingGame");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator LoadingGame()
    {
        while(loadingScreen.color.a < 1f)
        {
            loadingScreen.color = new Color
                (loadingScreen.color.r,
                loadingScreen.color.g,
                loadingScreen.color.b,
                Mathf.MoveTowards
                    (loadingScreen.color.a,
                    1f,
                    loadingSpeed * Time.deltaTime));
            yield return null;
        }

        yield return new WaitForSecondsRealtime(loadingDelay);
        Instantiate(essentialsLoader);
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
        FindObjectOfType<GameManager>().LoadData();
        FindObjectOfType<QuestManager>().LoadQuestData();
    }
}
