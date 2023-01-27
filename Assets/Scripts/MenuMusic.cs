using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private static GameObject Instance;

    public AudioSource _Music;
    void Start()
    {
        _Music.volume = PlayerPrefs.GetFloat("MenuBGM");
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = gameObject;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        _Music.volume = PlayerPrefs.GetFloat("MenuBGM");
    }
}
