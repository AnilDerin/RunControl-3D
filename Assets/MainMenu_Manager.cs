using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    public void LoadScene(int Index)
    {
        SceneManager.LoadScene(Index);
    }
}
