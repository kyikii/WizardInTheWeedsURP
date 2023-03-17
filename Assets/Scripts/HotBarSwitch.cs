using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarSwitch : MonoBehaviour
{
    private Animator hotbarAnims;
    // Start is called before the first frame update
    void Start()
    {
        hotbarAnims = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShovelAnim()
    {
        hotbarAnims.SetBool("ShovelSelected",true);
        hotbarAnims.SetBool("ShearsSelected",false);
        hotbarAnims.SetBool("RakeSelected",false);
    }
    public void PlayShearsAnim()
    {
        hotbarAnims.SetBool("ShearsSelected",true);
        hotbarAnims.SetBool("ShovelSelected",false);
        hotbarAnims.SetBool("RakeSelected",false);
    }
    public void PlayRakeAnim()
    {
        hotbarAnims.SetBool("RakeSelected",true);
        hotbarAnims.SetBool("ShearsSelected",false);
        hotbarAnims.SetBool("ShovelSelected",false);
    }

    public void ResetHotbar()
    {
        hotbarAnims.SetBool("ShearsSelected",false);
        hotbarAnims.SetBool("ShovelSelected",false);
        hotbarAnims.SetBool("RakeSelected",false);
    }
}
