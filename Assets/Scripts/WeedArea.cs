using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeedArea : MonoBehaviour
{
    private int AreaWeedTotal, CurrentAreaCount;

    [SerializeField] GameObject areaGate;
    private GameObject areaIndicator;

    private GameManager GM;
    private TMP_Text AreaIndicatorText;
    
    // Start is called before the first frame update
    void Start()
    {
        GM = gameObject.GetComponentInParent<GameManager>();

        areaIndicator = GameObject.Find("Weed Area Indicator");

        AreaIndicatorText = areaIndicator.GetComponent<TMP_Text>();


        AreaWeedTotal = this.transform.childCount;
        CurrentAreaCount = AreaWeedTotal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AreaIndicatorText.text = CurrentAreaCount + " / " + AreaWeedTotal + " Weeds In Area";
            //UpdateAreaTotal();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            AreaIndicatorText.text = "No Weeds In Area";
        }    
    }

    public void UpdateAreaTotal()
    {
        switch(CurrentAreaCount)
        {
            case 1:
            AreaWeedTotal --;
            //EndingIndicator.gameObject.SetActive(true); //Rise Minion!
            areaGate.gameObject.SetActive(false); //be GONE thot!
            AreaIndicatorText.text = "No Weeds In Area";
            //end case
            break;

            case > 1:
            CurrentAreaCount --;
            AreaIndicatorText.text = CurrentAreaCount + " / " + AreaWeedTotal + " Weeds In Area";
            break;
        }
    }

}
