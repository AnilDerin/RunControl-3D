using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Anil;
using System;

public class SettingsManager : MonoBehaviour
{


    public AudioSource ButtonSound;
    public Slider MenuBGM;
    public Slider MenuFx;
    public Slider GameBGM;

    MemoryManagement _MemManage = new MemoryManagement();
    DataManagement _ItemData = new DataManagement();

    public List<LanguageDataMain> _LangDataMain = new List<LanguageDataMain>();
    List<LanguageDataMain> _LangReadData = new List<LanguageDataMain>();


    public TextMeshProUGUI[] TextObjects;

    [Header("---------LANG PREF OBJ---------")]
    public TextMeshProUGUI LangTextObj;
    public Button[] LangTextButtons;
    int ActiveLangIndex;



    // Start is called before the first frame update
    void Start()
    {
        ButtonSound.volume = _MemManage.ReadData_f("MenuFx");

        MenuBGM.value = _MemManage.ReadData_f("MenuBGM");
        MenuFx.value = _MemManage.ReadData_f("MenuFx");
        GameBGM.value = _MemManage.ReadData_f("GameBGM");

        _ItemData.LoadLang();
        _LangReadData = _ItemData.ExportLangList();
        _LangDataMain.Add(_LangReadData[4]);
        LanguageDetect();
        CheckLangStatus();
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

    public void GoBack(string Lang)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }

    private void LanguageDetect()
    {
        if (_MemManage.ReadData_s("Language") == "TR")
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = _LangDataMain[0]._LangData_TR[i].Text;
            }
        }
        else
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = _LangDataMain[0]._LangData_EN[i].Text;
            }
        }
    }

    void CheckLangStatus()
    {
        if (_MemManage.ReadData_s("Language") == "EN")
        {
            ActiveLangIndex = 0;
            LangTextObj.text = "ENGLISH";
            LangTextButtons[0].interactable = false;
        }
        else
        {
            ActiveLangIndex = 1;
            LangTextObj.text = "TÜRKÇE";
            LangTextButtons[1].interactable = false;
        }
    }
    public void ChangeLanguage(string Direction)
    {

        if (Direction == "forward")
        {
            ActiveLangIndex = 1;
            LangTextObj.text = "TÜRKÇE";
            LangTextButtons[0].interactable = true;
            LangTextButtons[1].interactable = false;
            _MemManage.SaveData_String("Language", "TR");
            LanguageDetect();
        }

        else
        {
            ActiveLangIndex = 0;

            LangTextObj.text = "ENGLISH";
            LangTextButtons[0].interactable = false;
            LangTextButtons[1].interactable = true;
            _MemManage.SaveData_String("Language", "EN");
            LanguageDetect();
        }



        ButtonSound.Play();

    }

    public void GoBack()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }

}
