﻿using BDT.Recipes;
using BDTInventory;
using Extend;
using Helper.ExtendSpace;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// An abstract class for an image that can be dragged and hovered over.
/// </summary>
public abstract class DraggableImageMonoBehaviour : CustomCursorHoverHandler, IDragHandlerThorough
{

    /// <summary>
    /// The original position of this recipe row object, before it possibly starts moving and being dragged.
    /// </summary>
    private Vector3 startPosition;

    /// <summary>
    /// The world camera. A reference to this is necessary in order to position the dragged object correctly.
    /// </summary>
    private Camera worldCamera;

    /// <summary>
    /// The RectTransform that will be dragged across the screen.
    /// </summary>
    private RectTransform _rectTransform;


    /// <summary>
    /// The drag handler that is handling the dragging this MonoBehaviour.
    /// </summary>
    private DragHandler dragHandler;

    /// <summary>
    /// In case this GameObject is being dragged, this is the original value of the parent this MonoBehaviour was
    /// parented to.
    /// </summary>
    internal GameObject originalParent;


    /// <summary>
    /// Run any code that runs when the player starts dragging this image.
    /// </summary>
    /// <param name="eventData">Any additionally required context variables.</param>
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        // Unless the drag condition hasn't been fulfilled, start dragging this object.
        if (!this.DraggableConditionFulfilled())
        {
            return;
        }

        // Keep track of the original position and parent of this image.
        this.startPosition = this.transform.position;
        originalParent = this.GetParent();

        // Use the drag handler to track this object, and parent this MonoBehaviour to its uiCanvas.
        DragHandler.SetMBBeingDragged(this);
        this.SetParent(this.dragHandler.dragCanvas.gameObject);
    }

    /// <summary>
    /// Return true iff the conditions required to drag this object have been met.
    /// </summary>
    /// <returns>true iff the conditions required to drag this object have been met.</returns>
    protected abstract bool DraggableConditionFulfilled();

    /// <summary>
    /// Start this script.
    /// </summary>
    public void Start()
    {
        // Store a reference to various components to facilitate dragging of this image (camera, drag handler, transform).
        this.worldCamera = GameObject.Find($"{InventorySceneManager.INVENTORY_SCENE_PREFAB_NAME}/Main Camera")
            .GetComponent<Camera>();
        this.dragHandler = GameObject.Find($"{InventorySceneManager.INVENTORY_SCENE_PREFAB_NAME}/DragHandler")
            .GetComponent<DragHandler>();
        this._rectTransform = this.GetComponent<RectTransform>();
    }

    /// <summary>
    /// Run any code that needs to be run while this image is being dragged.
    /// </summary>
    /// <param name="eventData">Any additionally required context info.</param>
    public void OnDrag(PointerEventData eventData)
    {
        if (!this.DraggableConditionFulfilled())
        {
            return;
        }

        // Convert the point that we're currently dragging over (on the screen) into a pair of coordinates (localPosition) on the 
        // RectTransform of the uiCanvas we're dragging this image over.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this._rectTransform, Input.mousePosition,
            this.worldCamera, out Vector2 localPosition);

        // Then, convert the position in the rectangle to a triple of 3D coordinates.
        Vector3 transformPoint = this.GetComponent<RectTransform>().TransformPoint(localPosition);
        this._rectTransform.position = transformPoint;
    }

    /// <summary>
    /// Run any code that needs to run upon the user no longer dragging this image.
    /// </summary>
    /// <param name="eventData">Any additionally required context info.</param>
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        this.SetParent(this.originalParent);

        // Set the image back to its original point.
        this.gameObject.SetGlobalPosition(this.startPosition);

        // Unset the drag handler so that it's no longer tracking this draggable image.
        DragHandler.SetMBBeingDragged(null);
    }

    /// <summary>
    /// Update the current frame for this MonoBehaviour.
    /// </summary>
    public void Update()
    {
        // Change the cursor to the preset cursor if it can be dragged and it's currently being hovered over.
        if (this.DraggableConditionFulfilled())
        {
        }
    }

    /// <summary>
    /// Run any code that needs to be run when the mouse pointer exits this object.
    /// </summary>
    /// <param name="eventData">Context information stored with the pointer and any relevant objects.</param>
    public override void OnPointerExit(PointerEventData eventData)
    {
        // Unless the drag condition hasn't been met, unset the cursor from the custom cursor.
        if (this.DraggableConditionFulfilled())
        {
            base.OnPointerExit(eventData);
        }
    }

    /// <summary>
    /// Run any code that needs to be run when the mouse pointer enters this object.
    /// </summary>
    /// <param name="eventData">Context information stored with the pointer and any relevant objects.</param>
    public override void OnPointerEnter(PointerEventData eventData)
    {
        // Call the base method to activate OnHover code to change the cursor, if this image is in fact draggable.
        if (this.DraggableConditionFulfilled())
        {
            base.OnPointerEnter(eventData);
        }
    }

}