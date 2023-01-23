using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Anil;

public class MainMenu_Manager : MonoBehaviour
{
    MemoryManagement _MemManage = new MemoryManagement();
    DataManagement _ItemData = new DataManagement();


    public GameObject QuitPanel;
    public List<ItemData> _ItemInfo = new List<ItemData>();
    public AudioSource ButtonSound;

    MathOps _MathOps = new MathOps();

    void Start()
    {

        _MemManage.CheckAndDefine();
        _ItemData.FirstBuildUp(_ItemInfo);
        _MemManage.SaveData_Int("LastPlayed", 5);
    }

    public void LoadScene(int Index)
    {
        ButtonSound.Play();
        SceneManager.LoadScene(Index);
    }

    public void Play()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(_MemManage.ReadData_i("LastPlayed"));
    }

    public void QuitButtonBehavior(string behavior)
    {

        ButtonSound.Play();
        if (behavior == "Yes")
            Application.Quit();
        else if (behavior == "Exit")
            QuitPanel.SetActive(true);
        else
        {
            QuitPanel.SetActive(false);
        }
    }
}
