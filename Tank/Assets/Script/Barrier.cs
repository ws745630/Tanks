using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip hitAudio;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void PlayerAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}
