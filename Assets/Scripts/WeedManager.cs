using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeedManager : MonoBehaviour
{
    [SerializeField] GameObject WeedTotalIndicator;
    [SerializeField] GameObject EndingIndicator;
    [SerializeField] GameObject GateObject;
    TMP_Text IndicatorText, EndText;
    private int TotalWeeds = 0, CurrentWeeds;
    
    void Start()
    {
        foreach(Transform child in this.transform)
        {
            TotalWeeds++;
            Debug.Log(TotalWeeds);
        }
        CurrentWeeds = TotalWeeds;
        IndicatorText = WeedTotalIndicator.GetComponent<TMP_Text>();
        EndText = EndingIndicator.GetComponent<TMP_Text>();
        IndicatorText.text = CurrentWeeds + " / " + TotalWeeds + " Weeds";
    }

    public void updateWeedTotal()
    {
        switch(CurrentWeeds)
        {
            case 1:
            TotalWeeds --;
            EndingIndicator.gameObject.SetActive(true); //Rise Minion!
            GateObject.gameObject.SetActive(false); //be GONE thot!
            IndicatorText.text = "All weeds clear";
            //end case
            break;

            case > 1:
            CurrentWeeds --;
            IndicatorText.text = CurrentWeeds + " / " + TotalWeeds + " Weeds";
            break;
        }
        //CurrentWeeds --;
        //IndicatorText.text = CurrentWeeds + " / " + TotalWeeds + " Weeds Remaining";
         
    }

    public void UpdateDistance()
    {
        
    }
}
