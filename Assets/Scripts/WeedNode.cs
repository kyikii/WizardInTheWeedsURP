using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeedNode : MonoBehaviour
{

    private HighlightScript Highliter;
    private WeedManager Manager;
    bool disabled;

    [SerializeField] private int state = 0;
    [SerializeField] private GameObject BeltObj,ToolA,ToolB,ToolC;
    private GameObject ModelA,ModelB,ModelC,DesiredTool,CurrentModel,WeedName;
    TMP_Text NameText;

    private ParticleSystem poof;
    private GameManager GM;
    private ToolBeltScript Belt;
    private WeedArea Area;

    void Start()
    {
        //data references
        Highliter = gameObject.GetComponent<HighlightScript>();
        Manager = gameObject.GetComponentInParent<WeedManager>();
        poof = gameObject.GetComponent<ParticleSystem>();
        GM = GetComponentInParent<Transform>().GetComponentInParent<GameManager>();
        Belt = BeltObj.GetComponent<ToolBeltScript>();
        Area = gameObject.GetComponentInParent<WeedArea>();

        //canvas interactions
        WeedName = GameObject.Find("WeedName");
        NameText = WeedName.GetComponent<TMP_Text>();

        //models
        ModelA = gameObject.transform.GetChild(1).gameObject;
        ModelB = gameObject.transform.GetChild(2).gameObject;
        ModelC = gameObject.transform.GetChild(3).gameObject;

        CurrentModel = gameObject.transform.GetChild(1).gameObject;

        DesiredTool = ToolA; //tool pass   
    }

    void OnMouseOver()
    {
        if(GM.HoveredObject.gameObject == this.gameObject)
        {
            Highliter.highlightObj(CurrentModel);
            GM.updateCodex(this.name);
            NameText.enabled = true;
            NameText.text = this.gameObject.name;
        }

        else if(GM.HoveredObject == GM.NillObj)
        {
            Highliter.UnhighlightObj(CurrentModel);
            NameText.enabled = false;
        }

    }
    void OnMouseExit()
    {
        Highliter.UnhighlightObj(CurrentModel);
        NameText.enabled = false;
    }

    void OnMouseDown()
    {
        if(state < 4 && GM.HoveredObject.gameObject == this.gameObject && Belt.CurrentTool == DesiredTool && Belt.ToolAnims.GetBool("UseTool") == false)
        {
            UpdateState();
            Belt.ActivateTool();
            //poof.Clear();
            poof.Play();
        }
    }

    private void UpdateState()
    {
        state ++;
        //all weeds will start in state A, there is never a need to reverse through the stages.
        switch(state)
        {
            //state B
            case 1:
            ModelA.SetActive(false); //one mean command to send to some poor a$$ game object.
            ModelB.SetActive(true);
            CurrentModel = gameObject.transform.GetChild(2).gameObject;
            DesiredTool = ToolB;
            break;

            //StateC
            case 2:
            ModelB.SetActive(false);
            ModelC.SetActive(true);
            CurrentModel = gameObject.transform.GetChild(3).gameObject;
            DesiredTool = ToolC;
            break;

            //Gone
            case 3:
            ModelC.SetActive(false);
            CurrentModel = null;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Area.UpdateAreaTotal();
            Manager.updateWeedTotal();
            DesiredTool = GM.NillObj;
            break;
        }
    }
}
