using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeedNode : MonoBehaviour
{

    private HighlightScript Highliter;
    private WeedManager Manager;

    [SerializeField] private int state = 0;

    private GameObject ModelA,ModelB,ModelC,CurrentModel,WeedName;
    TMP_Text NameText;

    void Start()
    {
        Highliter = gameObject.GetComponent<HighlightScript>();
        Manager = gameObject.GetComponentInParent<WeedManager>();

        WeedName = GameObject.Find("WeedName");
        NameText = WeedName.GetComponent<TMP_Text>();

        ModelA = gameObject.transform.GetChild(1).gameObject;
        ModelB = gameObject.transform.GetChild(2).gameObject;
        ModelC = gameObject.transform.GetChild(3).gameObject;

        CurrentModel = gameObject.transform.GetChild(1).gameObject;   
    }

    void OnMouseEnter()
    {
        Highliter.highlightObj(CurrentModel);
        NameText.enabled = true;
        NameText.text = this.gameObject.name;
    }
    void OnMouseExit()
    {
        Highliter.UnhighlightObj(CurrentModel);
        NameText.enabled = false;
    }

    void OnMouseDown()
    {
        if(state < 4)
        {
            UpdateState();
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
            break;

            //StateC
            case 2:
            ModelB.SetActive(false);
            ModelC.SetActive(true);
            CurrentModel = gameObject.transform.GetChild(3).gameObject;
            break;

            //Gone
            case 3:
            ModelC.SetActive(false);
            CurrentModel = null;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Manager.updateWeedTotal();
            break;
        }
    }
}
