using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Anil
{
    public class MathOps
    {
        public void Multiply(
            int GivenNumber,
            List<GameObject> Characters,
            Transform Posizyon,
            List<GameObject> SpawnEffects
        )
        {
            int DonguSayisi;
            if (GameManager.CurrentCharCount == 0)
                DonguSayisi = (GameManager.CurrentCharCount + 1);
            else
                DonguSayisi =
                    (GameManager.CurrentCharCount * GivenNumber) - GameManager.CurrentCharCount;

            int sayi = 0;
            foreach (var item in Characters)
            {
                if (GameManager.CurrentCharCount == 0)
                {
                    GameManager.CurrentCharCount++;
                    if (sayi <= DonguSayisi)
                    {
                        if (!item.activeInHierarchy)
                        {
                            foreach (var item2 in SpawnEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    item2.SetActive(true);
                                    item2.transform.position = Posizyon.position;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }

                            item.transform.position = Posizyon.position - new Vector3(0, 0, 0.2f);
                            item.SetActive(true);
                            sayi++;
                        }
                    }
                    else
                    {
                        sayi = 0;
                        break;
                    }
                }
                else
                {
                    if (sayi < DonguSayisi)
                    {
                        foreach (var item2 in SpawnEffects)
                        {
                            if (!item.activeInHierarchy)
                            {
                                item.transform.position =
                                    Posizyon.position - new Vector3(0, 0, 0.2f);
                                item.SetActive(true);
                                item2.SetActive(true);
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                sayi++;
                                break;
                            }
                        }
                    }
                    else
                    {
                        sayi = 0;
                        break;
                    }
                }
            }
            GameManager.CurrentCharCount *= GivenNumber;
        }

        public void Add(
            int GivenNumber,
            List<GameObject> Characters,
            Transform Posizyon,
            List<GameObject> SpawnEffects
        )
        {
            int sayi2 = 0;
            foreach (var item in Characters)
            {
                if (sayi2 < GivenNumber)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in SpawnEffects)
                        {
                            if (!item2.activeInHierarchy)
                            {
                                item2.SetActive(true);
                                item2.transform.position = Posizyon.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();

                                break;
                            }
                        }

                        item.transform.position = Posizyon.position - new Vector3(0, 0, 0.2f);
                        item.SetActive(true);
                        sayi2++;
                    }
                }
                else
                {
                    sayi2 = 0;
                    break;
                }
            }
            GameManager.CurrentCharCount += GivenNumber;
        }

        public void Subtract(
            int GivenNumber,
            List<GameObject> Characters,
            List<GameObject> DestroyEffects
        )
        {
            if (GameManager.CurrentCharCount < GivenNumber)
            {
                foreach (var item in Characters)
                {
                    foreach (var item2 in DestroyEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 yeniPoz = new Vector3(
                                item.transform.position.x,
                                .23f,
                                item.transform.position.z
                            );
                            item2.SetActive(true);
                            item2.transform.position = yeniPoz;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();

                            break;
                        }
                    }

                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.CurrentCharCount = 1;
            }
            else
            {
                int sayi3 = 0;
                foreach (var item in Characters)
                {
                    if (sayi3 != GivenNumber)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DestroyEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 yeniPoz = new Vector3(
                                        item.transform.position.x,
                                        .23f,
                                        item.transform.position.z
                                    );
                                    item2.SetActive(true);
                                    item2.transform.position = yeniPoz;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();

                                    break;
                                }
                            }

                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }
                GameManager.CurrentCharCount -= GivenNumber;
            }
        }

        public void Divide(
            int GivenNumber,
            List<GameObject> Characters,
            List<GameObject> DestroyEffects
        )
        {
            if (GameManager.CurrentCharCount <= GivenNumber)
            {
                foreach (var item in Characters)
                {
                    foreach (var item2 in DestroyEffects)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 newPos = new Vector3(
                                item.transform.position.x,
                                .23f,
                                item.transform.position.z
                            );
                            item2.SetActive(true);
                            item2.transform.position = newPos;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }

                GameManager.CurrentCharCount = 1;
            }
            else
            {
                int bolen = GameManager.CurrentCharCount / GivenNumber;

                int sayi3 = 0;
                foreach (var item in Characters)
                {
                    // 0 default
                    if (sayi3 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in DestroyEffects)
                            {
                                if (!item2.activeInHierarchy)
                                {
                                    Vector3 newPos = new Vector3(
                                        item.transform.position.x,
                                        .23f,
                                        item.transform.position.z
                                    );
                                    item2.SetActive(true);
                                    item2.transform.position = newPos;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;
                        }
                    }
                    else
                    {
                        sayi3 = 0;
                        break;
                    }
                }

                if (GameManager.CurrentCharCount % GivenNumber == 0)
                    GameManager.CurrentCharCount /= GivenNumber;
                else if (GameManager.CurrentCharCount % GivenNumber == 1)
                {
                    GameManager.CurrentCharCount /= GivenNumber;
                    GameManager.CurrentCharCount++;
                }
                else if (GameManager.CurrentCharCount % GivenNumber == 2)
                {
                    GameManager.CurrentCharCount /= GivenNumber;
                    GameManager.CurrentCharCount += 2;
                }
            }
        }
    }

    public class MemoryManagement
    {
        public void SaveData_String(string Key, string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }

        public void SaveData_Int(string Key, int value)
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }

        public void SaveData_Float(string Key, float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }

        public string ReadData_s(string Key)
        {
            return PlayerPrefs.GetString(Key);
        }

        public int ReadData_i(string Key)
        {
            return PlayerPrefs.GetInt(Key);
        }

        public float ReadData_f(string Key)
        {
            return PlayerPrefs.GetFloat(Key);
        }

        public void CheckAndDefine()
        {
            if (!PlayerPrefs.HasKey("LastPlayed"))
            {
                PlayerPrefs.SetInt("LastPlayed", 5);
                PlayerPrefs.SetInt("Score", 1000);
                PlayerPrefs.SetInt("ActiveHat", -1);
                PlayerPrefs.SetInt("ActiveBat", -1);
                PlayerPrefs.SetInt("ActiveMat", -1);
                PlayerPrefs.SetFloat("MenuMusic", .5f);
                PlayerPrefs.SetFloat("MenuFX", .5f);
                PlayerPrefs.SetFloat("GameMusic", .5f);
                PlayerPrefs.SetString("Language", "TR");
            }
        }
    }

    [Serializable]
    public class ItemData
    {
        public int GroupIndex;
        public int Item_Index;
        public string Item_Name;
        public int Score;
        public bool BuyStatus;
    }

    public class DataManagement
    {

        List<ItemData> _ItemInnerData;

        public void Save(List<ItemData> _ItemData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemData.gd");
            bf.Serialize(file, _ItemData);
            file.Close();
        }

        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(
                    Application.persistentDataPath + "/ItemData.gd",
                    FileMode.Open
                );
                _ItemInnerData = (List<ItemData>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<ItemData> ExportList()
        {
            return _ItemInnerData;
        }

        public void FirstBuildUp(List<ItemData> _ItemData, List<LanguageDataMain> _LangData)
        {
            if (!File.Exists(Application.persistentDataPath + "/ItemData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemData.gd");
                bf.Serialize(file, _ItemData);
                file.Close();
            }
            if (!File.Exists(Application.persistentDataPath + "/LangData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/LangData.gd");
                bf.Serialize(file, _LangData);
                file.Close();
            }
        }

        List<LanguageDataMain> _LangDataInnerList;


        public void LoadLang()
        {
            if (File.Exists(Application.persistentDataPath + "/LangData.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(
                    Application.persistentDataPath + "/LangData.gd",
                    FileMode.Open
                );
                _LangDataInnerList = (List<LanguageDataMain>)bf.Deserialize(file);
                file.Close();
            }
        }

        public List<LanguageDataMain> ExportLangList()
        {
            return _LangDataInnerList;
        }

    }



    //Language


    [Serializable]
    public class LanguageDataMain
    {

        public List<LanguageData> _LangData_TR = new List<LanguageData>();
        public List<LanguageData> _LangData_EN = new List<LanguageData>();

    }

    [Serializable]
    public class LanguageData
    {
        public string Text;
    }



}
