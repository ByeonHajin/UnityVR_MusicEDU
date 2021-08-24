using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void VideoControl(int num) {
        videoPlayer.clip = videos[num];

        videoPlayer.Play();
    }

}
