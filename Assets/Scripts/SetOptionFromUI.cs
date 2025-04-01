using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SetOptionFromUI : MonoBehaviour
{
    public Scrollbar volumeSlider;
    public TMPro.TMP_Dropdown turnDropdown;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);

        if (PlayerPrefs.HasKey("turn"))
            turnDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("turn"));
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
    }

}
