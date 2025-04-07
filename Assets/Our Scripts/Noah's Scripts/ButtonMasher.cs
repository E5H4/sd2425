using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMasher : MonoBehaviour
{
    public float mashDelay = .5f;
    public GameObject text;
    float mash;
    bool pressedKey;
    bool startMashing;

    void Start()
    {
        mash = mashDelay;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            startMashing = true;
        }

        if (startMashing)
        {
            text.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space) && !pressedKey)
            {
                pressedKey = true;
                mash = mashDelay; // Resets timer on space press
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                pressedKey = false;
            }

            mash -= Time.deltaTime; // Decrease mash timer over time

            if (mash < 0)
            {
                text.GetComponent<Text>().text = "You've Failed";
            }
        }
    }
}
