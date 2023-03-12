using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBeltScript : MonoBehaviour
{
    public GameObject CurrentTool;
    [SerializeField] GameObject NillObj;
    


    // Start is called before the first frame update
    void Start()
    {
        
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
                this.GetComponent<Animator>().SetBool("Equip Shovel",true);
                this.GetComponent<Animator>().SetBool("Equip Rake",false);
                this.GetComponent<Animator>().SetBool("Equip Shears",false);
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
                this.GetComponent<Animator>().SetBool("Equip Shovel",false);
                this.GetComponent<Animator>().SetBool("Equip Rake",false);
                this.GetComponent<Animator>().SetBool("Equip Shears",true);
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
                this.GetComponent<Animator>().SetBool("Equip Shovel",false);
                this.GetComponent<Animator>().SetBool("Equip Rake",true);
                this.GetComponent<Animator>().SetBool("Equip Shears",false);
            }
            
        }
    }

    private void resetTools()
    {
        CurrentTool = NillObj;
        this.GetComponent<Animator>().SetBool("Equip Shovel",false);
        this.GetComponent<Animator>().SetBool("Equip Rake",false);
        this.GetComponent<Animator>().SetBool("Equip Shears",false);
    }

    public void ActivateTool()
    {
        this.GetComponent<Animator>().SetBool("UseTool",true);
    }

    public void DeactivateTool()
    {
        this.GetComponent<Animator>().SetBool("UseTool",false);
    }
    void OnMouseDown()
    {
        
    }

}
