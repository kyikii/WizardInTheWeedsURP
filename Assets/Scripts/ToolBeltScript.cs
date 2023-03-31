using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBeltScript : MonoBehaviour
{
    public GameObject CurrentTool;
    public Animator ToolAnims;
    public bool EquipReady;
    [SerializeField] GameObject NillObj;
    [SerializeField] GameManager GM;
    [SerializeField] HotBarSwitch HotBar;
    private ParticleSystem MagicParticles;


    // Start is called before the first frame update
    void Start()
    {
        EquipReady = true;
        MagicParticles = this.GetComponent<ParticleSystem>();
        ToolAnims = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateKeys();
    }

    private void updateKeys()
    {
        if (GM.ChartsRunning() == false && EquipReady == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (CurrentTool == gameObject.transform.GetChild(0).gameObject)
                {
                    resetTools();
                }
                else
                {
                    ShowShovel();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (CurrentTool == gameObject.transform.GetChild(1).gameObject)
                {
                    resetTools();
                }
                else
                {
                    ShowShears();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (CurrentTool == gameObject.transform.GetChild(2).gameObject)
                {
                    resetTools();
                }
                else
                {
                    ShowRake();
                }
            }
        }
    }

    private void ShowShovel()
    {
        CurrentTool = gameObject.transform.GetChild(0).gameObject;
        HotBar.PlayShovelAnim();
        ToolAnims.SetBool("Equip Shovel", true);
        ToolAnims.SetBool("Equip Rake", false);
        ToolAnims.SetBool("Equip Shears", false);
    }

    public void EnableShovel()
    {
        ToolAnims.SetBool("ShovelEnabled", true);
        HotBar.showShovel_ICN();
        ShowShovel();
    }



    private void ShowShears()
    {
        CurrentTool = gameObject.transform.GetChild(1).gameObject;
        HotBar.PlayShearsAnim();
        ToolAnims.SetBool("Equip Shovel", false);
        ToolAnims.SetBool("Equip Rake", false);
        ToolAnims.SetBool("Equip Shears", true);
    }

    public void EnableShears()
    {
        ToolAnims.SetBool("ShearsEnabled", true);
        HotBar.showShears_ICN();
        ShowShears();
    }

    private void ShowRake()
    {
        CurrentTool = gameObject.transform.GetChild(2).gameObject;
        HotBar.PlayRakeAnim();
        ToolAnims.SetBool("Equip Shovel", false);
        ToolAnims.SetBool("Equip Rake", true);
        ToolAnims.SetBool("Equip Shears", false);
    }
    public void EnableRake()
    {
        ToolAnims.SetBool("RakeEnabled", true);
        HotBar.showRake_ICN();
        ShowRake();
    }

    public void resetTools()
    {
        HotBar.ResetHotbar();
        CurrentTool = NillObj;
        ToolAnims.SetBool("Equip Shovel", false);
        ToolAnims.SetBool("Equip Rake", false);
        ToolAnims.SetBool("Equip Shears", false);
    }

    public void ActivateTool()
    {
        ToolAnims.SetBool("UseTool", true);
    }

    public void DeactivateTool()
    {
        ToolAnims.SetBool("UseTool", false);
    }

    public void Switching()
    {
        EquipReady = false;
    }
    public void Idle()
    {
        EquipReady = true;
    }

    public void Particles()
    {
        MagicParticles.Clear();
        MagicParticles.Play();
    }

}
