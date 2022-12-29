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

    MathOps _MathOps = new MathOps();

    // Start is called before the first frame update
    void Start()
    {
        _MemManage.CheckAndDefine();
        //_ItemData.FirstBuildUp(_ItemInfo); set active at end!
    }

    public void LoadScene(int Index)
    {
        SceneManager.LoadScene(Index);
    }

    public void Play()
    {
        SceneManager.LoadScene(_MemManage.ReadData_i("LastPlayed"));
    }

    public void QuitButtonBehavior(string behavior)
    {
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
