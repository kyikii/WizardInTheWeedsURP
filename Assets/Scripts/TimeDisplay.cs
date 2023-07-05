using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TimeDisplay : MonoBehaviour
{
    [SerializeField] GameObject timeDisplay;
    TMP_Text TimeText;
    SessionData DataObject;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DataObject = GameObject.Find("SessionData").GetComponent<SessionData>();

        TimeText = timeDisplay.GetComponent<TMP_Text>();

        TimeText.text = "Completed in "+ DataObject.LastTime +" seconds";
    }

    

    
}
