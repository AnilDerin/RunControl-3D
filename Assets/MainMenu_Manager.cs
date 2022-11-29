using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Anil;

public class MainMenu_Manager : MonoBehaviour
{
    MemoryManagement _MemManage = new MemoryManagement();

    public GameObject QuitPanel;

    //MathOps _MathOps = new MathOps();

    // Start is called before the first frame update
    void Start()
    {
        _MemManage.CheckAndDefine();
    }

    public void LoadScene(int Index)
    {
        SceneManager.LoadScene(Index);
    }

    public void Play()
    {
        SceneManager.LoadScene(_MemManage.ReadData_i("LastPlayed"));
    }

    public void Exit()
    {
        QuitPanel.SetActive(true);
    }

    public void QuitButtonBehavior(string behavior)
    {
        if (behavior == "Yes")
            Application.Quit();
        else
        {
            QuitPanel.SetActive(false);
        }
    }
}
