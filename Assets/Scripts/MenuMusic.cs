using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private static GameObject Instance;

    public AudioSource _Music;
    void Start()
    {
        // _Music.volume = PlayerPrefs.GetFloat("MenuMusic"); //buraya bakmayı unutma
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = gameObject;
        else
            Destroy(gameObject);
    }

    void Update()
    {

    }
}
