using System.Net.WebSockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class MenuScript : MonoBehaviour
{
    Animator MenuAnims;
    AudioSource Woosh, Click;
    SessionData DataObject;

    [SerializeField] GameObject NextLevel;
    Animation Fade;
    [SerializeField] Flowchart ViewCommands;

    // Start is called before the first frame update
    void Start()
    {
        MenuAnims = GetComponent<Animator>();
        DataObject = GameObject.Find("SessionData").GetComponent<SessionData>();
        Fade = GameObject.Find("FadeImage").GetComponent<Animation>();

        Click = transform.GetChild(0).GetComponent<AudioSource>();
        Woosh = transform.GetChild(1).GetComponent<AudioSource>();

        Fade.Play("FadeImageOutLong");
        StartCheck();
    }

    private void StartCheck()
    {
        if (DataObject.LastTime > 0) //if they have played already
        {
            if (DataObject.Tutorial)
            {
                MenuAnims.SetBool("EndScreen", true);
                MenuAnims.SetBool("Main", false);
                NextLevel.SetActive(true);
                NextLevel.GetComponent<Animation>().Play("FadeImageIn");
            }
            else
            {
                MenuAnims.SetBool("EndScreen", true);
                MenuAnims.SetBool("Main", false);
                //dont
            }
        }
        else
        {
            MenuAnims.SetBool("Main", true);
            MenuAnims.SetBool("EndScreen", false);
            ViewCommands.ExecuteBlock("Main");
        }
    }

    public void WooshSFX()
    {
        Woosh.Play();
    }

    private void ClickSFX()
    {
        Click.Play();
    }

    public void SelectLevel()
    {
        ClickSFX();
        MenuAnims.SetBool("Levels", true);
        MenuAnims.SetBool("Main", false);
        ViewCommands.ExecuteBlock("Levels");
    }

    public void SelectCredits()
    {
        ClickSFX();
        MenuAnims.SetBool("Credits", true);
        MenuAnims.SetBool("Main", false);
        ViewCommands.ExecuteBlock("Credits");
    }

    public void ReturnMain()
    {
        ClickSFX();
        if(DataObject.Tutorial)
        {
            NextLevel.GetComponent<Animation>().Play("FadeImageOut");
        }
        MenuAnims.SetBool("Main", true);
        MenuAnims.SetBool("Credits", false);
        MenuAnims.SetBool("Levels", false);
        MenuAnims.SetBool("EndScreen", false);
        ViewCommands.ExecuteBlock("Main");
        Invoke("Button",0.3f);
    }

    private void Button()
    {
        NextLevel.SetActive(false);
    }

    public void LaunchExit()
    {
        ClickSFX();
        ViewCommands.ExecuteBlock("Exit");
        MenuAnims.SetBool("Main", false);
    }

    public void Running()
    {
        if (MenuAnims.GetBool("Running"))
        {
            MenuAnims.SetBool("Running", false);
        }
        else
        {
            MenuAnims.SetBool("Running", true);
        }
    }


    private void DevLevel()
    {
        SceneManager.LoadScene(2);
    }

    private void MainLevel()
    {
        SceneManager.LoadScene(3);
    }

    private void TutorialLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void LauchDev()
    {
        ClickSFX();
        Invoke("DevLevel", 0.5f);
        Fade.Play("FadeImageIn");
    }

    public void LaunchMain()
    {
        ClickSFX();
        Invoke("MainLevel", 0.5f);
        Fade.Play("FadeImageIn");
    }

    public void LaunchTutorial()
    {
        ClickSFX();
        Invoke("TutorialLevel", 0.5f);
        Fade.Play("FadeImageIn");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
