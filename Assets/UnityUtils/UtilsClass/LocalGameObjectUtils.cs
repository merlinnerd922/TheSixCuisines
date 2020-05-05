﻿#region

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Extend;
using UnityEngine;
using UnityEngine.UIElements;
using UnityUtils;
using Button = UnityEngine.UI.Button;
using UnityObject = UnityEngine.Object;

#endregion

namespace Helper
{

    namespace ExtendSpace
    {

        /// <summary>A static class containing extension methods for GameObjects.</summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public static class LocalGameObjectUtils
        {

            /// <summary>
            ///     Given a GameObject (gameObject), deactivate it if it is currently (locally) active. By locally active, we
            ///     allow for the possibility that one of its ancestors is inactive, while it is currently flagged as locally active,
            ///     which would result in it being flagged as locally inactive.
            /// </summary>
            /// <param name="go">The GameObject we should activate.</param>
            public static void SetActiveIfInactive(this GameObject go)
            {
                if (!go.activeSelf) go.SetActive(true);
            }

            /// <summary>Given a GameObject, create a cloned version of it.</summary>
            /// <param name="go">The GameObject to clone.</param>
            /// <returns>The cloned version of the GameObject.</returns>
            public static GameObject Clone(this GameObject go)
            {
                return UnityObject.Instantiate(go).RemoveCloneSubstrRecursive();
            }

            /// <summary>
            ///     Given a GameObject (gameObject), convert the given <paramref name="worldCoordinates" /> to local coordinates
            ///     relative to (gameObject).
            /// </summary>
            /// <param name="go">
            ///     The GameObject relative to which we are converting the coordinates
            ///     <paramref name="worldCoordinates" />.
            /// </param>
            /// <param name="worldCoordinates">The world Coordinates.</param>
            /// <returns>The <see cref="Vector3" /> to convert <paramref name="worldCoordinates" /> into.</returns>
            public static Vector3 ConvertWorldToLocalRelative(this GameObject go, Vector3 worldCoordinates)
            {
                return go.transform.InverseTransformPoint(worldCoordinates);
            }

            /// <summary>
            ///     Given a GameObject (gameObject), convert the given <paramref name="worldCoordinates" /> to local coordinates
            ///     relative to (gameObject).
            /// </summary>
            /// <param name="gameObjectToGetRelativeCoordinates">
            ///     The GameObject for whom we are converting the coordinates
            ///     <paramref
            ///         name="worldCoordinates" />
            ///     to be relative to.
            /// </param>
            /// <param name="worldCoordinates">The world Coordinates.</param>
            /// <returns>
            ///     The coordinates <paramref name="worldCoordinates" />, having been converted to be represented as local
            ///     coordinates relative to <paramref name="gameObjectToGetRelativeCoordinates" />.
            /// </returns>
            public static Vector3 ConvertWorldToLocalRelative(this GameObject gameObjectToGetRelativeCoordinates,
                Vector3? worldCoordinates)
            {
                // Reject any null coordinates.
                if (worldCoordinates == null)
                    throw new RuntimeException("You passed in an empty set of coordinates to the method " +
                                               "ConvertWorldToLocalRelative()!");

                // Then, call the function on the non-nullable version of the coordinates.
                return gameObjectToGetRelativeCoordinates.ConvertWorldToLocalRelative(worldCoordinates);
            }

            /// <summary>Deactivate (gameObject) if it's currently active.</summary>
            /// <param name="gameObjectToDeactivate">The GameObject to deactivate, if it is not deactivated already.</param>
            public static void DeactivateIfActive(this GameObject gameObjectToDeactivate)
            {
                if (gameObjectToDeactivate.activeSelf) gameObjectToDeactivate.SetActive(false);
            }

            /// <summary>Given a GameObject with a Rigidbody attached, freeze that rigid body's position.</summary>
            /// <param name="go">The GameObject whose RigidBody we should freeze.</param>
            public static void FreezeRigidBodyPosition(this GameObject go)
            {
                go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }

            /// <summary>Given a GameObject (obj), return the absolute path of (obj).</summary>
            /// <param name="obj">The GameObject to return the absolute path of.</param>
            /// <returns>The absolute path of the GameObject (obj).</returns>
            public static string GetAbsoluteGameObjectPath(this GameObject obj)
            {
                // Keep looping to get the name of the parent, and prepend it to the path, until there
                // are no more ancestors.
                string path = obj.name;
                while (obj.transform.parent != null)
                {
                    obj = obj.transform.parent.gameObject;
                    path = obj.name + "/" + path;
                }

                return path;
            }

            /// <summary>
            ///     Given a GameObject (gameObject), return the component of type T (where T is a superclass of Component) within
            ///     (gameObject) if it exists, and if no such component exists, add such a component and return it.
            /// </summary>
            /// <typeparam name="T">
            ///     The type of the component we should retrieve from <paramref name="go" />, or add if we cannot find
            ///     it.
            /// </typeparam>
            /// <param name="go">The GameObject whose component we should extract.</param>
            /// <returns>The component of type T attached to <paramref name="go" />; if <paramref name="go" /> has no</returns>
            public static T GetComponentAddIfNull<T>(this GameObject go) where T : Component
            {
                T newComponent = go.GetComponent<T>();
                if (newComponent == null) newComponent = go.AddComponent<T>();
                return newComponent;
            }

            /// <summary>Given a GameObject (thisGameObject), return the global distance between it and (otherGameObject).</summary>
            /// <param name="thisGameObject">The GameObject whose distance to (otherGameObject) we're trying to get.</param>
            /// <param name="otherGameObject">The other GameObject whose distance we're getting.</param>
            /// <returns>The distance between (thisGameObject) and (otherGameObject).</returns>
            public static float GetGlobalDistanceTo(this GameObject thisGameObject, GameObject otherGameObject)
            {
                Vector3 otherObjectLocation = otherGameObject.GetGlobalPosition();
                Vector3 thisObjectLocation = thisGameObject.GetGlobalPosition();
                return Vector3.Distance(otherObjectLocation, thisObjectLocation);
            }

            /// <summary>Given a game object (GameObject), return its global position (i.e. its position relative to the overworld).</summary>
            /// <param name="gameObject">The GameObject whose global position we're retrieving.</param>
            /// <returns>The global position of the given GameObject.</returns>
            public static Vector3 GetGlobalPosition(this GameObject gameObject)
            {
                try
                {
                    return gameObject.transform.position;
                }
                catch (Exception)
                {
                    throw new RuntimeException("GetGlobalPosition() just received a null GameObject as a parameter!");
                }
            }

            /// <summary>
            ///     Given a GameObject <paramref name="go" />, return that GameObject's position's x-component, relative to the
            ///     overworld.
            /// </summary>
            /// <param name="go">The GameObject whose global position's x component we're retrieving.</param>
            /// <returns>The GameObject's position's x-component, relative to the overworld.</returns>
            public static float GetGlobalPositionX(this GameObject go)
            {
                return go.transform.position.x;
            }

            /// <summary>Given a game object (obj), return its Y-height with respect to the overworld.</summary>
            /// <param name="gameObjectToGetPosition">The GameObject whose global position's y-component we should retrieve.</param>
            /// <returns>The y-component of the GameObject's position relative to the overworld.</returns>
            public static float GetGlobalPositionY(this GameObject gameObjectToGetPosition)
            {
                return gameObjectToGetPosition.transform.position.y;
            }

            /// <summary>Given a GameObject <paramref name="go" />, return its z position relative to the overworld.</summary>
            /// <param name="go">The gameObject.</param>
            /// <returns>The <see cref="float" />.</returns>
            public static float GetGlobalPositionZ(this GameObject go)
            {
                return go.transform.position.z;
            }

            /// <summary>Return the global rotation of this GameObject along the Y-axis, in degrees.</summary>
            /// <param name="go">The GameObject we're rotating.</param>
            /// <returns>The global rotation of this GameObject along the Y-axis, in degrees.</returns>
            public static float GetGlobalRotationY(this GameObject go)
            {
                return go.transform.rotation.eulerAngles.y;
            }

            /// <summary>Given a GameObject (gameObject), return the scale of the object relative to the overworld.</summary>
            /// <param name="gameObjectToRetrieveScaleFrom">The GameObject's global scale value to retrieve.</param>
            /// <returns>The scale of the GameObject <paramref name="gameObjectToRetrieveScaleFrom" /> relative to the overworld.</returns>
            public static Vector3 GetGlobalScale(this GameObject gameObjectToRetrieveScaleFrom)
            {
                return gameObjectToRetrieveScaleFrom.transform.lossyScale;
            }


            /// <summary>Given a game object (GameObject), return its local position (i.e. its position relative to its parent).</summary>
            /// <param name="gameObject">The GameObject whose local position we should retrieve.</param>
            /// <returns>The local position of the GameObject we should retrieve.</returns>
            public static Vector3 GetLocalPosition(this GameObject gameObject)
            {
                return gameObject.transform.localPosition;
            }

            /// <summary>Given a game object (gameObject), return its x-coordinate relative to its parent object.</summary>
            /// <param name="go">The GameObject whose local x-position we should retrieve.</param>
            /// <returns>The local x-position of the given <paramref name="go" />.</returns>
            public static float GetLocalPositionX(this GameObject go)
            {
                return go.transform.localPosition.x;
            }

            /// <summary>Given a game object (gameObject), return its z-coordinate relative to its parent object.</summary>
            /// <param name="go">The GameObject whose local y-position we should retrieve.</param>
            /// <returns>The local y-position of the given <paramref name="go" />.</returns>
            public static float GetLocalPositionY(this GameObject go)
            {
                return go.transform.localPosition.y;
            }

            /// <summary>Given a game object (gameObject), return its z-coordinate relative to its parent object.</summary>
            /// <param name="go">The GameObject whose local z-position we should retrieve.</param>
            /// <returns>The local z-position of the given <paramref name="go" />.</returns>
            public static float GetLocalPositionZ(this GameObject go)
            {
                return go.transform.localPosition.z;
            }

            /// <summary>Given a GameObject (gameObject), return its local scale (i.e. scale relative to its parent object).</summary>
            /// <param name="go">The GameObject whose local scale should be returned.</param>
            /// <returns>The local scale of <paramref name="go" />.</returns>
            public static Vector3 GetLocalScale(this GameObject go)
            {
                return go.transform.localScale;
            }

            /// <summary>
            ///     Given a GameObject <paramref name="go" />, return its MonoBehaviour component of type
            ///     <typeparamref name="T" />. If it has no such component, add the component to the object and then return it.
            /// </summary>
            /// <param name="go">The GameObject whose MonoBehaviour component we should extract.</param>
            /// <typeparam name="T">The type of MonoBehaviour we should look for.</typeparam>
            /// <returns>The MonoBehaviour component of type T attached to <paramref name="go" />.</returns>
            public static T GetMonoBehaviourAddIfNull<T>(this GameObject go) where T : MonoBehaviour
            {
                T newComponent = go.GetComponent<T>();
                if (newComponent == null) newComponent = go.AddComponent<T>();
                return newComponent;
            }

            /// <summary>
            ///     Given two GameObjects (gameObject) and (relativeObject), return (gameObject)'s location relative to
            ///     (relativeObject).
            /// </summary>
            /// <param name="go">The GameObject whose location we're setting relative to (relativeObject).</param>
            /// <param name="relativeGameObject">The GameObject we're setting (relativeObject) relative to.</param>
            /// <returns>The position of GameObject, relative to (relativeGameObject).</returns>
            public static Vector3 GetPositionRelativeTo(this GameObject go, GameObject relativeGameObject)
            {
                return relativeGameObject.transform.InverseTransformPoint(go.GetGlobalPosition());
            }

            /// <summary>Given a GameObject <paramref name="go" /> that has a solid colour component, return that colour.</summary>
            /// <param name="go">The GameObject whose colour component we should return.</param>
            /// <returns>The Color of the GameObject.</returns>
            public static Color GetSolidColor(this GameObject go)
            {
                return go.GetComponentAddIfNull<Renderer>().material.color;
            }

            /// <summary>
            ///     Given a GameObject <paramref name="gameObject" />, increment its local x position by
            ///     <paramref name="imgOffset" /> units.
            /// </summary>
            /// <param name="gameObject">The GameObject whose local x position we're incrementing.</param>
            /// <param name="imgOffset">The amount to offset the GameObject's x position by.</param>
            public static void IncrementLocalPositionX(this GameObject gameObject, float imgOffset)
            {
                gameObject.SetLocalPositionX(gameObject.GetLocalPositionX() + imgOffset);
            }

            /// <summary>Given a game object (GameObject) and a float (f), increment the GameObject's localPosition by that amount.</summary>
            /// <param name="go">The GameObject whose y-coordinate we're incrementing.</param>
            /// <param name="f">The amount we're incrementing (gameObject)'s height by.</param>
            public static void IncrementY(this GameObject go, float f)
            {
                Vector3 newVectorValue = go.GetLocalPosition();
                newVectorValue.y += f;
                go.SetLocalPosition(newVectorValue);
            }

            /// <summary>
            ///     Initialize and return a RectTransform whose margins are maxed out, and attach it to the GameObject
            ///     <paramref name="gameObject" />. By maxed out, we refer to a RectTransform whose anchorMin is equal to Vector2.zero,
            ///     and whose anchorMax is equal to Vector2.one.
            /// </summary>
            /// <param name="gameObject">
            ///     The GameObject holding the image objects for appliances that need to be shoved onto their
            ///     sockets.
            /// </param>
            /// <returns>A RectTransform whose margins are maxed out.</returns>
            public static RectTransform InitializeMaxedOutRectTransform(this GameObject gameObject)
            {
                RectTransform applianceHolderRect = gameObject.AddComponent<RectTransform>();
                applianceHolderRect.MaximizeAnchorSpace();
                return applianceHolderRect;
            }

            /// <summary>
            ///     Given the GameObject <paramref name="gameObject" /> and a sphere
            ///     <paramref
            ///         name="sphere" />
            ///     , return true iff <paramref name="gameObject" /> intersects with <paramref name="sphere" />.
            /// </summary>
            /// <param name="gameObject">The GameObject we're checking to see intersects with the sphere <paramref name="sphere" />.</param>
            /// <param name="sphere">The sphere that <paramref name="gameObject" /> intersects with.</param>
            /// <returns>true iff the given</returns>
            public static bool IntersectsWith(this GameObject gameObject, Sphere sphere)
            {
                return gameObject.GetComponent<Collider>().bounds.IntersectsWith(sphere);
            }

            /// <summary>
            ///     Given a GameObject <paramref name="go" />, an axis <paramref name="axis" /> and a float <paramref name="amount" />,
            ///     move that GameObject in the given axis direction by
            ///     <paramref
            ///         name="amount" />
            ///     units.
            /// </summary>
            /// <param name="go">The GameObject to move.</param>
            /// <param name="axis">The axis to move the GameObject along.</param>
            /// <param name="amount">The amount to move the GameObject by.</param>
            public static void MovePosition(this GameObject go, Axis3D axis, float amount)
            {
                Vector3 tempVec = go.transform.position; // Temporary variable

                // Increment the appropriate axis by the amount and reassign this transform's vector.
                switch (axis)
                {
                    case Axis3D.X:
                    {
                        tempVec.x += amount;
                        break;
                    }

                    case Axis3D.Y:
                    {
                        tempVec.y += amount;
                        break;
                    }

                    case Axis3D.Z:
                    {
                        tempVec.z += amount;
                        break;
                    }
                }

                go.transform.position = tempVec;
            }

            /// <summary>
            ///     Normalize the GameObject <paramref name="gameObject" /> by setting its local position and rotation to 0, and
            ///     setting the local scale (across all dimensions) to 1.
            /// </summary>
            /// <param name="gameObject">The GameObject to normalize.</param>
            public static void NormalizeTransform(this GameObject gameObject)
            {
                gameObject.SetLocalPosition(Vector3.zero);
                gameObject.SetLocalRotation(Vector3.zero);
                gameObject.SetLocalScale(Vector3.one);
            }

            /// <summary>
            ///     Given a GameObject <paramref name="go" /> that has presumably just been cloned, remove the "(Clone)" part as a
            ///     suffix from its name, as well as for all of its descendants.
            /// </summary>
            /// <param name="go">The GameObject whose names we should trim the "(Clone)" substring off of.</param>
            /// <returns>The <see cref="GameObject" /></returns>
            public static GameObject RemoveCloneSubstrRecursive(this GameObject go)
            {
                // Trim off the "clone" part of the name.
                go.name = go.name.TrimEnd("(Clone)");

                // Then, do the same for all children:
                foreach (GameObject child in go.GetChildren()) child.RemoveCloneSubstrRecursive();

                return go;
            }

            /// <summary>
            ///     Given a game object (GameObject) and three floats <paramref name="i" />,
            ///     <paramref
            ///         name="j" />
            ///     and <paramref name="k" /> set the game object's GLOBAL position to (i, j, k).
            /// </summary>
            /// <param name="gameObject">The GameObject whose global position we should set.</param>
            /// <param name="i">The i component of the global position.</param>
            /// <param name="j">The j component of the global position.</param>
            /// <param name="k">The k component of the global position.</param>
            public static void SetGlobalPosition(this GameObject gameObject, float i, float j, float k)
            {
                gameObject.transform.position = new Vector3(i, j, k);
            }

            /// <summary>
            ///     Given a game object (GameObject) and a 3D vector <paramref name="vec" /> , set the game object's GLOBAL
            ///     position to (vec).
            /// </summary>
            /// <param name="gameObject">The GameObject whose global position we should set</param>
            /// <param name="vec">The vector we should set <paramref name="gameObject" />'s global position to.</param>
            public static void SetGlobalPosition(this GameObject gameObject, Vector3 vec)
            {
                gameObject.SetGlobalPosition(vec.x, vec.y, vec.z);
            }

            /// <summary>
            ///     // Given a game object (GameObject) and a float <paramref name="newX" /> , set the GameObject's location on
            ///     the X-axis with respect to the overworld to that value.
            /// </summary>
            /// <param name="go">The GameObject whose global position we should set.</param>
            /// <param name="newX">The new X value we should set the global position to.</param>
            public static void SetGlobalPositionX(this GameObject go, float newX)
            {
                Vector3 newVectorValue = go.GetGlobalPosition();
                newVectorValue.x = newX;
                go.SetGlobalPosition(newVectorValue);
            }

            /// <summary>
            ///     Given a game object (GameObject) and a float <paramref name="newY" /> , set the GameObject's location on the
            ///     Y-axis with respect to the overworld to that value.
            /// </summary>
            /// <param name="go">The GameObject whose global Y position we should set.</param>
            /// <param name="newY">The new Y component we should set the GameObject to.</param>
            public static void SetGlobalPositionY(this GameObject go, float newY)
            {
                Vector3 newVectorValue = go.GetGlobalPosition();
                newVectorValue.y = newY;
                go.SetGlobalPosition(newVectorValue);
            }

            /// <summary>
            ///     Given a game object (GameObject) and a float <paramref name="newZ" /> , set the GameObject's location with
            ///     respect to the overworld to that value.
            /// </summary>
            /// <param name="go">The GameObject whose global position we should set.</param>
            /// <param name="newZ">The new Z value to set <paramref name="go" />'s value to.</param>
            public static void SetGlobalPositionZ(this GameObject go, float newZ)
            {
                Vector3 newVectorValue = go.GetGlobalPosition();
                newVectorValue.z = newZ;
                go.SetGlobalPosition(newVectorValue);
            }

            /// <summary>Set the global rotation along the X-axis for GameObject (gameObject) to (newGlobalRotationY).</summary>
            /// <param name="go">The GameObject whose global rotation must be modified.</param>
            /// <param name="newGlobalRotationX">The new value of (gameObject)'s global rotation on the x-axis..</param>
            public static void SetGlobalRotationX(this GameObject go, float newGlobalRotationX)
            {
                Vector3 objectRotation = go.transform.rotation.eulerAngles;
                objectRotation.x = newGlobalRotationX;
                go.transform.rotation = objectRotation.ToQuaternion();

                // gameObject.transform.rotation = Quaternion.Euler(newGlobalRotationX,
                // gameObject.transform.rotation.y, gameObject.transform.rotation.z);
            }

            /// <summary>Set the global rotation along the Y-axis for GameObject (gameObject) to (newGlobalRotationY).</summary>
            /// <param name="go">The GameObject whose global rotation must be modified.</param>
            /// <param name="newGlobalRotationY">The new value of (gameObject)'s global rotation.</param>
            public static void SetGlobalRotationY(this GameObject go, float newGlobalRotationY)
            {
                Vector3 objectRotation = go.transform.rotation.eulerAngles;
                objectRotation.y = newGlobalRotationY;
                go.transform.rotation = objectRotation.ToQuaternion();

                // gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x,
                // newGlobalRotationY, gameObject.transform.rotation.z);
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and a value <paramref name="newZ" /> , set the object's z-rotation to
            ///     <paramref name="newZ" /> .
            /// </summary>
            /// <param name="gameObject">The GameObject whose global rotation we should set.</param>
            /// <param name="newZ">The new value we should set the GameObject's value to.</param>
            public static void SetGlobalRotationZ(this GameObject gameObject, float newZ)
            {
                Vector3 objectRotation = gameObject.transform.rotation.eulerAngles;
                objectRotation.z = newZ;
                gameObject.transform.rotation = objectRotation.ToQuaternion();
            }

            /// <summary>Given a GameObject (obj), set its layer as well as those of its children to the layer defined by (newLayer).</summary>
            /// <param name="obj">The GameObject whose layer we should set.</param>
            /// <param name="newLayer">The layer value to set the GameObject to.</param>
            public static void SetLayerRecursively(this GameObject obj, int newLayer)
            {
                // Do nothing if the GameObject is null.
                if (null == obj) return;

                // Otherwise, set the object's layer...
                obj.layer = newLayer;

                // ...and then proceed to set the children's' layers (for non-null children)
                foreach (Transform child in obj.transform.Cast<Transform>().Where(child => null != child))
                {
                    child.gameObject.SetLayerRecursively(newLayer);
                }
            }

            /// <summary>
            ///     Given a game object (GameObject) and a 3D vector <paramref name="vec" /> , set the game object's position
            ///     relative to its parent object to (i, j, k).
            /// </summary>
            /// <param name="gameObject">The GameObject whose local position we should set.</param>
            /// <param name="vec">The vector we should set <paramref name="gameObject" />'s local position to.</param>
            public static void SetLocalPosition(this GameObject gameObject, Vector3 vec)
            {
                gameObject.SetLocalPosition(vec[0], vec[1], vec[2]);
            }

            /// <summary>
            ///     Given a game object (GameObject) and three integers <paramref name="i" />,
            ///     <paramref
            ///         name="j" />
            ///     , <paramref name="k" />, set the game object's position relative to its parent object to <paramref name="i" />,
            ///     <paramref name="j" />, <paramref name="k" />.
            /// </summary>
            /// <param name="gameObject">The GameObject whose local position we should set.</param>
            /// <param name="i">The x component to set the local position to./</param>
            /// <param name="j">The y component to set the local position to.</param>
            /// <param name="k">The z component to set the local position to.</param>
            public static void SetLocalPosition(this GameObject gameObject, int i, int j, int k)
            {
                gameObject.SetLocalPosition(i, j, (float) k);
            }

            /// <summary>
            ///     Given a game object (GameObject) and three floats <paramref name="i" />,
            ///     <paramref
            ///         name="j" />
            ///     , <paramref name="k" />, set the game object's position relative to its parent object to <paramref name="i" />,
            ///     <paramref name="j" />, <paramref name="k" />.
            /// </summary>
            /// <param name="gameObject">The GameObject whose local position we should set.</param>
            /// <param name="i">The x value to set the local position to.</param>
            /// <param name="j">The y value to set the local position to.</param>
            /// <param name="k">The z value to set the local position to.</param>
            public static void SetLocalPosition(this GameObject gameObject, float i, float j, float k)
            {
                gameObject.transform.localPosition = new Vector3(i, j, k);
            }

            /// <summary>
            ///     Given a GameObject (gameObject), set its local x-coordinate to the value
            ///     <paramref
            ///         name="newX" />
            ///     .
            /// </summary>
            /// <param name="go">The GameObject whose x component we should set.</param>
            /// <param name="newX">The new value to set the GameObject's x component to.</param>
            public static void SetLocalPositionX(this GameObject go, float newX)
            {
                Transform objectTransform = go.transform;
                Vector3 objectLocalPosition = objectTransform.localPosition;
                objectTransform.localPosition = new Vector3(newX, objectLocalPosition.y, objectLocalPosition.z);
            }

            /// <summary>
            ///     Given a game object (GameObject) and a 2D float vector <paramref name="vec" /> , set the game object's
            ///     position's x- and z- components to (vec)'s components.
            /// </summary>
            /// <param name="gameObject">The GameObject's x and z components we should set.</param>
            /// <param name="vec">The vector to set the GameObject's x and z components to.</param>
            public static void SetLocalPositionXZ(this GameObject gameObject, Vector2 vec)
            {
                gameObject.SetLocalPosition(vec.x, gameObject.GetLocalPosition().y, vec.y);
            }

            /// <summary>Given a game object (gameObject), return its y-coordinate relative to its parent object.</summary>
            /// <param name="go">The GameObject whose local Y position we should set.</param>
            /// <param name="f">The float value we should set the Y coordinate to.</param>
            public static void SetLocalPositionY(this GameObject go, float f)
            {
                Vector3 newVectorValue = go.GetLocalPosition();
                newVectorValue.y = f;
                go.SetLocalPosition(newVectorValue);
            }

            /// <summary>
            ///     Given a GameObject (gameObject), set its local z-coordinate to the value
            ///     <paramref
            ///         name="newZ" />
            ///     .
            /// </summary>
            /// <param name="go">The GameObject whose Z component we should set.</param>
            /// <param name="newZ">The new value we should set the Z component to.</param>
            public static void SetLocalPositionZ(this GameObject go, float newZ)
            {
                Transform objectTransform = go.transform;
                Vector3 objectLocalPosition = objectTransform.localPosition;
                objectTransform.localPosition = new Vector3(objectLocalPosition.x, objectLocalPosition.y, newZ);
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and a parameter <paramref name="newLocalRotation" /> , set the object's local
            ///     scale to (newLocalRotation).
            /// </summary>
            /// <param name="go">The GameObject whose local rotation we should set.</param>
            /// <param name="newLocalRotation">The new value of the local rotation we should set the GameObject's local rotation to.</param>
            public static void SetLocalRotation(this GameObject go, Vector3 newLocalRotation)
            {
                go.transform.localRotation =
                    Quaternion.Euler(newLocalRotation.x, newLocalRotation.y, newLocalRotation.z);
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and a parameter <paramref name="newX" /> , set the object's local rotation in
            ///     the X direction to (newX).
            /// </summary>
            /// <param name="go">The GameObject whose local rotation we should set.</param>
            /// <param name="newX">The new value we should set <paramref name="go" />'s X-rotation value to.</param>
            public static void SetLocalRotationX(this GameObject go, float newX)
            {
                // Get the value of the current rotation of the GameObject, and save the values of the y
                // and z axes while changing the x-axis.
                Quaternion currentRotation = go.transform.localRotation;
                go.transform.localRotation = Quaternion.Euler(newX, currentRotation.y, currentRotation.z);
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and a parameter <paramref name="newY" /> , set the object's local rotation in
            ///     the Y direction to (newY).
            /// </summary>
            /// <param name="go">The GameObject whose Y rotation component we should set.</param>
            /// <param name="newY">The value we should set <paramref name="go" />'s y component to.</param>
            public static void SetLocalRotationY(this GameObject go, float newY)
            {
                // Get the value of the current rotation of the GameObject, and save the values of the x
                // and z axes while changing the y-axis.
                Quaternion currentRotation = go.transform.localRotation;
                go.transform.localRotation = Quaternion.Euler(currentRotation.x, newY, currentRotation.z);
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and a parameter <paramref name="newLocalScale" /> , set the object's local
            ///     scale to (newLocalRotation).
            /// </summary>
            /// <param name="go">The GameObject whose local scale we should set.</param>
            /// <param name="newLocalScale">The new value to set the local scale to.</param>
            public static void SetLocalScale(this GameObject go, Vector3 newLocalScale)
            {
                go.transform.localScale = newLocalScale;
            }

            /// <summary>Given a GameObject (gameObject) and a float (newScale), scale the GameObject along all axes to (newScale).</summary>
            /// <param name="go">The GameObject to scale.</param>
            /// <param name="scaleFactor">The value to scale all dimensions to.</param>
            public static void SetLocalScale(this GameObject go, float scaleFactor)
            {
                go.SetLocalScale(scaleFactor, scaleFactor, scaleFactor);
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and three floats (newX, newY, newZ), set the object's local scale to
            ///     (newLocalRotation).
            /// </summary>
            /// <param name="go">The GameObject whose local scale we should set.</param>
            /// <param name="newX">The new X value to set the local scale to.</param>
            /// <param name="newY">The new Y value to set the local scale to.</param>
            /// <param name="newZ">The new Z value to set the local scale to.</param>
            public static void SetLocalScale(this GameObject go, float newX, float newY, float newZ)
            {
                go.SetLocalScale(new Vector3(newX, newY, newZ));
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and a float <paramref name="newX" /> , set the object's local scale in the x
            ///     direction to newX.
            /// </summary>
            /// <param name="go">The GameObject whose local X scale position we should set.</param>
            /// <param name="newX">The new value we should set <paramref name="go" />'s local X position to.</param>
            public static void SetLocalScaleX(this GameObject go, float newX)
            {
                Vector3 newScale = go.GetLocalScale();
                newScale.x = newX;
                go.SetLocalScale(newScale);
            }

            /// <summary>
            ///     Given a GameObject <paramref name="gameObject" />, set its local scale along the X and Z dimensions to the
            ///     value <paramref name="newXZ" />.
            /// </summary>
            /// <param name="gameObject">The GameObject whose local scale along the X and Z dimensions we're setting.</param>
            /// <param name="newXZ">The new value we're setting the GameObject's X and Z scales to.</param>
            public static void SetLocalScaleXZ(this GameObject gameObject, float newXZ)
            {
                gameObject.SetLocalScaleX(newXZ);
                gameObject.SetLocalScaleZ(newXZ);
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and a float <paramref name="newY" /> , set the object's local scale in the y
            ///     direction to (newY).
            /// </summary>
            /// <param name="go">The GameObject whose local Y scale component we should set.</param>
            /// <param name="newY">The new Y scale component we should set <paramref name="go" />'s value to.</param>
            public static void SetLocalScaleY(this GameObject go, float newY)
            {
                Vector3 newScale = go.GetLocalScale();
                newScale.y = newY;
                go.SetLocalScale(newScale);
            }

            /// <summary>
            ///     Given a GameObject (gameObject) and a float <paramref name="newZ" /> , set the object's local scale in the z
            ///     direction to (newZ).
            /// </summary>
            /// <param name="go">The GameObject whose local scale Z value we should set.</param>
            /// <param name="newZ">The new value we should set <paramref name="go" />'s Z scale to.</param>
            public static void SetLocalScaleZ(this GameObject go, float newZ)
            {
                Vector3 newScale = go.GetLocalScale();
                newScale.z = newZ;
                go.SetLocalScale(newScale);
            }

            /// <summary>Given a game object, set this game object's color to a solid color (col).</summary>
            /// <param name="go">The GameObject whose solid colour we're setting.</param>
            /// <param name="col">The colour we're setting our GameObject to.</param>
            public static void SetSolidColor(this GameObject go, Color col)
            {
                Renderer objectRenderer = go.GetComponentAddIfNull<Renderer>();
                objectRenderer.material.color = col;
            }

            /// <summary>
            ///     Given a string, return the game object with that path if it exists; otherwise, create a new game object with
            ///     that ID and return it.
            /// </summary>
            /// <param name="stringObjectPath">The path of the object to create.</param>
            /// <returns>The <see cref="GameObject" /> to create.</returns>
            internal static GameObject GetObjectCreateIfNull(string stringObjectPath)
            {
                // Create the game object if it doesn't exist.
                GameObject returnGameObject = GameObject.Find(stringObjectPath);
                if (returnGameObject == null) return new GameObject(stringObjectPath);

                // Otherwise, if it does, return it.
                return returnGameObject;
            }

            /// <summary>
            ///     Create an instance of a UnityObject from the Asset whose relative path in the resources folder is
            ///     <paramref name="objectPath" />, and return that instantiated UnityObject.
            /// </summary>
            /// <param name="objectPath">The relative path of the Asset to instantiate in the Resources directory.</param>
            /// <returns>The instantiated UnityObject instance, loaded from the provided relative path.</returns>
            public static T  InstantiateFromResourcePath<T>(string objectPath) where T : UnityObject
            {
                UnityObject assetInstance = Resources.Load<UnityObject>(objectPath);
                return UnityObject.Instantiate(assetInstance) as T;
            }


            /// <summary>
            /// Programatically click on the button attached to <paramref name="go"/>.
            /// </summary>
            /// <param name="go">The GameObject whose button should be invoked.</param>
            public static void  InvokeButton(this GameObject go)
            {
                go.GetComponent<Button>().onClick.Invoke();
            }

        }

    }

}