﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A sample detector for clicks on 3D objects.
/// </summary>
public class SampleClickDetector : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{

    /// <summary>
    /// Run any code that needs to run when this object is clicked on.
    ///
    /// Note that this object must have a camera with a physics raycaster in order for this object to be detected.
    /// </summary>
    /// <param name="eventData">Context info required to process the click.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("HI");
    }

    /// <summary>
    /// Run any code that needs to run when this object has a pointer clicked downwards on it.
    /// </summary>
    /// <param name="eventData">Context info required to process the click downwards.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
    }

    /// <summary>
    /// Run any code that needs to run when this object has a pointer released from it.
    /// </summary>
    /// <param name="eventData">Context info required to process the click upwards.</param>
    public void OnPointerUp(PointerEventData eventData)
    {
    }

}