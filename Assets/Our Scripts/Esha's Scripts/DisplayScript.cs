using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScript : MonoBehaviour
{
    // Displays and enables or disables symptom GameObjects and/or equipment

    [SerializeField] private GameObject ChestPain1;
    [SerializeField] private GameObject Fatigue2;
    [SerializeField] private GameObject ShortBreath3;
    [SerializeField] private GameObject Nausea4;
    [SerializeField] private GameObject ColdSweat5;
    [SerializeField] private GameObject Dizziness6;

    // minor scene objects ONLY below
    
    // major scene objects ONLY below


    // allow objects to display when prompted by dialogues

    public void chestPain() {
        if (ChestPain1 != null) {
            ChestPain1.SetActive(true);
        }
    }

    public void fatigue() {
        if (Fatigue2 != null) {
            Fatigue2.SetActive(true);
        }
    }

    public void shortBreath() {
        if (ShortBreath3 != null) {
            ShortBreath3.SetActive(true);
        }
    }
    
    public void nausea() {
        if (Nausea4 != null) {
            Nausea4.SetActive(true);
        }
    }

    public void coldSweat() {
        if (ColdSweat5 != null) {
            ColdSweat5.SetActive(true);
        }
    }

    public void dizziness() {
        if (Dizziness6 != null) {
            Dizziness6.SetActive(true);
        }
    }
}