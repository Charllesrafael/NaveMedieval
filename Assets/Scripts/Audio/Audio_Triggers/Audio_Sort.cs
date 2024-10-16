using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Sort : MonoBehaviour
{
    public List<GameObject> audios;
    private void OnEnable()
    {
        if (audios.Count > 0)
        {
            int id_audio = Random.Range(0, audios.Count - 1);
            audios[id_audio].SetActive(true);
        }
    }
}
