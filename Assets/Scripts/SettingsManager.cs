using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Anil;

public class SettingsManager : MonoBehaviour
{


    public AudioSource ButtonSound;
    public Slider MenuBGM;
    public Slider MenuFx;
    public Slider GameBGM;

    MemoryManagement _MemManage = new MemoryManagement();



    // Start is called before the first frame update
    void Start()
    {
        ButtonSound.volume = _MemManage.ReadData_f("MenuFx");

        MenuBGM.value = _MemManage.ReadData_f("MenuBGM");
        MenuFx.value = _MemManage.ReadData_f("MenuFx");
        GameBGM.value = _MemManage.ReadData_f("GameBGM");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoBack()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }

    public void AdjustVolume(string Selection)
    {
        switch (Selection)

        {
            case "MenuBGM":
                _MemManage.SaveData_Float("MenuBGM", MenuBGM.value);
                break;

            case "MenuFx":
                _MemManage.SaveData_Float("MenuFx", MenuFx.value);
                break;

            case "GameBGM":
                _MemManage.SaveData_Float("GameBGM", GameBGM.value);
                break;

        }
    }

    public void ChangeLanguage()
    {
        ButtonSound.Play();

    }
}
