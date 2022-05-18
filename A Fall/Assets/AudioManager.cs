using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioMixer mixer;
    public static AudioManager instance;
    AudioSource generalAudioSource;

    public List<AudioClip> audioClips = new List<AudioClip>();

    AudioClip lastClip;

    private void Awake()
    {
        instance = this;
        generalAudioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) PlayGlobalSound(audioClips[0]);
        if (Input.GetKeyDown(KeyCode.C)) PauseCurrent();
        if (Input.GetKeyDown(KeyCode.V)) UnPauseCurrent();
    }

    public AudioManager PlayGlobalSound(AudioClip clip)
    {
        isPaused = false;
        lastClip = generalAudioSource.clip;
        generalAudioSource.PlayOneShot(clip);
        StartCoroutine(ClipTimer(clip));


        return this;
    }

    public AudioClip GetLastClip()
    {
        return lastClip;
    }

    public AudioClip GetCurrentClip()
    {
        return generalAudioSource.clip;
    }

    bool isPaused;
    IEnumerator ClipTimer(AudioClip clip)
    {
        float t = 0f;

        while(t < clip.length)
        {
            while (isPaused) yield return null;

            t += Time.deltaTime;
            yield return null;
        }


        if (t >= clip.length)
        {      
            if (audioClips.Count > 0) audioClips.RemoveAt(0);

            if(audioClips.Count > 0) PlayGlobalSound(audioClips[0]);
        }
    }

    public void QueueClip(AudioClip clip)
    {
        audioClips.Add(clip);
    }
    public void ClearClipList()
    {
        audioClips.Clear();
    }

    public void FinishAll()
    {
        isPaused = false;
        StopAllCoroutines();
        ClearClipList();
    }

    public void PauseCurrent()
    {
        generalAudioSource.Pause();
        isPaused = true;
    }

    public void UnPauseCurrent()
    {
        generalAudioSource.UnPause();
        isPaused = false;
    }

}
