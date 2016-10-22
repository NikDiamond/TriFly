using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class Advert : MonoBehaviour {

    [SerializeField]
    private string _banner;
    private static string banner;

    [SerializeField]
    private string _fullScreen;
    private static string fullScreen;

    [SerializeField]
    private string _video;
    private static string video;

    [SerializeField]
    private int _fullScreenFrequency;
    public static int fullScreenFrequency;

    [SerializeField]
    private bool _bannerEnabled;
    private static bool banner_enabled;

    [SerializeField]
    private bool _fullScreenEnabled;
    private static bool fullScreen_enabled;

    private static bool banner_loaded = false;
    private static bool fullScreen_running = false;

    private static InterstitialAd interstitial;
    private static InterstitialAd videoAd;
    private static BannerView bannerView;

    // Use this for initialization
    void Start () {
        banner = _banner;
        fullScreen = _fullScreen;
        video = _video;
        banner_enabled = _bannerEnabled;
        fullScreen_enabled = _fullScreenEnabled;
        fullScreenFrequency = _fullScreenFrequency;

        if (banner_enabled)
            RequestBanner();

        if (fullScreen_enabled)
            RequestInterstitial();

        RequestVideo();
        
    }

    private static void RequestBanner()
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

    private static void RequestInterstitial()
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

        //interstitial.OnAdLoaded += OpenInterstitial;
        interstitial.OnAdFailedToLoad += _CloseInterstitial;
        interstitial.OnAdClosed += _CloseInterstitial;
        interstitial.OnAdLeavingApplication += _CloseInterstitial;
    }

    private static void RequestVideo()
    {
        #if UNITY_ANDROID
            string adUnitId = video;
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        videoAd = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        videoAd.LoadAd(request);

        //interstitial.OnAdLoaded += OpenInterstitial;
        videoAd.OnAdFailedToLoad += _CloseVideo;
        videoAd.OnAdClosed += _CloseVideo;
        videoAd.OnAdLeavingApplication += _CloseVideo;
    }

    //PUBLIC METHODS
    public static void OpenBanner()
    {
        if (bannerView == null) return;
        bannerView.Show();
        Debug.Log("[Ads] Banner opened");
    }

    public static void CloseBanner()
    {
        if (bannerView == null) return;
        bannerView.Hide();
        Debug.Log("[Ads] Banner closed");
    }

    private static void InterstitialLoaded(object sender, System.EventArgs args)
    {
        OpenInterstitial();
    }

    public static void OpenInterstitial()
    {
        if (fullScreen_running) return;
        if (!interstitial.IsLoaded()) return;
        CloseBanner();

        fullScreen_running = true;
        interstitial.Show();
        Debug.Log("[Ads] Interstitial opened");
    }

    public static void OpenVideo()
    {
        if (!videoAd.IsLoaded()) return;
        CloseBanner();

        videoAd.Show();
    }

    private static void _CloseInterstitial(object sender, System.EventArgs args)
    {
        CloseInterstitial();
    }

    private static void _CloseVideo(object sender, System.EventArgs args)
    {
        CloseVideo();
    }

    public static void CloseInterstitial()
    {
        if(interstitial != null)
            interstitial.Destroy();
        fullScreen_running = false;

        RequestInterstitial();
        Debug.Log("[Ads] Interstitial closed");
    }

    public static void CloseVideo()
    {
        if (videoAd != null)
            videoAd.Destroy();

        RequestVideo();
    }
}