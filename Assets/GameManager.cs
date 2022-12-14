using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anil;
using System;

public class GameManager : MonoBehaviour
{
    public List<GameObject> Characters;
    public List<GameObject> SpawnEffects;
    public List<GameObject> DestroyEffects;
    public List<GameObject> DeathStains;
    public GameObject _Player;

    public static int CurrentCharCount;

    [Header("Level Data")]
    public List<GameObject> Enemies;
    public int enemyCount;
    public bool isGameEnded;
    bool isEndLine;

    MathOps _MathOps = new MathOps();
    MemoryManagement _MemoryManage = new MemoryManagement();

    void Start()
    {
        CreateEnemies();
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

                    Debug.Log("You Lose");
                else
                {
                    if ((CurrentCharCount > 5))
                    {
                        _MemoryManage.SaveData_Int(
                            "Score",
                            _MemoryManage.ReadData_i("Score") + 600
                        );
                        _MemoryManage.SaveData_Int(
                            "LastPlayed",
                            _MemoryManage.ReadData_i("LastPlayed" + 1)
                        );
                    }
                    else
                    {
                        _MemoryManage.SaveData_Int(
                            "Score",
                            _MemoryManage.ReadData_i("Score") + 200
                        );
                        _MemoryManage.SaveData_Int(
                            "LastPlayed",
                            _MemoryManage.ReadData_i("LastPlayed" + 1)
                        );
                        Debug.Log("You Win");
                    }
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
    }
}
