using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advert : MonoBehaviour {
    private RevMobBanner banner;

    private static readonly Dictionary<String, String> REVMOB_APP_IDS = new Dictionary<String, String>() {
        { "Android", "575812f69f4c7d4601b2bb4f"},
    };
    private RevMob revmob;
    void Awake()
    {
        revmob = RevMob.Start(REVMOB_APP_IDS, "Canvas");
    }

    #region IRevMobListener implementation
    public void SessionIsStarted()
    {
        #if UNITY_ANDROID
                banner = revmob.CreateBanner(RevMob.Position.BOTTOM);
        #endif
        #if UNITY_IPHONE
                    banner = revmob.CreateBanner();
                    banner.Show();
        #endif
    }

    public void hideBanner()
    {
        banner.Hide();
        banner.Release();
        banner = null;
    }
    #endregion
}
