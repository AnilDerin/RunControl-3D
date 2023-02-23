using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Anil;

public class MainMenu_Manager : MonoBehaviour
{
    MemoryManagement _MemManage = new MemoryManagement();
    DataManagement _ItemData = new DataManagement();
    AdManagement _AdManage = new AdManagement();


    public GameObject QuitPanel;
    public List<ItemData> _Default_ItemInfo = new List<ItemData>();
    public List<LanguageDataMain> _Default_Lang = new List<LanguageDataMain>();


    public List<LanguageDataMain> _LangDataMain = new List<LanguageDataMain>();
    List<LanguageDataMain> _LangReadData = new List<LanguageDataMain>();

    public TextMeshProUGUI[] TextObjects;
    public AudioSource ButtonSound;
    public GameObject LoadingScreen;
    public Slider LoadingSlider;

    MathOps _MathOps = new MathOps();

    [System.Obsolete]
    void Start()
    {

        _MemManage.CheckAndDefine();
        _ItemData.FirstBuildUp(_Default_ItemInfo, _Default_Lang);
        ButtonSound.volume = _MemManage.ReadData_f("MenuFx");

        //_MemManage.SaveData_String("Language", "EN");


        _ItemData.LoadLang();
        _LangReadData = _ItemData.ExportLangList();
        _LangDataMain.Add(_LangReadData[0]);
        LanguageDetect();


    }


    public void LanguageDetect()
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


    public void LoadScene(int Index)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(Index);
    }

    public void Play()
    {
        ButtonSound.Play();

        StartCoroutine(LoadAsync(_MemManage.ReadData_i("LastPlayed")));
    }

    IEnumerator LoadAsync(int SceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            LoadingSlider.value = progress;
            yield return null;
        }


    }

    public void QuitButtonBehavior(string behavior)
    {

        ButtonSound.Play();
        if (behavior == "Yes")
            Application.Quit();
        else if (behavior == "Exit")
            QuitPanel.SetActive(true);
        else
            QuitPanel.SetActive(false);
    }
}
