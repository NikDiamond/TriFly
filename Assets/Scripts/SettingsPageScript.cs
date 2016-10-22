using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsPageScript : MonoBehaviour {
    [SerializeField]
    Toggle soundToggle;

    void Start()
    {
        soundToggle.isOn = GameMaster.soundEnabled();
    }

    public void SaveSound(bool value)
    {
        GameMaster.setSoundEnabled(value);
    }
}
