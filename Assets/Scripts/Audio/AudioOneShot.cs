using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneShot : MonoBehaviour
{

    protected AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        audioSource.Play();
        float audioClipLength = audioSource.clip.length;
        StartCoroutine(DestroyAudio(audioClipLength));
    }

    public IEnumerator DestroyAudio(float audioClipLength)
    {
        yield return new WaitForSecondsRealtime(audioClipLength);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
