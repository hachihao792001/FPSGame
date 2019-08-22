using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject AudioObj;
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    public GameObject CreateAudioObj(string name, Transform pr)
    {
        GameObject audioObj = null;
        for (int i = 0; i < sounds.Length; i++)
            if (sounds[i].name == name)
            {
                audioObj = Instantiate(AudioObj, pr);
                audioObj.transform.localPosition = Vector3.zero;
                audioObj.GetComponent<AudioSource>().clip = sounds[i].clip;
                audioObj.GetComponent<AudioSource>().volume = sounds[i].volume;
                audioObj.GetComponent<DisappearAfterSeconds>().seconds = audioObj.GetComponent<AudioSource>().clip.length;
                break;
            }
        return audioObj;
    }

    public void PlaySound(string name, Transform pr, float blend, float maxDist, float volume)
    {
        AudioSource audio = CreateAudioObj(name, pr).GetComponent<AudioSource>();
        audio.volume = Mathf.Min(volume, OptionScreenScript.allSound);
        audio.spatialBlend = blend;
        audio.rolloffMode = AudioRolloffMode.Linear;
        audio.minDistance = 0f;
        audio.maxDistance = maxDist;
        audio.Play();
    }
}