using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class Advert : MonoBehaviour {

    [SerializeField]
    private string banner;
    [SerializeField]
    private string fullScreen;

    [SerializeField]
    private bool banner_enabled;
    [SerializeField]
    private bool fullScreen_enabled;

    private static bool banner_loaded = false;
    private static bool fullScreen_running = false;

    private InterstitialAd interstitial;
    private BannerView bannerView;

    // Use this for initialization
    void Start () {
        if(banner_enabled)
            RequestBanner();

        if (fullScreen_enabled)
            RequestInterstitial();
    }

    private void RequestBanner()
    {
        if (banner_loaded) return;
        banner_loaded = true;

        #if UNITY_EDITOR
            string adUnitId = "unused";
        #elif UNITY_ANDROID
            string adUnitId = banner;
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the bottom of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request); 
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = fullScreen;
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);

        interstitial.OnAdLoaded += FullScreenLoaded;
        interstitial.OnAdFailedToLoad += FullScreenDestroy;
        interstitial.OnAdClosed += FullScreenDestroy;
        interstitial.OnAdLeavingApplication += FullScreenDestroy;
    }

    private void FullScreenLoaded(object sender, System.EventArgs args)
    {
        if (fullScreen_running) return;
        fullScreen_running = true;
        interstitial.Show();
    }

    private void FullScreenDestroy(object sender, System.EventArgs args)
    {
        interstitial.Destroy();
        fullScreen_running = false;
    }
}