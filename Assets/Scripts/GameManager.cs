using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Anil;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Characters;
    public List<GameObject> SpawnEffects;
    public List<GameObject> DestroyEffects;
    public List<GameObject> DeathStains;
    public GameObject _Player;

    public static int CurrentCharCount;

    [Header("--------LEVEL DATA")]
    public List<GameObject> Enemies;
    public int enemyCount;
    public bool isGameEnded;
    bool isEndLine;
    [Header("--------HATS")]
    public GameObject[] Hats;

    [Header("--------BATS")]
    public GameObject[] Bats;

    [Header("--------MATS")]
    public Material[] Mats;
    public SkinnedMeshRenderer _Renderer;
    public Material DefaultMat;

    MathOps _MathOps = new MathOps();
    MemoryManagement _MemoryManage = new MemoryManagement();
    DataManagement _ItemData = new DataManagement();

    Scene _Scene;

    [Header("--------GENERAL DATA")]
    public AudioSource[] Sounds;
    public GameObject[] opPanels;
    public Slider BGM_Volume;



    public List<LanguageDataMain> _LangDataMain = new List<LanguageDataMain>();
    List<LanguageDataMain> _LangReadData = new List<LanguageDataMain>();

    public TextMeshProUGUI[] TextObjects;


    private void Awake()
    {
        Sounds[0].volume = _MemoryManage.ReadData_f("MenuBGM");
        BGM_Volume.value = _MemoryManage.ReadData_f("MenuBGM");
        Sounds[1].volume = _MemoryManage.ReadData_f("MenuFx");
        Destroy(GameObject.FindWithTag("BGM"));
        CheckItems();
    }

    void Start()
    {
        _MemoryManage.SaveData_Int("LastPlayed", 5);
        CreateEnemies();
        _Scene = SceneManager.GetActiveScene();
        _ItemData.LoadLang();
        _LangReadData = _ItemData.ExportLangList();
        _LangDataMain.Add(_LangReadData[5]);
        LanguageDetect();
    }

    private void LanguageDetect()
    {
        if (_MemoryManage.ReadData_s("Language") == "TR")
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

    public void CreateEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Enemies[i].SetActive(true);
        }
    }

    void FightStatus()
    {
        if (isEndLine)
        {
            if (CurrentCharCount == 1 || enemyCount == 0)
            {
                isGameEnded = true;
                foreach (var item in Enemies)
                {
                    if (item.activeInHierarchy)
                        item.GetComponent<Animator>().SetBool("AttackEnemy", false);
                }
                foreach (var item in Characters)
                {
                    if (item.activeInHierarchy)

                        item.GetComponent<Animator>().SetBool("Attack", false);
                }

                _Player.GetComponent<Animator>().SetBool("Attack", false);

                if (CurrentCharCount < enemyCount || CurrentCharCount == enemyCount)

                    opPanels[3].SetActive(true);
                else
                {
                    if (CurrentCharCount > 5)
                    {
                        if (_Scene.buildIndex == _MemoryManage.ReadData_i("LastPlayed"))
                        {
                            _MemoryManage.SaveData_Int("Score", _MemoryManage.ReadData_i("Score") + 600);
                            _MemoryManage.SaveData_Int("LastPlayed", _MemoryManage.ReadData_i("LastPlayed") + 1);
                        }

                    }
                    else
                    {
                        if (_Scene.buildIndex == _MemoryManage.ReadData_i("LastPlayed"))
                        {
                            _MemoryManage.SaveData_Int("Score", _MemoryManage.ReadData_i("Score") + 200);
                            _MemoryManage.SaveData_Int("LastPlayed", _MemoryManage.ReadData_i("LastPlayed") + 1);

                        }
                    }
                    opPanels[2].SetActive(true);
                }
            }
        }
    }

    public void MathLogics(string islemTuru, int GelenSayi, Transform Posizyon)
    {
        switch (islemTuru)
        {
            case "Carpma":
                _MathOps.Multiply(GelenSayi, Characters, Posizyon, SpawnEffects);
                break;

            case "Toplama":
                _MathOps.Add(GelenSayi, Characters, Posizyon, SpawnEffects);
                break;

            case "Cikarma":
                _MathOps.Subtract(GelenSayi, Characters, DestroyEffects);
                break;

            case "Bolme":
                _MathOps.Divide(GelenSayi, Characters, DestroyEffects);
                break;
        }
    }

    public void CreateDestroyEffect(Vector3 Posizyon, bool Balyoz = false, bool FriendOrFoe = false)
    {
        foreach (var item in DestroyEffects)
        {
            if (!item.activeInHierarchy)
            {
                item.transform.position = Posizyon;
                item.SetActive(true);
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!FriendOrFoe)
                    CurrentCharCount--;
                else
                    enemyCount--;
                break;
            }
        }

        if (Balyoz)
        {
            Vector3 newPos = new Vector3(Posizyon.x, .005f, Posizyon.z);

            foreach (var item in DeathStains)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = newPos;
                    break;
                }
            }
        }

        if (!isGameEnded)
            FightStatus();
    }

    public void TriggerEnemies()
    {
        foreach (var item in Enemies)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<Enemy>().TriggerAnimation();
                item.GetComponent<Animator>().SetBool("AttackEnemy", false);
                item.GetComponent<Enemy>().isEndedEnemy = true;
                item.GetComponent<Enemy>().isAttacking = true;
            }
        }
        isEndLine = true;
        FightStatus();
        Debug.Log(CurrentCharCount);
    }

    public void CheckItems()
    {

        if (_MemoryManage.ReadData_i("ActiveHat") != -1)
            Hats[_MemoryManage.ReadData_i("ActiveHat")].SetActive(true);

        if (_MemoryManage.ReadData_i("ActiveBat") != -1)
            Bats[_MemoryManage.ReadData_i("ActiveBat")].SetActive(true);

        if (_MemoryManage.ReadData_i("ActiveMat") != -1)
        {
            Material[] matties = _Renderer.materials;
            matties[0] = Mats[_MemoryManage.ReadData_i("ActiveMat")];
            _Renderer.materials = matties;
        }
        else
        {
            Material[] matties = _Renderer.materials;
            matties[0] = DefaultMat;
            _Renderer.materials = matties;
        }
    }

    public void QuitButtonBehavior(string behavior)
    {
        Sounds[1].Play();
        Time.timeScale = 0;

        switch (behavior)
        {
            case "Pause":
                opPanels[0].SetActive(true);
                Sounds[0].volume = 0;
                break;
            case "Resume":
                Time.timeScale = 1;
                opPanels[0].SetActive(false);
                Sounds[0].volume = _MemoryManage.ReadData_f("MenuBGM");
                break;
            case "MainMenu":
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
                break;
            case "Restart":
                SceneManager.LoadScene(_Scene.buildIndex);
                Time.timeScale = 1;
                Sounds[0].volume = _MemoryManage.ReadData_f("MenuBGM");
                break;
        }

    }

    public void Settings(string behavior)
    {
        if (behavior == "Open")
        {
            opPanels[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            opPanels[1].SetActive(false);
            Time.timeScale = 1;

        }
    }


    public void AdjustVolume()
    {
        _MemoryManage.SaveData_Float("MenuBGM", BGM_Volume.value);
        Sounds[0].volume = BGM_Volume.value;
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene(_Scene.buildIndex + 1);
    }

}
