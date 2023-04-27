using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class MenuScript : MonoBehaviour
{
    Animator MenuAnims;
    [SerializeField]Flowchart ViewCommands;

    // Start is called before the first frame update
    void Start()
    {
        MenuAnims = GetComponent<Animator>();
        ReturnMain();
    }

    public void SelectLevel()
    {
        MenuAnims.SetBool("Levels",true);
        MenuAnims.SetBool("Main",false);
        ViewCommands.ExecuteBlock("Levels");
    } 

    public void SelectCredits()
    {
        
        MenuAnims.SetBool("Credits",true);
        MenuAnims.SetBool("Main",false);
        ViewCommands.ExecuteBlock("Credits");
    }

    public void ReturnMain()
    {
        MenuAnims.SetBool("Main",true);
        MenuAnims.SetBool("Credits",false);
        MenuAnims.SetBool("Levels",false);
        ViewCommands.ExecuteBlock("Main");
    }

    public void LaunchExit()
    {
        ViewCommands.ExecuteBlock("Exit");
        MenuAnims.SetBool("Main",false);
    }

    public void Running()
    {
        if(MenuAnims.GetBool("Running"))
        {
            MenuAnims.SetBool("Running",false);
        }
        else
        {
            MenuAnims.SetBool("Running",true);
        }
    }






    public void LauchDev()
    {
        SceneManager.LoadScene(2);
    }

    public void LaunchMain()
    {
        SceneManager.LoadScene(3);
    }

    public void LaunchTutorial()
    {
        SceneManager.LoadScene(4);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
