using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using Anil;

public class LevelManager : MonoBehaviour
{
    public Button[] LevelButtons;
    public int Level;
    public Sprite LockedButtonImage;
    public AudioSource ButtonSound;

    MemoryManagement _MemoManage = new MemoryManagement();

    void Start()
    {
        ButtonSound.volume = _MemoManage.ReadData_f("MenuFx");

        int CurrentLevel = _MemoManage.ReadData_i("LastPlayed") - 4;
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
