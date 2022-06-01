using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleNotification : MonoBehaviour
{
    [SerializeField] Text notificationText = null;

    [SerializeField] float awakeTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate()
    {
        gameObject.SetActive(true);
        StartCoroutine("Display");
    }

    IEnumerator Display()
    {
        yield return new WaitForSecondsRealtime(awakeTime);
        gameObject.SetActive(false);
    }

    public void SetNotificationText(string s)
    {
        notificationText.text = notificationText != null ? s : null;
    }
}
