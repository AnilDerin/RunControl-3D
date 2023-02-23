using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Anil;

public class MarketManager : MonoBehaviour
{

    MemoryManagement _MemManage = new MemoryManagement();
    DataManagement _ItemData = new DataManagement();

    public List<LanguageDataMain> _LangDataMain = new List<LanguageDataMain>();
    List<LanguageDataMain> _LangReadData = new List<LanguageDataMain>();
    public TextMeshProUGUI[] TextObjects;

    void Start()
    {
        _ItemData.LoadLang();
        _LangReadData = _ItemData.ExportLangList();
        _LangDataMain.Add(_LangReadData[3]);
        LanguageDetect();
    }

    public void LanguageDetect()
    {
        if (_MemManage.ReadData_s("Language") == "TR")
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = _LangDataMain[0]._LangData_TR[i].Text;
            }
        }
        else
        {
            for (int i = 0; i < TextObjects.Length; i++)
            {
                TextObjects[i].text = _LangDataMain[0]._LangData_EN[i].Text;
            }
        }
    }
}
