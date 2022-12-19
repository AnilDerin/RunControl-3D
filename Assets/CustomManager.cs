using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Anil;

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
            HatText.text = "Şapka yok";
        }
        else
        {
            HatIndex = _MemManage.ReadData_i("ActiveHat");
            Hats[_MemManage.ReadData_i("ActiveHat")].SetActive(true);
        }
    }

    public void ChangeHat(string op)
    {
        if (op == "forward")
        {
            if (HatIndex == -1)
            {
                HatIndex = 0;
                Hats[HatIndex].SetActive(true);
            }
            else
            {
                Hats[HatIndex].SetActive(false);
                HatIndex++;
                Hats[HatIndex].SetActive(true);
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
                }
                else
                {
                    HatButtons[0].interactable = false;
                }
            }
            else
            {
                HatButtons[0].interactable = false;
            }
            if (HatIndex != Hats.Length - 1)
                HatButtons[1].interactable = true;
        }
    }
}
