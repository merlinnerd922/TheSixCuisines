﻿#region

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Helper;
using Helper.ExtendSpace;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;
using Object = UnityEngine.Object;

#endregion

namespace Extend
{

    /// <summary>A class containing extension methods for Unity-exclusive classes.</summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class LocalUnityUtils
    {
        /// <summary>
        /// Return the global position of the provided MonoBehaviour <paramref name="mb"/>.
        /// </summary>
        /// <param name="mb">The MonoBehaviour whose global position should be returned.</param>
        /// <returns>the global position of the provided MonoBehaviour <paramref name="mb"/>.</returns>
        public static Vector3 GetGlobalPosition(this MonoBehaviour mb)
        {
            return mb.transform.position;
        }

        /// <summary>
        ///     Given a 2D texture <paramref name="textureToFill" />, set the color of all of its pixels to the colour
        ///     <paramref name="fillColour" />.
        /// </summary>
        /// <param name="textureToFill">The texture To fill in with <paramref name="fillColour" />.</param>
        /// <param name="fillColour">The fill Colour.</param>
        public static void Fill(this Texture2D textureToFill, Color fillColour)
        {
            int i;
            int j;
            for (i = 0; i < textureToFill.width; i++)
            for (j = 0; j < textureToFill.height; j++)
                textureToFill.SetPixel(i, j, fillColour);
            textureToFill.Apply();
        }

        /// <summary>
        ///     Given a piece of text <paramref name="textToFlash" />, flash it back and forth between colours
        ///     <paramref name="c1" /> and <paramref name="c2" /> with speed <paramref name="speed" />.
        /// </summary>
        /// <param name="textToFlash">The piece of text to flash.</param>
        /// <param name="c1">The first colour to flash the text's colour between.</param>
        /// <param name="c2">The second colour to flash the text's colour between.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="addShift">The add Shift.</param>
        public static void Flash(this Text textToFlash, Color c1, Color c2, float speed, float addShift = 0f)
        {
            textToFlash.color =
                LocalColorUtils.LinearInterpolate(c1, c2, 0.5f * Mathf.Cos(Time.time * speed + addShift) + 0.5f);
        }

        /// <summary>
        ///     Given a GUIStyle <paramref name="guiStyle" /> , flash all text that adopts this style back and forth between
        ///     colours <paramref name="c1" /> and <paramref name="c2" /> with speed <paramref name="speed" /> .
        /// </summary>
        /// <param name="guiStyle">The gui Style.</param>
        /// <param name="c1">The c 1.</param>
        /// <param name="c2">The c 2.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="addShift">The add Shift.</param>
        public static void Flash(this GUIStyle guiStyle, Color c1, Color c2, float speed, float addShift = 0f)
        {
            guiStyle.normal.textColor = LocalColorUtils.LinearInterpolate(
                c1, c2, 0.5f * Mathf.Cos(Time.time * speed + addShift) + 0.5f);
        }

        /// <summary>Return the global Y position of the transform <paramref name="transform" />.</summary>
        /// <param name="transform">The transform object whose global position we should get.</param>
        /// <returns>The global position of the given <paramref name="transform" />.</returns>
        public static float GetGlobalPositionY(this Transform transform)
        {
            return transform.position.y;
        }

        /// <summary>Return the height of the given <paramref name="rectangleTransform" />.</summary>
        /// <param name="rectangleTransform">The rectangle transform whose height we should get.</param>
        /// <returns>The height of the given <paramref name="rectangleTransform" />.</returns>
        public static float GetHeight(this RectTransform rectangleTransform)
        {
            return rectangleTransform.rect.height;
        }

        /// <summary>Given a MonoBehaviour object, return its parent GameObject.</summary>
        /// <param name="mb">The MonoBehaviour whose parent we should return.</param>
        /// <returns>The parent GameObject of the GameObject <paramref name="mb" /> is attached to.</returns>
        public static GameObject GetParent(this MonoBehaviour mb)
        {
            return mb.transform.parent.gameObject;
        }

        /// <summary>Return an arbitrary unit vector that is perpendicular to the given vector.</summary>
        /// <param name="v">The vector whose perpendicular unit vector we should return.</param>
        /// <param name="raiseExceptionIfZero">
        ///     A flag indicating whether we should raise an Exception if the given
        ///     <paramref name="v" /> is 0.
        /// </param>
        /// <returns>A perpendicular unit vector to <paramref name="v" />.</returns>
        public static Vector3 GetPerpendicularUnitVector(this Vector3 v, bool raiseExceptionIfZero = false)
        {
            // If the z component is nonzero, then a normalized version of the vector (1, 0, -v.x /
            // v.z) will suffice.
            if (Math.Abs(v.z) > Single.Epsilon) return new Vector3(1, 0, -v.x / v.z).normalized;

            // If z component is zero but not the y component, then a normalized version of the
            // vector (1, -v.x / v.y, 0) will suffice.
            if (Math.Abs(v.y) > Single.Epsilon) return new Vector3(1, -v.x / v.y, 0).normalized;

            // If x is the only non-zero component, then simply return (0, 1, 0) (i.e. Vector3.up).
            if (Math.Abs(v.x) > Single.Epsilon) return new Vector3(0, 1, 0);

            // Raise an exception if we have forbidden the vector passed in to be Vector3.zero.
            if (v == Vector3.zero && raiseExceptionIfZero)
                throw new UnityException("We were not expecting the zero vector to be passed in to " +
                                         "GetPerpendicularUnitVector()!");

            // Otherwise, any vector is a normal.
            return new Vector3(0, 1, 0);
        }

        /// <summary>Given a <paramref name="trans" />, return its size.</summary>
        /// <param name="trans">The RectTransform whose size we should return.</param>
        /// <returns>The size of the given <paramref name="trans" />.</returns>
        public static Vector2 GetSize(this RectTransform trans)
        {
            return trans.rect.size;
        }

        /// <summary>Given a rectangular transform object <paramref name="trans" />, return its width.</summary>
        /// <param name="trans">The RectTransform</param>
        /// <returns>The <see cref="float" />.</returns>
        public static float GetWidth(this RectTransform trans)
        {
            return trans.rect.width;
        }

        /// <summary>
        ///     Given a rectangle <paramref name="rectangle" /> and a float <paramref name="f" />, increment the rectangle's y
        ///     position by that value (f).
        /// </summary>
        /// <param name="rectangle">The Rect whose y-value we should increment.</param>
        /// <param name="f">The float value we should increment <paramref name="rectangle" />'s y value by.</param>
        public static void IncrementY(this Rect rectangle, float f)
        {
            rectangle.Set(rectangle.x, rectangle.y + f, rectangle.width, rectangle.height);
        }

        /// <summary>Return true iff the renderer <paramref name="renderer" /> is visible from camera <paramref name="camera" />.</summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="camera">The camera.</param>
        /// <returns>true iff the renderer <paramref name="renderer" /> is visible from camera <paramref name="camera" />.</returns>
        public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }

        /// <summary>
        ///     Set <paramref name="applianceUIRectTransform" />'s maximum and minimum anchor values to their maximum and
        ///     minimum values, respectively.
        /// </summary>
        /// <param name="applianceUIRectTransform">
        ///     The UI rectangular transform whose anchor values we need to expand out as much
        ///     as possible.
        /// </param>
        public static void MaximizeAnchorSpace(this RectTransform applianceUIRectTransform)
        {
            applianceUIRectTransform.anchorMax = Vector2.one;
            applianceUIRectTransform.anchorMin = Vector2.zero;
        }

        /// <summary>
        ///     Given a vector and an axis, modify the axis by the given amount. Given a texture <paramref name="texture" />,
        ///     save it to disk.
        /// </summary>
        /// <param name="texture">The texture that we should save.</param>
        /// <param name="fileName">The name we should save the file as.</param>
        public static void SaveTextureToFile(this Texture2D texture, string fileName)
        {
            byte[] bytes = texture.EncodeToPNG();
            FileStream file = File.Open(Application.dataPath + "/" + fileName, FileMode.Create);
            BinaryWriter binary = new BinaryWriter(file);
            binary.Write(bytes);
            binary.Dispose();
            file.Close();
        }

        /// <summary>Given a RectTransform <paramref name="rectTransform" />, set its margins along all sides to 0.</summary>
        /// <param name="rectTransform">The RectTransform whose margins we're setting.</param>
        public static void SetAllMarginsToZero(this RectTransform rectTransform)
        {
            foreach (RectangularAlignment alignmentDirection in LocalGeneralUtils.GetEnumValues<RectangularAlignment>())
                rectTransform.SetRectMargin(alignmentDirection, 0);
        }

        /// <summary>
        ///     Given a material <paramref name="material" />, set its alpha to the given value
        ///     <paramref
        ///         name="value" />
        ///     .
        /// </summary>
        /// <param name="material">The material whose alpha value we're setting.</param>
        /// <param name="value">The value we're setting the material's alpha value to.</param>
        public static void SetAlpha(this Material material, float value)
        {
            Color color = material.color;
            color.a = value;
            material.color = color;
        }

        /// <summary>
        ///     Given a 2D texture <paramref name="textureToFill" />, set its alpha (of all of its pixels) to the float value
        ///     f.
        /// </summary>
        /// <param name="textureToFill">The texture whose alpha we should set.</param>
        /// <param name="f">The value that we should set <paramref name="textureToFill" />'s alpha to.</param>
        public static void SetAlpha(this Texture2D textureToFill, float f)
        {
            int i, j;
            for (i = 0; i < textureToFill.width; i++)
            for (j = 0; j < textureToFill.height; j++)
            {
                Color c = textureToFill.GetPixel(i, j);
                c.a = f;
                textureToFill.SetPixel(i, j, c);
            }

            textureToFill.Apply();
        }

        /// <summary>Set the value of <paramref name="rectTransform" />'s anchor max x value to <paramref name="maxXValue" />.</summary>
        /// <param name="rectTransform">The RectTransform whose anchorMax value we're modifying.</param>
        /// <param name="maxXValue">The value we're setting the anchorMax x value to.</param>
        public static void SetAnchorMaxX(this RectTransform rectTransform, float maxXValue)
        {
            rectTransform.anchorMax = new Vector2(maxXValue, rectTransform.anchorMax.y);
        }


        /// <summary>
        /// Set the value of <paramref name="rectTransform" />'s anchor min y value to <paramref name="minYValue" />.</summary>
        /// <param name="rectTransform">The RectTransform whose anchorMin value we're modifying.</param>
        /// <param name="minYValue">The value we're setting the anchorMin Y value to.</param>
        public static void SetAnchorMinY(this RectTransform rectTransform, float minYValue)
        {
            rectTransform.anchorMin = new Vector2(rectTransform.anchorMin.x, minYValue);
        }

        /// <summary>
        /// Set the value of <paramref name="rectTransform" />'s anchor max y value to <paramref name="maxYValue" />.</summary>
        /// <param name="rectTransform">The RectTransform whose anchorMax value we're modifying.</param>
        /// <param name="maxYValue">The value we're setting the anchorMax y value to.</param>
        public static void SetAnchorMaxY(this RectTransform rectTransform, float maxYValue)
        {
            rectTransform.anchorMax = new Vector2(rectTransform.anchorMax.x, maxYValue);
        }

        /// <summary>Set the scale of the given <paramref name="trans" /> object to the default scale, (1, 1, 1).</summary>
        /// <param name="trans">The RectTransform whose scale we should set.</param>
        public static void SetDefaultScale(this RectTransform trans)
        {
            trans.localScale = new Vector3(1, 1, 1);
        }

        /// <summary>Set the GameObject that <paramref name="monoBehaviour" /> is attached to, to be active.</summary>
        /// <param name="monoBehaviour">The MonoBehaviour belonging to the GameObject whose activity state we should toggle.</param>
        /// <param name="activeFlag">The activity state we should set the MonoBehaviour's GameObject to</param>
        public static void SetGameObjectActive(this MonoBehaviour monoBehaviour, bool activeFlag)
        {
            monoBehaviour.gameObject.SetActive(activeFlag);
        }

        /// <summary>
        ///     Given a box collider <paramref name="boxCollider" /> and a value
        ///     <paramref
        ///         name="height" />
        ///     , set its height to (height).
        /// </summary>
        /// <param name="boxCollider">The box collider whose height we're changing.</param>
        /// <param name="height">The new height of the box collider.</param>
        public static void SetHeight(this BoxCollider boxCollider, float height)
        {
            // Store the size of the box collider, set the height of that vector, and then set the
            // box collider's size to that height.
            Vector3 originalSize = boxCollider.size;
            boxCollider.size = new Vector3(originalSize.x, height, originalSize.z);
        }

        /// <summary>The height we should set the given <paramref name="rectangularTransform" /> object to.</summary>
        /// <param name="rectangularTransform">The rectangular transform whose rectangular transform we should resize.</param>
        /// <param name="newSize">The new height of the RectTransform.</param>
        public static void SetHeight(this RectTransform rectangularTransform, float newSize)
        {
            SetSize(rectangularTransform, new Vector2(rectangularTransform.rect.size.x, newSize));
        }

        /// <summary>
        ///     Given a RectTransform <paramref name="rectTransform" />, set its bottom-left position to
        ///     <paramref name="newPosition" />.
        /// </summary>
        /// <param name="rectTransform">The transform whose position we should set.</param>
        /// <param name="newPosition">The new value we should set <paramref name="rectTransform" />' position to.</param>
        public static void SetLeftBottomPosition(this RectTransform rectTransform, Vector2 newPosition)
        {
            rectTransform.localPosition = new Vector3(newPosition.x + rectTransform.pivot.x * rectTransform.rect.width,
                newPosition.y + rectTransform.pivot.y * rectTransform.rect.height, rectTransform.localPosition.z);
        }

        /// <summary>
        ///     Given a RectTransform <paramref name="rectTransform" />, set its top-left position to
        ///     <paramref name="newPos" />.
        /// </summary>
        /// <param name="rectTransform">The RectTransform whose position we should set.</param>
        /// <param name="newPos">The new value to set the transform's position to.</param>
        public static void SetLeftTopPosition(this RectTransform rectTransform, Vector2 newPos)
        {
            rectTransform.localPosition = new Vector3(newPos.x + rectTransform.pivot.x * rectTransform.rect.width,
                newPos.y - (1f - rectTransform.pivot.y) * rectTransform.rect.height, rectTransform.localPosition.z);
        }

        /// <summary>Set the pivot and headers of the given RectTransform to the value <paramref name="vectorToSetTo" />.</summary>
        /// <param name="rectTransform">The RectTransform whose pivots and anchors we should change.</param>
        /// <param name="vectorToSetTo">The vector we should set <paramref name="rectTransform" />'s anchors to.</param>
        public static void SetPivotAndAnchors(this RectTransform rectTransform, Vector2 vectorToSetTo)
        {
            rectTransform.pivot = vectorToSetTo;
            rectTransform.anchorMin = vectorToSetTo;
            rectTransform.anchorMax = vectorToSetTo;
        }

        /// <summary>Set the X and Y values of the given <paramref name="rectTransform" /> to <paramref name="newPos" />.</summary>
        /// <param name="rectTransform">The RectTransform whose local position we need to change.</param>
        /// <param name="newPos">The vector containing the new position values for the RectTransform.</param>
        public static void SetPositionOfPivot(this RectTransform rectTransform, Vector2 newPos)
        {
            rectTransform.localPosition = new Vector3(newPos.x, newPos.y, rectTransform.localPosition.z);
        }

        /// <summary>Given a RectTransform <paramref name="rectTransform" />, set its PosX position to <paramref name="newX" />.</summary>
        /// <param name="rectTransform">The RectTransform whose positionToCheck-x value we're setting.</param>
        /// <param name="newX">The new value to set the X value of <paramref name="rectTransform" /> to.</param>
        public static void SetPosX(this RectTransform rectTransform, int newX)
        {
            rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);
        }

        /// <summary>
        ///     Given a RectTransform <paramref name="rectTransform" />, set its alignment along the margin
        ///     <paramref name="alignmentToSet" /> to the value of <paramref name="marginValue" />.
        /// </summary>
        /// <param name="rectTransform">The RectTransform whose margin value we're changing.</param>
        /// <param name="alignmentToSet">The direction of the alignment that we are adjusting.</param>
        /// <param name="marginValue">The value to set the margin along the direction <paramref name="alignmentToSet" /> to.</param>
        public static void SetRectMargin(this RectTransform rectTransform, RectangularAlignment alignmentToSet,
            float marginValue)
        {
            // Depending on the direction of the margin, set the margin along that direction accordingly.
            switch (alignmentToSet)
            {
                case RectangularAlignment.TOP:
                {
                    rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, marginValue);
                    break;
                }

                case RectangularAlignment.BOTTOM:
                {
                    rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, marginValue);
                    break;
                }

                case RectangularAlignment.LEFT:
                {
                    rectTransform.offsetMin = new Vector2(marginValue, rectTransform.offsetMin.y);
                    break;
                }

                case RectangularAlignment.RIGHT:
                {
                    rectTransform.offsetMax = new Vector2(marginValue, rectTransform.offsetMax.y);
                    break;
                }
            }
        }

        /// <summary>Set the given RectTransform's bottom-right position <paramref name="newPosition" />.</summary>
        /// <param name="rectTransform">The transform whose new position we should set.</param>
        /// <param name="newPosition">The new position we should set <paramref name="rectTransform" />'s position to.</param>
        public static void SetRightBottomPosition(this RectTransform rectTransform, Vector2 newPosition)
        {
            rectTransform.localPosition =
                new Vector3(newPosition.x - (1f - rectTransform.pivot.x) * rectTransform.rect.width,
                    newPosition.y + rectTransform.pivot.y * rectTransform.rect.height, rectTransform.localPosition.z);
        }

        /// <summary>Set the given RectTransform's top-right position to <paramref name="newPos" />.</summary>
        /// <param name="rectTransform">The RectTransform whose position we should set.</param>
        /// <param name="newPos">The new position to set the RectTransform's top-right position to.</param>
        public static void SetRightTopPosition(this RectTransform rectTransform, Vector2 newPos)
        {
            rectTransform.localPosition =
                new Vector3(newPos.x - (1f - rectTransform.pivot.x) * rectTransform.rect.width,
                    newPos.y - (1f - rectTransform.pivot.y) * rectTransform.rect.height, rectTransform.localPosition.z);
        }

        /// <summary>The set size.</summary>
        /// <param name="trans">The rectangularTransform.</param>
        /// <param name="newSize">The new size.</param>
        public static void SetSize(this RectTransform trans, Vector2 newSize)
        {
            Vector2 oldSize = trans.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            Vector2 pivot = trans.pivot;
            trans.offsetMin -= new Vector2(deltaSize.x * pivot.x, deltaSize.y * pivot.y);
            trans.offsetMax += new Vector2(deltaSize.x * (1f - pivot.x), deltaSize.y * (1f - pivot.y));
        }

        /// <summary>Set the width of the RectTransform <paramref name="rectTransform" /> to <paramref name="width" />.</summary>
        /// <param name="rectTransform">The RectTransform whose width we're setting.</param>
        /// <param name="width">The width that we're setting <paramref name="rectTransform" />'s width to.</param>
        public static void SetWidth(this RectTransform rectTransform, float width)
        {
            rectTransform.SetSize(new Vector2(width, rectTransform.rect.size.y));
        }

        /// <summary>
        ///     Given a rectangle object <paramref name="rectangle" />, set its X component to the value
        ///     <paramref name="newX" />. (To do this, call the Set() function, changing only the x component).
        /// </summary>
        /// <param name="rectangle">The rectangle whose X value we should change.</param>
        /// <param name="newX">The new value to set <paramref name="rectangle" />'s x-value to.</param>
        public static void SetX(this Rect rectangle, float newX)
        {
            rectangle.Set(newX, rectangle.y, rectangle.width, rectangle.height);
        }

        /// <summary>
        ///     Given a MonoBehaviour <paramref name="monoBehaviour" /> and a coroutine
        ///     <paramref
        ///         name="coroutineToBeStopped" />
        ///     that needs to be stopped, stop it if it is not null.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour from where we're trying to stop (coroutineToBeStopped)</param>
        /// <param name="coroutineToBeStopped">A possibly null reference to a coroutine, that we intend to stop.</param>
        public static void StopNullifyCoroutine(this MonoBehaviour monoBehaviour, ref Coroutine coroutineToBeStopped)
        {
            if (coroutineToBeStopped == null) return;

            monoBehaviour.StopCoroutine(coroutineToBeStopped);
            coroutineToBeStopped = null;
        }

        /// <summary>Return true iff any mouse button was just pressed down.</summary>
        /// <returns>true iff any mouse button was pressed down.</returns>
        public static bool GetAnyMouseButtonDown()
        {
            return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
        }

        /// <summary>
        ///     Given a string representing a uiCanvas object in the world interface, render it invisible and non-interactable
        ///     (if it exists).
        /// </summary>
        /// <param name="canvasToRenderInvisible">The name of the uiCanvas object to render invisible.</param>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        internal static void RenderCanvasElementInvisibleGone(string canvasToRenderInvisible)
        {
            // Find the object (if it exists).
            GameObject startGameObj = GameObject.Find(canvasToRenderInvisible);
            if (startGameObj == null) return;

            // Use its CanvasGroup object to render it invisible and uninteractable.
            CanvasGroup thisObjCanvasGroup = startGameObj.GetComponent<CanvasGroup>();
            if (thisObjCanvasGroup != null)
            {
                thisObjCanvasGroup.alpha = 0;
                thisObjCanvasGroup.interactable = false;
            }
        }

        /// <summary>
        ///     Generate a coloured box at position (colNum, y) with width w and height h and colour c. Unlike
        ///     RenderColoredRectangle, the interior of the box is transparent.
        /// </summary>
        /// <param name="w">The width of the coloured box to create.</param>
        /// <param name="h">The height of the coloured box to create.</param>
        /// <param name="c">The colour of the box to create.</param>
        /// <returns>The <see cref="Texture2D" /> to render.</returns>
        public static Texture2D RenderColoredBox(int w, int h, Color c)
        {
            Texture2D rgbTexture = new Texture2D(w, h);
            Color rgbColor = c;
            int i, j;
            Color alphaColour = new Color(0, 0, 0, 0);

            // Set every pixel on the border to the given colour, and every pixel on the interior to be transparent.
            for (i = 0; i < w; i++)
            {
                rgbTexture.SetPixel(i, 0, rgbColor);
                for (j = 1; j < h - 1; j++) rgbTexture.SetPixel(i, j, alphaColour);
                rgbTexture.SetPixel(i, h - 1, rgbColor);
            }

            for (j = 1; j < h - 1; j++)
            {
                rgbTexture.SetPixel(0, j, rgbColor);
                for (i = 1; i < w - 1; i++) rgbTexture.SetPixel(i, j, alphaColour);
                rgbTexture.SetPixel(w - 1, j, rgbColor);
            }

            // Apply all changes and return the box.
            rgbTexture.Apply();
            return rgbTexture;
        }

        /// <summary>
        ///     Generate a solid color 2D texture with the given <paramref name="width" /> and <paramref name="height" />,
        ///     coloured <paramref name="col" />.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="col">The colour of the texture to create.</param>
        /// <returns>The <see cref="Texture2D" /></returns>
        public static Texture2D RenderColoredRectangle(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i) pix[i] = col;
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        /// <summary>
        ///     Return the sibling GameObject named <paramref name="siblingName" /> of the GameObject attached to
        ///     <paramref name="monoBehaviour" />.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose sibling GameObject we're retrieving.</param>
        /// <param name="siblingName">
        ///     The name of the sibling to the GameObject that <paramref name="monoBehaviour" /> is attached
        ///     to.
        /// </param>
        /// <returns>
        ///     the sibling GameObject named <paramref name="siblingName" /> of the GameObject attached to
        ///     <paramref name="monoBehaviour" />.
        /// </returns>
        public static GameObject GetSiblingGameObject(this MonoBehaviour monoBehaviour, string siblingName)
        {
            return monoBehaviour.gameObject.GetSibling(siblingName);
        }

        /// <summary>Return the grandparent GameObject of the GameObject that <paramref name="monoBehaviour" /> is attached to.</summary>
        /// <param name="monoBehaviour">The MonoBehaviour script whose GameObject's grandparent we want to return.</param>
        /// <returns>the grandparent GameObject of the GameObject that <paramref name="monoBehaviour" /> is attached to.</returns>
        public static GameObject GetGrandParentGameObject(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.gameObject.GetGrandparent();
        }

        /// <summary>
        ///     Given a MonoBehaviour instance <paramref name="monoBehaviour" />, return the script component of type
        ///     <typeparamref name="T" /> attached to the grandparent GameObject of the GameObject <code>gameObject</code> that
        ///     this MonoBehaviour is attached to.
        ///     <param name="monoBehaviour">
        ///         The MonoBehaviour whose grandparent object's <see cref="Component" /> of type
        ///         <typeparamref name="T" /> we want to retrieve.
        ///     </param>
        ///     <typeparam name="T">
        ///         The type of <see cref="Component" /> to retrieve from the grandparent GameObject of the
        ///         GameObject the provided MonoBehaviour is attached to.
        ///     </typeparam>
        ///     <returns>
        ///         the script component of type <typeparamref name="T" /> attached to the grandparent GameObject of the
        ///         GameObject <code>gameObject</code> that this MonoBehaviour is
        ///     </returns>
        /// </summary>
        public static T GetGrandParentComponent<T>(this MonoBehaviour monoBehaviour) where T : Component
        {
            return monoBehaviour.gameObject.GetGrandparentComponent<T>();
        }

        /// <summary>Given a MonoBehaviour <paramref name="monoBehaviour" />, return that MonoBehaviour's parent GameObject's name.</summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose parent's name should be returned.</param>
        /// <returns><paramref name="monoBehaviour" />'s parent GameObject's name.</returns>
        public static string GetParentName(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.GetParent().name;
        }

        /// <summary>
        ///     Given a MonoBehaviour <paramref name="monoBehaviour" />, return its sibling named
        ///     <paramref
        ///         name="componentObjectName" />
        ///     's <see cref="Component" /> of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose sibling's component we wish to retrieve.</param>
        /// <param name="componentObjectName">The name of the sibling GameObject whose component should be returned.</param>
        /// <typeparam name="T">The type of the component to return.</typeparam>
        /// <returns>
        ///     <paramref name="monoBehaviour" />'s sibling named
        ///     <paramref
        ///         name="componentObjectName" />
        ///     's <see cref="Component" /> of type <typeparamref name="T" />.
        /// </returns>
        public static T GetComponentInSiblingGameObject<T>(this MonoBehaviour monoBehaviour, string componentObjectName)
        {
            return monoBehaviour.GetSiblingGameObject(componentObjectName).GetComponent<T>();
        }


        /// <summary>
        ///     Given a MonoBehaviour <paramref name="monoBehaviour" /> attached to a GameObject, return the component of type
        ///     <typeparamref name="T" /> attached to that GameObject.
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <returns>
        ///     The component of type <typeparamref name="T" /> attached to the GameObject that
        ///     <paramref name="monoBehaviour" /> is attached to.
        /// </returns>
        public static T GetGameObjectComponent<T>(this MonoBehaviour monoBehaviour) where T : MonoBehaviour
        {
            return monoBehaviour.gameObject.GetComponent<T>();
        }

        /// <summary>Click on the Button attached to the GameObject that <paramref name="monoBehaviour" /> is attached to.</summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose GameObject's button will be invoked.</param>
        public static void InvokeButton(this MonoBehaviour monoBehaviour)
        {
            Button incrementButton = monoBehaviour.GetGameObjectComponent<Button>();
            incrementButton.onClick.Invoke();
        }


        /// <summary>
        ///     Return the descendant belonging to the GameObject that the <paramref name="monoBehaviour" /> is attached to,
        ///     whose relative path is <paramref name="descendantPath" />.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose GameObject's descendant is being returned.</param>
        /// <param name="descendantPath">The path of the MonoBehaviour's GameObject's descendant to return.</param>
        /// <returns>
        ///     The descendant belonging to the GameObject that the <paramref name="monoBehaviour" /> is attached to, whose
        ///     relative path is <paramref name="descendantPath" />.
        /// </returns>
        public static GameObject GetDescendant(this MonoBehaviour monoBehaviour, string descendantPath)
        {
            return monoBehaviour.transform.GetDescendant(descendantPath);
        }

        /// <summary>
        ///     Return the descendant of <paramref name="transform" /> whose relative path is
        ///     <paramref name="descendantPath" />.
        /// </summary>
        /// <param name="descendantPath">The path of the descendant to return.</param>
        /// <returns>The descendant of <paramref name="transform" /> whose relative path is <paramref name="descendantPath" />.</returns>
        /// <param name="transform">The transform whose descendant is being returned.</param>
        public static GameObject GetDescendant(this Transform transform, string descendantPath)
        {
            return transform.Find(descendantPath).gameObject;
        }


        /// <summary>
        /// Return the root ancestor of the provided MonoBehaviour, or the root GameObject this script is attached to if it has no parent GameObject.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose root ancestor GameObject should be returned.</param>
        /// <returns>The root ancestor of the provided MonoBehaviour, or the root GameObject this script is attached to if it has no parent GameObject.</returns>
        public static GameObject GetRootAncestor(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.gameObject.GetRootAncestor();
        }

        /// <summary>
        /// Return the child of the GameObject that the MonoBehaviour <paramref name="monoBehaviour"/> is attached to named <paramref name="childPath"/>.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose GameObject's child should be returned.</param>
        /// <param name="childPath">The path of the child to return.</param>
        /// <returns>The child of the GameObject that the MonoBehaviour <paramref name="monoBehaviour"/> is attached to named <paramref name="childPath"/>.</returns>
        public static GameObject GetChild(this MonoBehaviour monoBehaviour, string childPath)
        {
            return monoBehaviour.gameObject.GetChild(childPath);
        }

        /// <summary>
        /// Given a MonoBehaviour script <paramref name="monoBehaviour"/>, return a component of type <typeparamref
        /// name="T"/> that is attached to the GameObject whose parent GameObject has the MonoBehaviour
        /// <paramref name="monoBehaviour"/> attached to it.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose GameObject's child's component should be returned.</param>
        /// <param name="childName">The name of the child whose <typeparamref name="T"/> component should be returned.
        /// </param>
        /// <typeparam name="T">The type of the component in the child object to return.</typeparam>
        /// <returns>The component of type <typeparamref
        /// name="T"/> that is attached to the GameObject whose parent GameObject has the MonoBehaviour
        /// <paramref name="monoBehaviour"/> attached to it.</returns>
        public static T GetComponentInChild<T>(this MonoBehaviour monoBehaviour, string childName) where T : Object
        {
            return monoBehaviour.GetChild(childName).GetComponent<T>();
        }

        /// <summary>
        /// Set the sprite of the Image <paramref name="image"/> to be the sprite at the path <paramref name="spritePath"/>.
        /// </summary>
        /// <param name="image">The image whose sprite should be set.</param>
        /// <param name="spritePath">The path of the sprite to set the image's sprite to.</param>
        public static void SetSprite(this Image image, string spritePath)
        {
            image.sprite = Resources.Load<Sprite>(spritePath);
        }

        /// <summary>
        /// Return the descendant of the GameObject that <paramref name="monoBehaviour"/> is attached to, whose relative path is <paramref name="descendantPath"/>.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose GameObject's descendant we want to return.</param>
        /// <param name="descendantPath">The path of the descendant to return.</param>
        /// <returns>The descendant of the GameObject that <paramref name="monoBehaviour"/> is attached to, whose relative path is <paramref name="descendantPath"/>.</returns>
        public static GameObject GetDescendantGameObject(this MonoBehaviour monoBehaviour, string descendantPath)
        {
            return monoBehaviour.gameObject.GetDescendant(descendantPath);
        }

        /// <summary>
        /// Return the script component of type <typeparamref name="T"/> belonging to the descendant of <paramref
        /// name="monoBehaviour"/> whose path is <paramref name="descendantPath"/>.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose descendant should be returned.</param>
        /// <param name="descendantPath">The path of the descendant to return.</param>
        /// <typeparam name="T">The type of component being returned.</typeparam>
        /// <returns>The script component of type <typeparamref name="T"/> belonging to the descendant of <paramref
        /// name="monoBehaviour"/> whose path is <paramref name="descendantPath"/>. </returns>
        public static T GetComponentInDescendant<T>(this MonoBehaviour monoBehaviour, string descendantPath)
            where T : MonoBehaviour
        {
            return monoBehaviour.GetDescendant(descendantPath).GetComponent<T>();
        }

        /// <summary>
        /// Set the minimum and maximum Y anchor for <paramref name="rectTransform"/> to <paramref name="newAnchorYValue"/>.
        /// </summary>
        /// <param name="rectTransform">The RectTransform whose RectTransform's anchor Y value should be set.</param>
        /// <param name="newAnchorYValue">The value to set the anchor Y value to.</param>
        public static void SetAnchorY(this RectTransform rectTransform, float newAnchorYValue)
        {
            rectTransform.SetAnchorMinY(newAnchorYValue);
            rectTransform.SetAnchorMaxY(newAnchorYValue);
        }

        /// <summary>
        /// Increment the minimum and maximum anchors of the provided <paramref name="rectTransform"/> by <paramref
        /// name="amountToIncrement"/>.
        /// </summary>
        /// <param name="rectTransform">The RectTransform whose anchor Y-values should be incremented.</param>
        /// <param name="amountToIncrement">The amount to increment those anchor Y-values by.</param>
        public static void IncrementAnchorY(this RectTransform rectTransform, float amountToIncrement)
        {
            rectTransform.SetAnchorY(rectTransform.anchorMin.y + amountToIncrement);
        }

        /// <summary>
        /// Decrement the minimum and maximum anchors of the provided <paramref name="rectTransform"/> by <paramref
        /// name="amountToDecrement"/>.
        /// </summary>
        /// <param name="rectTransform">The RectTransform whose anchor Y-values should be incremented.</param>
        /// <param name="amountToDecrement">The amount to decrement those anchor Y-values by.</param>
        public static void DecrementAnchorY(this RectTransform rectTransform, float amountToDecrement)
        {
            rectTransform.IncrementAnchorY(-amountToDecrement);
        }

        /// <summary>
        /// Given a MonoBehaviour script, add the child <paramref name="childToAdd"/> to that MonoBehaviour's GameObject.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose GameObject should have a child added.</param>
        /// <param name="childToAdd">The child to add to <paramref name="monoBehaviour"/>'s GameObject's child.</param>
        public static void AddChildToGO(this MonoBehaviour monoBehaviour, GameObject childToAdd)
        {
            monoBehaviour.gameObject.AddChild(childToAdd);
        }

        /// <summary>
        /// Change the cursor's texture to the provided texture <paramref name="cursorTexture"/>.
        /// </summary>
        /// <param name="cursorTexture">The texture to change the cursor to.</param>
        public static void SetCursor(Texture2D cursorTexture)
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        /// <summary>
        /// Set the parent of the provided <paramref name="monoBehaviour"/> to <paramref name="gameObject"/>.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose parent GameObject needs to be set.</param>
        /// <param name="gameObject">The GameObject to set <paramref name="monoBehaviour"/>'s parent to.</param>
        public static void SetParent(this MonoBehaviour monoBehaviour, GameObject gameObject)
        {
            monoBehaviour.transform.SetParent(gameObject.transform, true);
        }

        /// <summary>
        /// Set the global position of the MonoBehaviour <paramref name="monoBehaviourToSet"/> to match that of the 
        /// position of <paramref name="monoBehaviourToFollow"/>.
        /// </summary>
        /// <param name="monoBehaviourToSet">The MonoBehaviour whose global position should be set.</param>
        /// <param name="monoBehaviourToFollow">The MonoBehaviour whose global position should be matched.</param>
        public static void SetSameGlobalPositionAs(this MonoBehaviour monoBehaviourToSet, MonoBehaviour 
        monoBehaviourToFollow)
        {
            monoBehaviourToSet.gameObject.SetGlobalPosition(monoBehaviourToFollow.gameObject.GetGlobalPosition());
        }

        /// <summary>
        /// Set the global position of <paramref name="monoBehaviour"/> to <paramref name="positionToSet"/>.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose position should be set.</param>
        /// <param name="positionToSet">The value to set that MonoBehaviour's global position to.</param>
        public static void SetGlobalPosition(this MonoBehaviour monoBehaviour, Vector3 positionToSet)
        {
            monoBehaviour.transform.position = positionToSet;
        }

        /// <summary>
        /// Deactivate the GameObject attached to this MonoBehaviour.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose GameObject should be deactivated.</param>
        public static void Deactivate(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
        }

        /// <summary>
        /// Activate the GameObject attached to this script.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose GameObject should be activated.</param>
        public static void Activate(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(true);
        }

        /// <summary>
        /// Return this provided MonoBehaviour's <paramref name="nthAncestor"/>th ancestor.
        /// </summary>
        /// <param name="monoBehaviour">The MonoBehaviour whose ancestor should be returned,</param>
        /// <param name="nthAncestor">The nth ancestor of this MonoBehavior to retrieve.</param>
        public static GameObject GetNthAncestorGameObject(this MonoBehaviour monoBehaviour, int nthAncestor)
        {
            GameObject returnObject = monoBehaviour.gameObject;
            for (int i = 0; i < 4; i++)
            {
                returnObject = returnObject.GetParent();
            }
            return returnObject;
        }

    }

}