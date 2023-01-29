using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeedManager : MonoBehaviour
{
    [SerializeField] GameObject WeedTotalIndicator;
    TMP_Text IndicatorText;
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
        IndicatorText.text = CurrentWeeds + " / " + TotalWeeds + " Weeds Remaining";
    }

    public void updateWeedTotal()
    {
        CurrentWeeds --;
        IndicatorText.text = CurrentWeeds + " / " + TotalWeeds + " Weeds Remaining";
    }
}
