using HmsPlugin;
using HuaweiMobileServices.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    Collector _collector;
    // Start is called before the first frame update
    void Start()
    {
        InitializeAdsKit();
        _collector = FindObjectOfType<Collector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeAdsKit()
    {
        HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        HMSAdsKitManager.Instance.OnInterstitialAdClosed = OnInterstitialAdClosed;

        HMSAdsKitManager.Instance.ConsentOnFail = OnConsentFail;
        HMSAdsKitManager.Instance.ConsentOnSuccess = OnConsentSuccess;
        HMSAdsKitManager.Instance.RequestConsentUpdate();

        SetNonPersonalizedAd();
    }

    private void SetNonPersonalizedAd()
    {
        var builder = HwAds.RequestOptions.ToBuilder();
        builder.SetConsent("tcfString");
        builder.SetNonPersonalizedAd((int)NonPersonalizedAd.ALLOW_ALL);
        bool requestLocation = true;
        builder.SetRequestLocation(requestLocation);
        var requestOptions = builder.Build();

        HMSAdsKitManager.Instance.SetRequestOptions(requestOptions);

    }
    private void OnConsentSuccess(ConsentStatus consentStatus, bool isNeedConsent, IList<AdProvider> adProviders)
    {
        foreach (var adProvider in adProviders)
        {

        }
    }

    private void OnConsentFail(string desc)
    {
    }

    public void ShowBannerAd()
    {
        HMSAdsKitManager.Instance.ShowBannerAd();
    }

    public void HideBannerAd()
    {
        HMSAdsKitManager.Instance.HideBannerAd();
    }

    public void ShowRewardedAd()
    {
        HMSAdsKitManager.Instance.ShowRewardedAd();
    }

    public void ShowInterstitialAd()
    {
        HMSAdsKitManager.Instance.ShowInterstitialAd();
    }

    public void ShowSplashImage()
    {
        HMSAdsKitManager.Instance.LoadSplashAd("testq6zq98hecj", SplashAd.SplashAdOrientation.PORTRAIT);
    }

    public void ShowSplashVideo()
    {
        HMSAdsKitManager.Instance.LoadSplashAd("testd7c5cewoj6", SplashAd.SplashAdOrientation.PORTRAIT);
    }


    private void OnRewarded(Reward reward)
    {
        _collector.OnRewarded();
    }

    private void OnInterstitialAdClosed()
    {
    }
}
