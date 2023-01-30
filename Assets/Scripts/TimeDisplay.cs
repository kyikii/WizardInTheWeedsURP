using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimeDisplay : MonoBehaviour
{
    [SerializeField] GameObject timeDisplay;
    TMP_Text TimeText;
    // Start is called before the first frame update
    void Start()
    {
        float LastTime = PlayerPrefs.GetFloat("lastTime");
        TimeText = timeDisplay.GetComponent<TMP_Text>();

        TimeText.text = "Completed in "+ LastTime +" seconds";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
