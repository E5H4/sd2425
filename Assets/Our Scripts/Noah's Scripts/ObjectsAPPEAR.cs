using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsAPPEAR : MonoBehaviour
{

    [SerializeField] private GameObject cannedDrink;
    [SerializeField] private GameObject medicalTape;
    [SerializeField] private GameObject cellPhone;


    public void AllObjectsAppear(){
        if(cannedDrink != null) cannedDrink.SetActive(true);
        if(medicalTape != null) medicalTape.SetActive(true);
        else Debug.Log("SOMETHING IS WRONG AHHH");
        if(cellPhone != null) cellPhone.SetActive(true); 

    }
    
    public void AllObjectsppear(){
        if(cannedDrink != null) cannedDrink.SetActive(true);
        if(medicalTape != null) medicalTape.SetActive(true);
        else Debug.Log("SOMETHING IS WRONG AHHH");
        if(cellPhone != null) cellPhone.SetActive(true); 

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
