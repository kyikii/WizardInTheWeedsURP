using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TimeDisplay : MonoBehaviour
{
    [SerializeField] GameObject timeDisplay;
    TMP_Text TimeText;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        float LastTime = PlayerPrefs.GetFloat("lastTime");
        TimeText = timeDisplay.GetComponent<TMP_Text>();

        TimeText.text = "Completed in "+ LastTime +" seconds";
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
