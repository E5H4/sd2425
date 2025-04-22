using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayAEDStuff : MonoBehaviour
{
    [SerializeField] private GameObject dummyWithPads;
    [SerializeField] private GameObject padsInAir;
    [SerializeField] private GameObject wiresInAir;
    [SerializeField] private GameObject leftPadToDrag;
    [SerializeField] private GameObject rightPadToDrag;
    [SerializeField] private GameObject wiresToClick;
    [SerializeField] private GameObject connectedPadsWire;
    [SerializeField] private GameObject wiresToAED;

    public void ShowDummy()
    {
        if (dummyWithPads != null)
        {
            dummyWithPads.SetActive(true);
            Debug.Log("Dummy with pads is now visible.");
        }
    }

    public void HideDummy()
    {
        if (dummyWithPads != null)
        {
            dummyWithPads.SetActive(false);
            Debug.Log("Dummy with pads is now hidden.");
        }
        else
        {
            Debug.LogWarning("Dummy GameObject is not assigned!");
        }
    }

    public void HidePadsAndWiresInAir()
    {
        if (padsInAir != null)
        {
            padsInAir.SetActive(false);
            Debug.Log("Pads in air are now hidden.");
        }
        else
        {
            Debug.LogWarning("PadsInAir GameObject is not assigned!");
        }

        if (wiresInAir != null)
        {
            wiresInAir.SetActive(false);
            Debug.Log("Wires in air are now hidden.");
        }
        else
        {
            Debug.LogWarning("WiresInAir GameObject is not assigned!");
        }
    }

    public void ShowPadsAndWiresInAir()
    {
        if (padsInAir != null)
        {
            padsInAir.SetActive(true);
            Debug.Log("pads in air visible");
        }

        if (wiresInAir != null)
        {
            wiresInAir.SetActive(true);
            Debug.Log("wires in air visible");
        }
    }

    public void ShowDraggablePads()
    {
        if (leftPadToDrag != null)
        {
            leftPadToDrag.SetActive(true);
            Debug.Log("left pad visible");
        }

        if (rightPadToDrag != null)
        {
            rightPadToDrag.SetActive(true);
            Debug.Log("right pad visible");
        }
    }

    public void HideDraggablePads()
    {
        if (leftPadToDrag != null)
        {
            leftPadToDrag.SetActive(false);
            Debug.Log("Left pad to drag is now hidden.");
        }

        if (rightPadToDrag != null)
        {
            rightPadToDrag.SetActive(false);
            Debug.Log("Right pad to drag is now hidden.");
        }
    }

    public void ShowWiresToClick() {
        if (wiresToClick != null) {
            wiresToClick.SetActive(true);
            Debug.Log("clickable wires visible");
        }

    }

    public void HideWiresToClick(){
        if (wiresToClick != null) {
            wiresToClick.SetActive(false);
            Debug.Log("wires hidden");
        }
    }

    public void ShowConnectedSet() {
        if (connectedPadsWire != null) {
            connectedPadsWire.SetActive(true);
            Debug.Log("whole pads & wires appeared after pads and wire clicked");
        }
    }

    public void ShowWiresToAED(){
        if (wiresToAED != null) {
            wiresToAED.SetActive(true);
            Debug.Log("wires connected to aed.");
        }
    }


}