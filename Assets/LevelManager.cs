using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Anil;

public class LevelManager : MonoBehaviour
{
    public Button[] LevelButtons;
    public int Level;
    public Sprite LockedButtonImage;

    MemoryManagement _MemoManage = new MemoryManagement();

    void Start()
    {
        _MemoManage.SaveData_Int("LastPlayed", Level);

        int CurrentLevel = _MemoManage.ReadData_i("LastPlayed") - 4;

        for (int i = 0; i < LevelButtons.Length; i++)
        {
            if (i + 1 <= CurrentLevel)
            {
                LevelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
                int Index = i + 1;
                //LevelButtons[i].interactable = true;
            }
            else
            {
                LevelButtons[i].GetComponent<Image>().sprite = LockedButtonImage;
            }
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
