using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBeltScript : MonoBehaviour
{
    public GameObject CurrentTool;
    public Animator ToolAnims;
    [SerializeField] GameObject NillObj;
    


    // Start is called before the first frame update
    void Start()
    {
        ToolAnims = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        updateKeys();
    }
    
    void updateKeys()
    {
        

    if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(CurrentTool == gameObject.transform.GetChild(0).gameObject)
            {
                resetTools();
            }
            else
            {
                CurrentTool = gameObject.transform.GetChild(0).gameObject;
                ToolAnims.SetBool("Equip Shovel",true);
                ToolAnims.SetBool("Equip Rake",false);
                ToolAnims.SetBool("Equip Shears",false);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(CurrentTool == gameObject.transform.GetChild(1).gameObject)
            {
                resetTools();
            }
            else
            {
                CurrentTool = gameObject.transform.GetChild(1).gameObject;
                ToolAnims.SetBool("Equip Shovel",false);
                ToolAnims.SetBool("Equip Rake",false);
                ToolAnims.SetBool("Equip Shears",true);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(CurrentTool == gameObject.transform.GetChild(2).gameObject)
            {
                resetTools();
            }
            else
            {
                CurrentTool = gameObject.transform.GetChild(2).gameObject;
                ToolAnims.SetBool("Equip Shovel",false);
                ToolAnims.SetBool("Equip Rake",true);
                ToolAnims.SetBool("Equip Shears",false);
            }
            
        }
    }

    private void resetTools()
    {
        CurrentTool = NillObj;
        ToolAnims.SetBool("Equip Shovel",false);
        ToolAnims.SetBool("Equip Rake",false);
        ToolAnims.SetBool("Equip Shears",false);
    }

    public void ActivateTool()
    {
        ToolAnims.SetBool("UseTool",true);
    }

    public void DeactivateTool()
    {
        ToolAnims.SetBool("UseTool",false);
    }
    
}
