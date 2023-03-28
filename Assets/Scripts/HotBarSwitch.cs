using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class HotBarSwitch : MonoBehaviour
{
    private Animator hotbarAnims;
    [SerializeField] GameObject Shovel_ICN, Shears_ICN, Rake_ICN;
    // Start is called before the first frame update
    void Start()
    {
        hotbarAnims = this.GetComponent<Animator>();
    }

    public void showRake_ICN()
    {
        Rake_ICN.GetComponent<Animation>().Play();
    }

    public void showShears_ICN()
    {
        Shears_ICN.GetComponent<Animation>().Play();
    }

    public void showShovel_ICN()
    {
        Shovel_ICN.GetComponent<Animation>().Play();
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
