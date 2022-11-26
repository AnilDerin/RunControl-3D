using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anil
{
    public class MathOps : MonoBehaviour
    {
        public static void Multiply(
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
                        if (!item.activeInHierarchy)
                        {
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
            }
            GameManager.CurrentCharCount *= GivenNumber;
            Debug.Log("Gelen Sayı  : " + GivenNumber);
            Debug.Log("Anlık Karakter Sayısı : " + GameManager.CurrentCharCount);
        }

        public static void Add(
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
            Debug.Log("Gelen Sayı : " + GivenNumber);
            Debug.Log("Anlık Karakter Sayısı : " + GameManager.CurrentCharCount);
        }

        public static void Subtract(
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

        public static void Divide(
            int GivenNumber,
            List<GameObject> Characters,
            List<GameObject> DestroyEffects
        )
        {
            if (GameManager.CurrentCharCount <= GivenNumber)
            {
                foreach (var item in Characters)
                {
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
}
