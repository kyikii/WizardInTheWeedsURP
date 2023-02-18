using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool holding;
    public GameObject heldObject;
    public float PlayerDist;

    //public AudioSource PickUp,PutDown;
    // Start is called before the first frame update
    void Start()
    {

        holding = false;
    }
    public void UpdateDistance()
    {
        
    }

    //public void 

    /*public void updateDistanace()  
    {
        BroadcastMessage("updateDistanceCheck");
    }*/
}
