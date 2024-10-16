using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_AudioSource_Trigger : MonoBehaviour
{
    public List<AudioSource> audioList;

    private void OnEnable()
    {
        foreach(AudioSource aud_s in audioList)
        {
            if (audioList.Count > 0)
            {
                if (aud_s != null)
                {
                    aud_s.Play();
                }
                else { Debug.Log("Audio in list of gameobject " + gameObject + " is NULL"); }
            }
            else
            {
                Debug.Log("Audio in list of gameobject " + gameObject + " is not attached or empty");
            }
        }
    }
}
