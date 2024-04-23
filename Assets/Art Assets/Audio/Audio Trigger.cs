using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
        Debug.Log("Start Moosik");
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
