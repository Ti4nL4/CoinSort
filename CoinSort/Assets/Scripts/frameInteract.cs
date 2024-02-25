using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class frameInteract : MonoBehaviour
{
    public delegate void ChildEventHandler(int index);
    public event ChildEventHandler OnChildEvent;
    public int frameIdex;
    private void OnMouseDown()
    {
        if (OnChildEvent != null)
        {
            // Trigger the event
            OnChildEvent.Invoke(frameIdex);
        }
    }
}
