using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class Bookbag : MonoBehaviour
{
    [SerializeField] private GameObject bag;
    [SerializeField] private DialogueUIController minordialogue2controller;
    [SerializeField] private GameObject MinorDialogue2;
    [SerializeField] private DialogueUIController MoneyDialoguecontroller;
    [SerializeField] private GameObject MoneyDialogue;
    [SerializeField] private DialogueUIController SodaDialoguecontroller;
    [SerializeField] private GameObject SodaDialogue;
    [SerializeField] private DialogueUIController PhoneDialoguecontroller;
    [SerializeField] private GameObject PhoneDialogue;
    [SerializeField] private DialogueUIController KitDialoguecontroller;
    [SerializeField] private GameObject KitDialogue;
    [SerializeField] private DialogueUIController NotebookDialoguecontroller;
    [SerializeField] private GameObject NotebookDialogue;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && TestingText.bookbagButton == true)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Bookbag clicked");
                    if (bag != null)
                    {
                        bag.SetActive(true);
                    }
                }
            }
        }
    }

    private void DeactivateAllDialogues()
    {
        MinorDialogue2?.SetActive(false);
        MoneyDialogue?.SetActive(false);
        SodaDialogue?.SetActive(false);
        PhoneDialogue?.SetActive(false);
        KitDialogue?.SetActive(false);
        NotebookDialogue?.SetActive(false);
    }


    public void MonitorDialogue()
    {
        DeactivateAllDialogues();
        if (bag.activeInHierarchy)
        {
            bag.SetActive(false);
        }

        if (MinorDialogue2 != null)
        {
            MinorDialogue2.SetActive(true);
            Debug.Log("Minor dialogue 2 activated.");
        }

        if (minordialogue2controller != null)
        {
            minordialogue2controller.ShowDialogueUI();
        }
        else
        {
            Debug.LogError("minordialogue2controller is NULL! Check the Inspector.");
        }
    }

    public void MoneyBagsDialogue()
    {
        DeactivateAllDialogues();
        if (MoneyDialogue != null)
        {
            MoneyDialogue.SetActive(true);
        }

        if (MoneyDialoguecontroller != null)
        {
            MoneyDialoguecontroller.ShowDialogueUI();
        }
    }

    public void PopDialogue()
    {
        DeactivateAllDialogues();
        if (SodaDialogue != null)
        {
            SodaDialogue.SetActive(true);
        }

        if (SodaDialoguecontroller != null)
        {
            SodaDialoguecontroller.ShowDialogueUI();
        }
    }

    public void TelephoneDialogue()
    {
        DeactivateAllDialogues();
        if (PhoneDialogue != null)
        {
            PhoneDialogue.SetActive(true);
        }

        if (PhoneDialoguecontroller != null)
        {
            PhoneDialoguecontroller.ShowDialogueUI();
        }
    }

    public void GlucagonDialogue()
    {
        DeactivateAllDialogues();
        if (KitDialogue != null)
        {
            KitDialogue.SetActive(true);
        }

        if (KitDialoguecontroller != null)
        {
            KitDialoguecontroller.ShowDialogueUI();
        }
    }

    public void BookDialogue()
    {
        DeactivateAllDialogues();
        if (NotebookDialogue != null)
        {
            NotebookDialogue.SetActive(true);
        }

        if (NotebookDialoguecontroller != null)
        {
            NotebookDialoguecontroller.ShowDialogueUI();
        }
    }
}