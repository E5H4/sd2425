using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class mashtimer : MonoBehaviour
{

    public TextMeshProUGUI percentOfPressure;
    int percent;

    public void ButtonPressed()
    {
       if (percent < 100)
        {
            percent += 2;

            // Clamp to 100 in case it goes over
            if (percent > 100)
                percent = 100;

            percentOfPressure.text = percent + "%";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
