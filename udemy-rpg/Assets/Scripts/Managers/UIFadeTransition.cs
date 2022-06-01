using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeTransition : MonoBehaviour
{
    // Variables set by editor
    [SerializeField] float fadeSpeed = 1f;
    
    // Variables set internally
    private Image fadeScreen = null;

    void Start()
    {
        fadeScreen = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public Image GetFadeScreen()
    {
        return fadeScreen;
    }

    public void StartFadingToBlack()
    {
        StartCoroutine(FadingToBlack());
    }

    public void StartFadingFromBlack()
    {
        StartCoroutine(FadingFromBlack());
    }

    IEnumerator FadingToBlack()
    {
        while (fadeScreen.color.a < 1f) 
        {
            fadeScreen.color = new Color
                (fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards
                    (fadeScreen.color.a,
                    1f,
                    fadeSpeed * Time.deltaTime));
            yield return null;
        }
        yield return null;
    }

    IEnumerator FadingFromBlack()
    {
        while(fadeScreen.color.a > 0f)
        {
            fadeScreen.color = new Color
                (fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards
                    (fadeScreen.color.a,
                    0f,
                    fadeSpeed * Time.deltaTime));
            yield return null;
        }
        yield return null;
    }
}
