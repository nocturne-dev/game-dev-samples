using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] float areaLoadDelay = 0f;
    [SerializeField] string areaToLoad = "";
    [SerializeField] string currLocation = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.IsTouchingLayers(LayerMask.GetMask("Exit"))) 
        { 
            StartCoroutine(LoadNextArea()); 
        }
    }

    IEnumerator LoadNextArea()
    {
        FindObjectOfType<UIManager>().ActivateTransitionScreen(true);
        FindObjectOfType<UIFadeTransition>().StartFadingToBlack();
        FindObjectOfType<GameManager>().SetSceneTransitionActive(true);
        
        yield return new WaitForSecondsRealtime(areaLoadDelay);

        FindObjectOfType<Player>().SetPrevLocation(currLocation);
        SceneManager.LoadScene(areaToLoad);
    }
}
