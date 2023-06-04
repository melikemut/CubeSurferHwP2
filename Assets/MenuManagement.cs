using HmsPlugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HuaweiMobileServices.IAP;
using HuaweiMobileServices.Utils;

public class MenuManagement : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject storeCanvas;
    List<InAppPurchaseData> consumablePurchaseRecord = new List<InAppPurchaseData>();
    List<InAppPurchaseData> activeNonConsumables = new List<InAppPurchaseData>();
    private void Start()
    {
        menuCanvas.SetActive(true);
        storeCanvas.SetActive(false);
        if (HMSAdsKitManager.Instance.IsBannerAdLoaded)
        {
            HMSAdsKitManager.Instance.ShowBannerAd();
        }
        RestoreProducts();
        HMSIAPManager.Instance.OnInitializeIAPSuccess += OnInitializeIAPSuccess;
        HMSIAPManager.Instance.OnInitializeIAPFailure += OnInitializeIAPFailure;
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeCanvases()
    {
        if (menuCanvas.active)
        {
            menuCanvas.SetActive(false);
            storeCanvas.SetActive(true);
        }
        else
        {
            menuCanvas.SetActive(true);
            storeCanvas.SetActive(false);
        }
    }

    public void BuyCoins()
    {
        HMSIAPManager.Instance.OnBuyProductSuccess += OnBuyProductSuccess;
        HMSIAPManager.Instance.PurchaseProduct(HMSIAPConstants.coins10000);
    }
    public void RemoveAds()
    {
        HMSIAPManager.Instance.OnInitializeIAPSuccess += OnInitializeIAPSuccess;
        HMSIAPManager.Instance.OnInitializeIAPFailure += OnInitializeIAPFailure;
        HMSIAPManager.Instance.OnBuyProductSuccess = OnBuyProductSuccess;
        HMSIAPManager.Instance.PurchaseProduct(HMSIAPConstants.removeAds);
    }

    private void OnBuyProductSuccess(PurchaseResultInfo obj)
    {
        if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.coins10000)
        {
            FindObjectOfType<ScoreManager>().SetScore(10000);
        }
        if (obj.InAppPurchaseData.ProductId == HMSIAPConstants.removeAds)
        {
            HMSAdsKitManager.Instance.DestroyBannerAd();
            Destroy(GameObject.Find("native_large_image"));
        }
    }
    private void OnInitializeIAPFailure(HMSException ex)
    {
        Debug.Log("IAP FAILURE!");
        HMSIAPManager.Instance.InitializeIAP();
    }
    private void OnInitializeIAPSuccess()
    {
        Debug.Log("IAP IS READY!");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void RestoreProducts()
    {

        HMSIAPManager.Instance.RestorePurchaseRecords((restoredProducts) =>
        {
            foreach (var item in restoredProducts.InAppPurchaseDataList)
            {
                if ((IAPProductType)item.Kind == IAPProductType.Consumable)
                {
                    Debug.Log($"Consumable: ProductId {item.ProductId} , SubValid {item.SubValid} , PurchaseToken {item.PurchaseToken} , OrderID  {item.OrderID}");
                    consumablePurchaseRecord.Add(item);
                }
            }
        });

        HMSIAPManager.Instance.RestoreOwnedPurchases((restoredProducts) =>
        {
            foreach (var item in restoredProducts.InAppPurchaseDataList)
            {
                    Debug.Log($"NonConsumable: ProductId {item.ProductId} , DaysLasted {item.DaysLasted} , SubValid {item.SubValid} , PurchaseToken {item.PurchaseToken} ,OrderID {item.OrderID}");
                    activeNonConsumables.Add(item);
                }
            
        });
    }
}
