using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public GameObject[] generalPanels;
    public Button[] opButtons;

    public TextMeshProUGUI BuyText;

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

    public Animator Saved_Anim;

    void Start()
    {
        ScoreText.text = _MemManage.ReadData_i("Score").ToString();

        //_DataManage.Save(_ItemData);

        _DataManage.Load();
        _ItemData = _DataManage.ExportList();

        CheckStatus(0, true);
        CheckStatus(1, true);
        CheckStatus(2, true);

    }

    void CheckStatus(int Section, bool operation = false)
    {
        if (Section == 0)
        {
            if (_MemManage.ReadData_i("ActiveHat") == -1)
            {
                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }
                opButtons[0].interactable = false;
                opButtons[1].interactable = false;

                if (!operation)
                {
                    HatIndex = -1;
                    HatText.text = "No Hat";
                }


            }
            else
            {

                foreach (var item in Hats)
                {
                    item.SetActive(false);
                }

                HatIndex = _MemManage.ReadData_i("ActiveHat");
                Hats[HatIndex].SetActive(true);

                HatText.text = _ItemData[HatIndex].Item_Name;
                BuyText.text = "BUY";
                opButtons[0].interactable = false;
                opButtons[1].interactable = true;
            }
        }
        else if (Section == 1)
        {
            if (_MemManage.ReadData_i("ActiveBat") == -1)
            {
                foreach (var item in Bats)
                {
                    item.SetActive(false);
                }
                opButtons[0].interactable = false;
                opButtons[1].interactable = false;

                if (!operation)
                {
                    BatIndex = -1;
                    BatText.text = "No Bat";
                }

            }
            else
            {

                foreach (var item in Bats)
                {
                    item.SetActive(false);
                }
                BatIndex = _MemManage.ReadData_i("ActiveBat");
                Bats[BatIndex].SetActive(true);

                BatText.text = _ItemData[BatIndex + 3].Item_Name;
                BuyText.text = "BUY";
                opButtons[0].interactable = false;
                opButtons[1].interactable = true;

            }
        }

        else
        {
            if (_MemManage.ReadData_i("ActiveMat") == -1)
            {

                if (!operation)
                {
                    MatIndex = -1;
                    MatText.text = "No Theme";
                    opButtons[0].interactable = false;
                    opButtons[1].interactable = false;
                }
                else
                {
                    Material[] matties = _Renderer.materials;
                    matties[0] = DefaultMat;
                    _Renderer.materials = matties;
                }

            }
            else
            {
                MatIndex = _MemManage.ReadData_i("ActiveMat");
                Material[] matties = _Renderer.materials;
                matties[0] = Mats[MatIndex];
                _Renderer.materials = matties;


                MatText.text = _ItemData[MatIndex + 6].Item_Name;
                BuyText.text = "BUY";
                opButtons[0].interactable = false;
                opButtons[1].interactable = true;

            }
        }
    }

    public void BuyItem()
    {
        if (activeOpIndex != -1)
        {
            switch (activeOpIndex)
            {
                case 0:
                    BuyResult(HatIndex);
                    break;
                case 1:
                    int Index = BatIndex + 3;
                    BuyResult(Index);
                    break;
                case 2:
                    int Index_Mat = MatIndex + 6;
                    BuyResult(Index_Mat);
                    break;
            }
        }



    }

    public void SaveItem()
    {
        if (activeOpIndex != -1)
        {
            switch (activeOpIndex)
            {
                case 0:
                    SaveItemResult("ActiveHat", HatIndex);
                    break;

                case 1:
                    SaveItemResult("ActiveBat", BatIndex);
                    break;

                case 2:
                    SaveItemResult("ActiveMat", MatIndex);
                    break;
            }
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
                HatText.text = _ItemData[HatIndex].Item_Name;

                if (!_ItemData[HatIndex].BuyStatus)
                {
                    BuyText.text = "BUY -  " + _ItemData[HatIndex].Score;
                    opButtons[1].interactable = false;

                    if (_MemManage.ReadData_i("Score") < _ItemData[HatIndex].Score)
                        opButtons[0].interactable = false;
                    else
                        opButtons[0].interactable = true;

                }
                else
                {
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
                    opButtons[1].interactable = true;
                }

            }
            else
            {
                Hats[HatIndex].SetActive(false);
                HatIndex++;
                Hats[HatIndex].SetActive(true);
                HatText.text = _ItemData[HatIndex].Item_Name;

                if (!_ItemData[HatIndex].BuyStatus)
                {
                    BuyText.text = "BUY -  " + _ItemData[HatIndex].Score;
                    opButtons[1].interactable = false;
                    if (_MemManage.ReadData_i("Score") < _ItemData[HatIndex].Score)
                        opButtons[0].interactable = false;
                    else
                        opButtons[0].interactable = true;

                }
                else
                {
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
                    opButtons[1].interactable = true;
                }
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
                    if (!_ItemData[HatIndex].BuyStatus)
                    {
                        BuyText.text = "BUY -  " + _ItemData[HatIndex].Score;
                        opButtons[1].interactable = false;
                        if (_MemManage.ReadData_i("Score") < _ItemData[HatIndex].Score)
                            opButtons[0].interactable = false;
                        else
                            opButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyText.text = "BUY";
                        opButtons[0].interactable = false;
                        opButtons[1].interactable = true;
                    }
                }
                else
                {
                    HatButtons[0].interactable = false;
                    HatText.text = "No Hat";
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
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

                if (!_ItemData[BatIndex + 3].BuyStatus)
                {
                    BuyText.text = "BUY -  " + _ItemData[BatIndex + 3].Score;
                    opButtons[1].interactable = false;
                    if (_MemManage.ReadData_i("Score") < _ItemData[BatIndex + 3].Score)
                        opButtons[0].interactable = false;
                    else
                        opButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
                    opButtons[1].interactable = true;
                }
            }
            else
            {
                Bats[BatIndex].SetActive(false);
                BatIndex++;
                Bats[BatIndex].SetActive(true);
                BatText.text = _ItemData[BatIndex + 3].Item_Name;

                if (!_ItemData[BatIndex + 3].BuyStatus)
                {
                    BuyText.text = "BUY -  " + _ItemData[BatIndex + 3].Score;
                    opButtons[1].interactable = false;
                    if (_MemManage.ReadData_i("Score") < _ItemData[BatIndex + 3].Score)
                        opButtons[0].interactable = false;
                    else
                        opButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
                    opButtons[1].interactable = true;
                }
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

                    if (!_ItemData[BatIndex + 3].BuyStatus)
                    {
                        BuyText.text = "BUY -  " + _ItemData[BatIndex + 3].Score;
                        opButtons[1].interactable = false;
                        if (_MemManage.ReadData_i("Score") < _ItemData[BatIndex + 3].Score)
                            opButtons[0].interactable = false;
                        else
                            opButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyText.text = "BUY";
                        opButtons[0].interactable = false;
                        opButtons[1].interactable = true;
                    }
                }
                else
                {
                    BatButtons[0].interactable = false;
                    BatText.text = "No Bat";
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
                }
            }
            else
            {
                BatButtons[0].interactable = false;
                BatText.text = "No Bat";
                BuyText.text = "BUY";
                opButtons[0].interactable = false;
            }
            if (BatIndex != Bats.Length - 1)
            {
                BatButtons[1].interactable = true;
            }
        }
        //Debug.Log(BatIndex);
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

                if (!_ItemData[MatIndex + 6].BuyStatus)
                {
                    BuyText.text = "BUY -  " + _ItemData[MatIndex + 6].Score;
                    opButtons[1].interactable = false;

                    if (_MemManage.ReadData_i("Score") < _ItemData[MatIndex + 6].Score)
                        opButtons[0].interactable = false;
                    else
                        opButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
                    opButtons[1].interactable = true;
                }
            }
            else
            {
                MatIndex++;
                Material[] matties = _Renderer.materials;
                matties[0] = Mats[MatIndex];
                _Renderer.materials = matties;

                MatText.text = _ItemData[MatIndex + 6].Item_Name;

                if (!_ItemData[MatIndex + 6].BuyStatus)
                {
                    BuyText.text = "BUY -  " + _ItemData[MatIndex + 6].Score;
                    opButtons[1].interactable = false;
                    if (_MemManage.ReadData_i("Score") < _ItemData[MatIndex + 6].Score)
                        opButtons[0].interactable = false;
                    else
                        opButtons[0].interactable = true;
                }
                else
                {
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
                    opButtons[1].interactable = true;
                }
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

                    if (!_ItemData[MatIndex + 6].BuyStatus)
                    {
                        BuyText.text = "BUY -  " + _ItemData[MatIndex + 6].Score;
                        opButtons[1].interactable = false;
                        if (_MemManage.ReadData_i("Score") < _ItemData[MatIndex + 6].Score)
                            opButtons[0].interactable = false;
                        else
                            opButtons[0].interactable = true;
                    }
                    else
                    {
                        BuyText.text = "BUY";
                        opButtons[0].interactable = false;
                        opButtons[1].interactable = true;
                    }
                }
                else
                {
                    Material[] matties = _Renderer.materials;
                    matties[0] = DefaultMat;
                    _Renderer.materials = matties;

                    MatButtons[0].interactable = false;
                    MatText.text = "No Theme";
                    BuyText.text = "BUY";
                    opButtons[0].interactable = false;
                }
            }
            else
            {
                Material[] matties = _Renderer.materials;
                matties[0] = DefaultMat;
                _Renderer.materials = matties;

                MatButtons[0].interactable = false;
                MatText.text = "No Theme";
                BuyText.text = "BUY";
                opButtons[0].interactable = false;
            }
            if (MatIndex != Mats.Length - 1)
            {
                MatButtons[1].interactable = true;
            }
        }
        //Debug.Log(MatIndex);
    }

    public void opShowPanel(int Index)
    {
        CheckStatus(Index);
        generalPanels[0].SetActive(true);
        activeOpIndex = Index;
        opPanels[Index].SetActive(true);
        generalPanels[1].SetActive(true);
        opCanvas.SetActive(false);
    }

    public void GoBack()
    {

        generalPanels[0].SetActive(false);
        opCanvas.SetActive(true);
        generalPanels[1].SetActive(false);
        opPanels[activeOpIndex].SetActive(false);
        CheckStatus(activeOpIndex, true);
        activeOpIndex = -1;



    }

    public void ReturnToMainMenu()
    {
        _DataManage.Save(_ItemData);
        SceneManager.LoadScene(0);
    }


    //----------------------//


    void BuyResult(int Index)
    {
        _ItemData[Index].BuyStatus = true;
        _MemManage.SaveData_Int("Score", _MemManage.ReadData_i("Score") - _ItemData[Index].Score);
        BuyText.text = "BUY";
        opButtons[0].interactable = false;
        opButtons[1].interactable = true;
        ScoreText.text = _MemManage.ReadData_i("Score").ToString();
    }

    void SaveItemResult(string key, int Index)
    {
        _MemManage.SaveData_Int(key, Index);
        opButtons[1].interactable = false;
        if (!Saved_Anim.GetBool("isSaved"))
            Saved_Anim.SetBool("isSaved", true);
    }
}
