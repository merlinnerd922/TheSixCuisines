﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// A GameObject that has an image label over it.
/// </summary>
public class ImageLabelObject : MonoBehaviour
{

    /// <summary>
    /// The image currently hovering over the object.
    /// </summary>
    public Image iconImage;

    /// <summary>
    /// The camera that is displaying this object's image.
    /// </summary>
    [FormerlySerializedAs("mainCamera")] public Camera uiMainCamera;

    private void Update()
    {
        // Update this object's image to move with the object.
        Vector3 imagePosition = this.uiMainCamera.WorldToScreenPoint(this.transform.position + Vector3.up * 1);
        this.iconImage.transform.position = imagePosition;
    }

}