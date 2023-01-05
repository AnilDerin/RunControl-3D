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

    public GameObject[] opPanels;
    public GameObject opCanvas;
    public GameObject[] generalObjects;
    int activeOpIndex;

    [Header("HATS")]
    public GameObject[] Hats;
    public Button[] HatButtons;
    public TextMeshProUGUI HatText;

    [Header("BATS")]
    public GameObject[] Bats;
    public Button[] BatButtons;
    public TextMeshProUGUI BatText;

    [Header("MATS")]
    public Material[] Mats;
    public Material DefaultMat;
    public Button[] MatButtons;
    public TextMeshProUGUI MatText;
    public SkinnedMeshRenderer _Renderer;

    int HatIndex = -1;
    int BatIndex = -1;
    int MatIndex = -1;

    MemoryManagement _MemManage = new MemoryManagement();
    DataManagement _DataManage = new DataManagement();

    [Header("GENERAL DATA")]
    public List<ItemData> _ItemData = new List<ItemData>();

    void Start()
    {
        _MemManage.SaveData_Int("ActiveHat", -1);
        _MemManage.SaveData_Int("ActiveBat", -1);
        _MemManage.SaveData_Int("ActiveMat", -1);

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
            Hats[HatIndex].SetActive(true);
        }

        if (_MemManage.ReadData_i("ActiveBat") == -1)
        {
            foreach (var item in Bats)
            {
                item.SetActive(false);
            }
            BatIndex = -1;
            BatText.text = "No Bat";
        }
        else
        {
            BatIndex = _MemManage.ReadData_i("ActiveBat");
            Bats[BatIndex].SetActive(true);
        }

        if (_MemManage.ReadData_i("ActiveMat") == -1)
        {
            MatIndex = -1;
            MatText.text = "No Theme";
        }
        else
        {
            MatIndex = _MemManage.ReadData_i("ActiveMat");
            Material[] matties = _Renderer.materials;
            matties[0] = Mats[0];
            _Renderer.materials = matties;
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
                HatText.text = "No Hat";
            }
            if (HatIndex != Hats.Length - 1)
            {
                HatButtons[1].interactable = true;
            }
        }
        Debug.Log(HatIndex);
    }

    public void ChangeBat(string op)
    {
        if (op == "forward")
        {
            if (BatIndex == -1)
            {
                BatIndex = 0;
                Bats[BatIndex].SetActive(true);
                BatText.text = _ItemData[BatIndex + 3].Item_Name;
            }
            else
            {
                Bats[BatIndex].SetActive(false);
                BatIndex++;
                Bats[BatIndex].SetActive(true);
                BatText.text = _ItemData[BatIndex + 3].Item_Name;
            }

            if (BatIndex == Bats.Length - 1)
                BatButtons[1].interactable = false;
            else
                BatButtons[1].interactable = true;

            if (BatIndex != -1)
                BatButtons[0].interactable = true;
        }
        else
        {
            if (BatIndex != -1)
            {
                Bats[BatIndex].SetActive(false);
                BatIndex--;
                if (BatIndex != -1)
                {
                    Bats[BatIndex].SetActive(true);
                    BatButtons[0].interactable = true;
                    BatText.text = _ItemData[BatIndex + 3].Item_Name;
                }
                else
                {
                    BatButtons[0].interactable = false;
                    BatText.text = "No Bat";
                }
            }
            else
            {
                BatButtons[0].interactable = false;
                BatText.text = "No Bat";
            }
            if (BatIndex != Bats.Length - 1)
            {
                BatButtons[1].interactable = true;
            }
        }
        Debug.Log(BatIndex);
    }

    public void ChangeMat(string op)
    {
        if (op == "forward")
        {
            if (MatIndex == -1)
            {
                MatIndex = 0;
                Material[] matties = _Renderer.materials;
                matties[0] = Mats[MatIndex];
                _Renderer.materials = matties;

                MatText.text = _ItemData[MatIndex + 6].Item_Name;
            }
            else
            {
                MatIndex++;
                Material[] matties = _Renderer.materials;
                matties[0] = Mats[MatIndex];
                _Renderer.materials = matties;

                MatText.text = _ItemData[MatIndex + 6].Item_Name;
            }

            if (MatIndex == Mats.Length - 1)
                MatButtons[1].interactable = false;
            else
                MatButtons[1].interactable = true;

            if (MatIndex != -1)
                MatButtons[0].interactable = true;
        }
        else
        {
            if (MatIndex != -1)
            {
                MatIndex--;
                if (MatIndex != -1)
                {
                    Material[] matties = _Renderer.materials;
                    matties[0] = Mats[MatIndex];
                    _Renderer.materials = matties;

                    MatButtons[0].interactable = true;
                    MatText.text = _ItemData[MatIndex + 6].Item_Name;
                }
                else
                {
                    Material[] matties = _Renderer.materials;
                    matties[0] = DefaultMat;
                    _Renderer.materials = matties;

                    MatButtons[0].interactable = false;
                    MatText.text = "No Theme";
                }
            }
            else
            {
                Material[] matties = _Renderer.materials;
                matties[0] = DefaultMat;
                _Renderer.materials = matties;

                MatButtons[0].interactable = false;
                MatText.text = "No Theme";
            }
            if (MatIndex != Mats.Length - 1)
            {
                MatButtons[1].interactable = true;
            }
        }
        Debug.Log(MatIndex);
    }

    public void opShowPanel(int Index)
    {
        generalObjects[2].SetActive(true);
        activeOpIndex = Index;
        opPanels[Index].SetActive(true);
        generalObjects[3].SetActive(true);
        opCanvas.SetActive(false);
    }

    public void GoBack()
    {
        generalObjects[2].SetActive(false);
        opCanvas.SetActive(true);
        generalObjects[3].SetActive(false);
        opPanels[activeOpIndex].SetActive(false);
    }
}
