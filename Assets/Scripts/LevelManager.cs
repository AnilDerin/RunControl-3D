using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using Anil;
using System;

public class LevelManager : MonoBehaviour
{
    public Button[] LevelButtons;
    public int Level;
    public Sprite LockedButtonImage;
    public AudioSource ButtonSound;

    MemoryManagement _MemManage = new MemoryManagement();
    DataManagement _ItemData = new DataManagement();

    public List<LanguageDataMain> _LangDataMain = new List<LanguageDataMain>();
    List<LanguageDataMain> _LangReadData = new List<LanguageDataMain>();
    public TextMeshProUGUI TextObjects;


    void Start()
    {
        _ItemData.LoadLang();
        _LangReadData = _ItemData.ExportLangList();
        _LangDataMain.Add(_LangReadData[2]);
        LanguageDetect();


        ButtonSound.volume = _MemManage.ReadData_f("MenuFx");

        int CurrentLevel = _MemManage.ReadData_i("LastPlayed") - 4;
        //Debug.Log(_MemoManage.ReadData_i("LastPlayed"));

        int Index = 1;
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            if (i + 1 <= CurrentLevel)
            {
                LevelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = Index.ToString();
                int SceneIndex = Index + 4;
                LevelButtons[i].onClick.AddListener(
                    delegate
                    {
                        LoadTheScene(SceneIndex);
                    }
                );
            }
            else
            {
                LevelButtons[i].GetComponent<Image>().sprite = LockedButtonImage;
            }
            Index++;
        }


    }

    public void LanguageDetect()
    {
        if (_MemManage.ReadData_s("Language") == "TR")
            TextObjects.text = _LangDataMain[0]._LangData_TR[0].Text;

        else
            TextObjects.text = _LangDataMain[0]._LangData_EN[0].Text;
    }

    public void LoadTheScene(int Index)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(Index);
    }

    /* public void LoadTheScene()
     {
         Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text);
         SceneManager.LoadScene(int.Parse(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text) + 4);
     }
     */


    public void GoBack()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }
}
