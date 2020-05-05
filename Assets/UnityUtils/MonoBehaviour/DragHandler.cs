﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BDT.Recipes
{

    /// <summary>
    /// A handler for an item being dragged across the screen.
    /// </summary>
    public class DragHandler : MonoBehaviour
    {
        /// <summary>
        /// The uiCanvas containing the currently dragged object.
        /// </summary>
        public Canvas dragCanvas;

        /// <summary>
        /// The current GameObject being dragged.
        /// </summary>
        private static GameObject gameObjectBeingDragged = null;

        /// <summary>
        /// The type of the GameObject being dragged.
        /// </summary>
        public static Type classBeingDragged { get; private set; }

        /// <summary>
        /// The DraggableImage script attached to the GameObject currently being dragged.
        /// </summary>
        private static DraggableImageMonoBehaviour mbBeingDragged;

        /// <summary>
        /// Set the GameObject currently being dragged to <paramref name="gameObject"/>, and store the type
        /// <paramref name="typeBeingDragged"/>, which is the type of the script invoking this method, in this handler.
        /// </summary>
        /// <param name="gameObject">The GameObject being stored in this handler.</param>
        /// <param name="typeBeingDragged">The type of the object being dragged.</param>
        private static void SetGameObjectBeingDragged(GameObject gameObject, Type typeBeingDragged)
        {
            gameObjectBeingDragged = gameObject;
            classBeingDragged = typeBeingDragged;
        }

        /// <summary>
        /// Set the GameObject being dragged to the one attached to the MonoBehaviour <paramref name="mb"/>.
        /// </summary>
        /// <param name="mb">The MonoBehaviour (hence mb) being dragged</param>
        public static void SetMBBeingDragged(DraggableImageMonoBehaviour mb)
        {
            mbBeingDragged = mb;

            // For non-null MonoBehaviours, also keep references to the MonoBehaviour's type and attached GameObject.
            if (mb != null)
            {
                SetGameObjectBeingDragged(mb.gameObject, mb.GetType());
            }
        }

        /// <summary>
        /// Return the MonoBehaviour attached to the GameObject currently being dragged.
        /// </summary>
        /// <returns>the MonoBehaviour attached to the GameObject currently being dragged.</returns>
        public static DraggableImageMonoBehaviour GetMBBeingDragged()
        {
            return mbBeingDragged;
        }

        /// <summary>
        /// Return the GameObject currently being dragged.
        /// </summary>
        /// <returns>The GameObject currently being dragged.</returns>
        public static GameObject GetGameObjectBeingDragged()
        {
            return gameObjectBeingDragged;
        }

    }

}