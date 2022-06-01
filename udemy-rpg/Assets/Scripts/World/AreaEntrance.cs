using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [Tooltip("Name of next location"), SerializeField] string prevLocation = "";

    // Start is called before the first frame update
    void Start()
    {
        GetPrevLocationOfPlayer();
        GetUITransitionFromBlack();
        GetGameManagerFading();
    }

    private void GetPrevLocationOfPlayer()
    {
        Player player = FindObjectOfType<Player>();
        if (!player) 
        { 
            return; 
        }

        else if (player.GetPrevLocation().Equals(prevLocation)) 
        { 
            player.transform.position = transform.position; 
        }
    }

    private void GetUITransitionFromBlack()
    {
        UIFadeTransition uiFade = FindObjectOfType<UIFadeTransition>();
        if (!uiFade || !uiFade.GetFadeScreen())
        {
            return;
        }

        uiFade.StartFadingFromBlack();
    }
    
    private void GetGameManagerFading()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (!gm)
        {
            return;
        }

        gm.SetSceneTransitionActive(false);
    }
}
