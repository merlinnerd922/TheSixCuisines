﻿using System;
using Extend;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A custom handler for changing the cursor when it hovers over this object.
/// </summary>
public class CustomCursorHoverHandler : MonoBehaviour, IHoverHandler
{

    /// <summary>
    /// TODO_LATER Change this so that it's stored elsewhere.
    /// </summary>
    public Texture2D cursor;

    /// <summary>
    /// A flag indicating if this image is currently being hovered over.
    /// </summary>
    protected bool isHovered = false;

    /// <summary>
    /// Trigger any code that needs to run when the mouse pointer hovers over this object.
    /// </summary>
    /// <param name="eventData">Context info that needs to be provided for the pointer entering the zone.</param>
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        LocalUnityUtils.SetCursor(this.cursor);
    }

    /// <summary>
    /// Run any code that needs to run when the cursor is moved away from this object.
    /// </summary>
    /// <param name="eventData">Context info that needs to be provided for moving the cursor away.</param>
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        this.isHovered = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}  