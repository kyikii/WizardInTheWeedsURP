using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsAudio : MonoBehaviour 
{
    
    [SerializeField] AudioClip Shovel,Rake,Shears;
    AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<AudioSource>();
    }
    public void PlayRake()
    {
        player.clip = Rake;
        player.Play();
    }

    public void PlayShears()
    {
        player.clip = Shears;
        player.Play();
    }

    public void PlayShovel()
    {
        player.clip = Shovel;
        player.Play();
    }
}
