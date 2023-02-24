using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Anil;
using System;

public class MarketManager : MonoBehaviour, IStoreListener

{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    public AudioSource ButtonSound;

    private static string Puan_250 = "puan250";
    private static string Puan_500 = "puan500";
    private static string Puan_1000 = "puan1000";
    private static string Puan_2500 = "puan2500";

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

        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void GoBack()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(0);
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Puan_250, ProductType.Consumable);
        builder.AddProduct(Puan_500, ProductType.Consumable);
        builder.AddProduct(Puan_1000, ProductType.Consumable);
        builder.AddProduct(Puan_2500, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProduct_250()
    {
        BuyProductID(Puan_250);
    }
    public void BuyProduct_500()
    {
        BuyProductID(Puan_500);
    }
    public void BuyProduct_1000()
    {
        BuyProductID(Puan_1000);
    }
    public void BuyProduct_2500()
    {
        BuyProductID(Puan_2500);
    }

    private void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("FAIL. Not initialized.");
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_250, StringComparison.Ordinal))
        {
            _MemManage.SaveData_Int("Puan", _MemManage.ReadData_i("Puan") + 250);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_500, StringComparison.Ordinal))
        {
            _MemManage.SaveData_Int("Puan", _MemManage.ReadData_i("Puan") + 500);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_1000, StringComparison.Ordinal))
        {
            _MemManage.SaveData_Int("Puan", _MemManage.ReadData_i("Puan") + 1000);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Puan_2500, StringComparison.Ordinal))
        {
            _MemManage.SaveData_Int("Puan", _MemManage.ReadData_i("Puan") + 2500);
        }
        else
        {
            Debug.Log("FAIL. Unrecognized product.");
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
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
