using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void highlightObj(GameObject TargetObj)
    {
        if(TargetObj != null)
        {
            foreach(Transform child in TargetObj.transform)
            {
                if(child.gameObject.tag != "Particle")
                {
                    child.gameObject.layer = 31;
                }
            
            }
        }
    }

    public void UnhighlightObj(GameObject TargetObj)
    {
        if(TargetObj != null)
        {
            foreach(Transform child in TargetObj.transform)
            {
                if(child.gameObject.tag != "Particle")
                {
                    child.gameObject.layer = 0;
                }
            
            }
        }
        
    }
    
}
