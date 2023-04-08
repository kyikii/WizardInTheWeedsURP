using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeedArea : MonoBehaviour
{
    private int AreaWeedTotal,CurrentAreaCount;

    [SerializeField] public GameObject areaGate;
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (CurrentAreaCount)
            {
                case 1:

                    AreaIndicatorText.text = "No Weeds In Area";
                    break;

                case > 1:
                    AreaIndicatorText.text = CurrentAreaCount + " / " + AreaWeedTotal + " Weeds In Area";
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AreaIndicatorText.text = "No Weeds In Area";
        }
    }

    public void UpdateAreaTotal()
    {
        switch (CurrentAreaCount)
        {
            case 1:
                CurrentAreaCount--;
                areaGate.SetActive(false);
                AreaIndicatorText.text = "No Weeds In Area";
                break;

            case > 1:
                CurrentAreaCount--;
                AreaIndicatorText.text = CurrentAreaCount + " / " + AreaWeedTotal + " Weeds In Area";
                break;
        }
    }

}
