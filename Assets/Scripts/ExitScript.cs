using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    SessionData DataObject;

    GameManager GM;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        DataObject = GameObject.Find("SessionData").GetComponent<SessionData>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("LoadStart", 1.5f);
            float lastTime = Time.timeSinceLevelLoad;
            DataObject.LastTime = Mathf.RoundToInt(lastTime);
            GM.Fade.Play("FadeImageIn");

            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                DataObject.Tutorial = true;
            }
        }
    }


    private void LoadStart()
    {
        DontDestroyOnLoad(DataObject);
        SceneManager.LoadScene(0);
    }
}
