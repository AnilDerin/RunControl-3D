using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Anil;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CustomManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HatText;

    [Header("HATS")]
    public GameObject[] Hats;
    public Button[] HatButtons;

    [Header("BATS")]
    public GameObject[] Bats;

    [Header("MATS")]
    public Material[] Materials;

    int HatIndex = -1;

    MemoryManagement _MemManage = new MemoryManagement();
    DataManagement _DataManage = new DataManagement();

    public List<ItemData> _ItemData = new List<ItemData>();

    void Start()
    {
        _MemManage.SaveData_Int("ActiveHat", -1);

        if (_MemManage.ReadData_i("ActiveHat") == -1)
        {
            foreach (var item in Hats)
            {
                item.SetActive(false);
            }
            HatIndex = -1;
            HatText.text = "No Hat";
        }
        else
        {
            HatIndex = _MemManage.ReadData_i("ActiveHat");
            Hats[_MemManage.ReadData_i("ActiveHat")].SetActive(true);
        }

        //_DataManage.Save(_ItemData);

        _DataManage.Load();
        _ItemData = _DataManage.ExportList();
    }

    public void ChangeHat(string op)
    {
        if (op == "forward")
        {
            if (HatIndex == -1)
            {
                HatIndex = 0;
                Hats[HatIndex].SetActive(true);
                HatText.text = _ItemData[HatIndex].Item_Name;
            }
            else
            {
                Hats[HatIndex].SetActive(false);
                HatIndex++;
                Hats[HatIndex].SetActive(true);
                HatText.text = _ItemData[HatIndex].Item_Name;
            }

            if (HatIndex == Hats.Length - 1)
                HatButtons[1].interactable = false;
            else
                HatButtons[1].interactable = true;

            if (HatIndex != -1)
                HatButtons[0].interactable = true;
        }
        else
        {
            if (HatIndex != -1)
            {
                Hats[HatIndex].SetActive(false);
                HatIndex--;
                if (HatIndex != -1)
                {
                    Hats[HatIndex].SetActive(true);
                    HatButtons[0].interactable = true;
                    HatText.text = _ItemData[HatIndex].Item_Name;
                }
                else
                {
                    HatButtons[0].interactable = false;
                    HatText.text = "No Hat";
                }
            }
            else
            {
                HatButtons[0].interactable = false;
            }
            if (HatIndex != Hats.Length - 1)
            {
                HatButtons[1].interactable = true;
            }
        }
        Debug.Log(HatIndex);
    }
}
