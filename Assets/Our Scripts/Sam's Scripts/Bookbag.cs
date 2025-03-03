using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystemWithText;

public class Bookbag : MonoBehaviour
{
    [SerializeField] private GameObject bag;
    [SerializeField] private DialogueUIController minordialogue2controller;
    [SerializeField] private GameObject MinorDialogue2;
    [SerializeField] private DialogueUIController minordialogue4controller;
    [SerializeField] private GameObject MinorDialogue4;
    [SerializeField] private DialogueUIController MeterDialoguecontroller;
    [SerializeField] private GameObject MeterDialogue;
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
    private static bool minorsecond = false;
    public static bool soda = false;

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
        MeterDialogue?.SetActive(false);
        MoneyDialogue?.SetActive(false);
        SodaDialogue?.SetActive(false);
        PhoneDialogue?.SetActive(false);
        KitDialogue?.SetActive(false);
        NotebookDialogue?.SetActive(false);
    }
    public void MinorSecondTrue()
    {
        minorsecond = true;
        Debug.Log("Minor second is" + minorsecond);
    }



    public void MonitorDialogue()
    {
        DeactivateAllDialogues();

        if (Difficulty.difficulty == "Minor")
        {
            if (minorsecond == false)
            {
                if (bag.activeInHierarchy)
                {
                    bag.SetActive(false);
                }

                if (MinorDialogue2 != null)
                {
                    MinorDialogue2.SetActive(true);
                    Debug.Log("Minor dialogue 2 activated.");
                    minordialogue2controller.ShowDialogueUI();
                }

            }
            else
            {
                if (MeterDialogue != null)
                {
                    MeterDialogue.SetActive(true);
                    Debug.Log("Meter dialogue activated.");
                }

                if (MeterDialoguecontroller != null)
                {
                    MeterDialoguecontroller.ShowDialogueUI();
                }

            }
        }
        else
        {
            if (MeterDialogue != null)
            {
                MeterDialogue.SetActive(true);
                Debug.Log("Meter dialogue activated.");
            }

            if (MeterDialoguecontroller != null)
            {
                MeterDialoguecontroller.ShowDialogueUI();
            }

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
        if (minorsecond)
        {
            soda = true;
            if (bag.activeInHierarchy)
            {
                bag.SetActive(false);
            }
            {
                MinorDialogue4.SetActive(true);
                Debug.Log("Minor dialogue 4 activated.");
            }

            if (minordialogue4controller != null)
            {
                minordialogue4controller.ShowDialogueUI();
            }
        }
        else {
            if (SodaDialogue != null)
            {
                SodaDialogue.SetActive(true);
            }

            if (SodaDialoguecontroller != null)
            {
                SodaDialoguecontroller.ShowDialogueUI();
            }
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