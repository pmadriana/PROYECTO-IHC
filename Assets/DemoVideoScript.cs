using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DemoVideoScript : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioClip audio;
    public bool startVideo = false;
    // Start is called before the first frame update
    void Start()
    {
        startVideo = false;
    }

    void Update()
    {
        if (startVideo)
        {
            StartCoroutine(playVideo());
            startVideo = false;

        }    
    }

    IEnumerator playVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        
        while (!videoPlayer.isPrepared) {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
       
    }
}
