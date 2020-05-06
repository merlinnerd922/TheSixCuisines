﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Extend;
using Helper.ExtendSpace;
using UnityEngine;
 using Object = System.Object;

 namespace UnityUtils
{

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class LocalGameObjectFamilyUtils
    {

        /// <summary>
        ///     Given a GameObject (gameObject) and another GameObject (child), set (child)'s parent to be (parent). If
        ///     (worldPositionStays) is true, then maintain the global position of the child object after setting the parent;
        ///     otherwise, set the local position of the child object so its local position is the same as before its parent was
        ///     set. Afterwards, return the newly created child object.
        /// </summary>
        /// <param name="go">The GameObject we'll be adding a child to.</param>
        /// <param name="child">The child we'll be adding to the GameObject.</param>
        /// <param name="worldPositionStays">A flag indicating whether the world position of the created GameObject should stay.</param>
        /// <returns>The newly created GameObject.</returns>
        public static GameObject AddChild(this GameObject go, GameObject child, bool worldPositionStays = true)
        {
            child.SetParent(go, worldPositionStays);
            return child;
        }

        /// <summary>
        ///     Given a GameObject <paramref name="go" /> and another GameObject (child), set (child)'s parent to be (parent)
        ///     and return (child). Given a GameObject (gameObject) and a string (childName), create a new empty GameObject named
        ///     (child), set (child)'s parent to be (parent), and return (child).
        /// </summary>
        /// <param name="go">The GameObject we're adding a child to.</param>
        /// <param name="childName">The name of the GameObject we're creating.</param>
        /// <returns>The GameObject we added as a child to (gameObject).</returns>
        public static GameObject AddChild(this GameObject go, string childName)
        {
            GameObject child = new GameObject(childName);
            return go.AddChildReturn(child);
        }

        /// <summary>
        ///     Given a game object (parent), a GameObject (child) and a string consisting of a name (name) to set the child
        ///     game object, add (child) to (parent) as a child, set its name to (name) and return it.
        /// </summary>
        /// <param name="parent">The parent to add a child to.</param>
        /// <param name="child">The child to be added to the (parent).</param>
        /// <param name="name">The name of the new (child).</param>
        /// <returns>The child object.</returns>
        public static GameObject AddChild(this GameObject parent, GameObject child, string name)
        {
            parent.AddChild(child);
            child.name = name;
            return child;
        }

        /// <summary>
        ///     Given a GameObject <paramref name="gameObjectToAppend" />, add a child object
        ///     <paramref
        ///         name="childToAdd" />
        ///     to it.
        /// </summary>
        /// <param name="gameObjectToAppend">The GameObject to add a child to.</param>
        /// <param name="childToAdd">The child to add to the GameObject.</param>
        /// <returns>The newly added child object for the parent object <paramref name="gameObjectToAppend" />.</returns>
        public static GameObject AddChildReturn(this GameObject gameObjectToAppend, GameObject childToAdd)
        {
            childToAdd.SetParent(gameObjectToAppend);
            return childToAdd;
        }

        /// <summary>Given a game object, return the number of children this object has.</summary>
        /// <param name="gameObjectToCountChildren">The GameObject whose children we should count.</param>
        /// <returns>The number of children <paramref name="gameObjectToCountChildren" /> has.</returns>
        public static int CountChildren(this GameObject gameObjectToCountChildren)
        {
            return gameObjectToCountChildren.transform.Cast<Transform>().Count();
        }

        /// <summary>
        ///     Given a GameObject <paramref name="gameObjectForWhichToFindChild" />, return its child who has the name
        ///     <paramref name="childName" /> if it exists, or a brand new GameObject parented to it with that name.
        /// </summary>
        /// <param name="gameObjectForWhichToFindChild">
        ///     The GameObject whose child we are attempting to find, or create if no such
        ///     child exists.
        /// </param>
        /// <param name="childName">The name of the child to find and return, or create and return</param>
        /// <returns>
        ///     The first child object of <paramref name="gameObjectForWhichToFindChild" /> with the name
        ///     <paramref name="childName" />, or a brand new child object with that name if it does not already exist.
        /// </returns>
        public static GameObject FindChildCreateIfNull(this GameObject gameObjectForWhichToFindChild, string childName)
        {
            GameObject childObject = gameObjectForWhichToFindChild.FindFirstChild(childName);
            if (childObject == null)
                childObject = gameObjectForWhichToFindChild.AddChild(new GameObject(childName));
            return childObject;
        }

        /// <summary>
        ///     Given a GameObject (fromGameObject) and a relative path, return the object within (fromGameObject) named this
        ///     relative path. However, use the child transform instead of GameObject.Find().
        /// </summary>
        /// <param name="fromGameObject">The GameObject from which to find a child GameObject.</param>
        /// <param name="withName">The name of the child object to find.</param>
        /// <returns>The child object with the given name attached to <paramref name="fromGameObject" />.</returns>
        public static GameObject FindChildWithTransform(this GameObject fromGameObject, string withName)
        {
            return fromGameObject.GetChildren().FirstOrDefault(child => child.name == withName);
        }

        /// <summary>
        ///     Given a GameObject <paramref name="fromGameObject" /> and the name
        ///     <paramref
        ///         name="withName" />
        ///     of a potential GameObject, return the FIRST child we find with that name.
        /// </summary>
        /// <returns>The FIRST child of <paramref name="fromGameObject" /> with the name <paramref name="withName" />.</returns>
        /// <param name="fromGameObject">The GameObject whose children we're searching.</param>
        /// <param name="withName">The name of the child object we're looking for.</param>
        public static GameObject FindFirstChild(this GameObject fromGameObject, string withName)
        {
            // Use the Transform object to find (fromGameObject)'s child with (withName). Return null
            // if we don't find it, and the GameObject that has that name otherwise.
            Transform childTransform = fromGameObject.transform.Find(withName);
            if (childTransform != null) return childTransform.gameObject;
            return null;
        }


        /// <summary>
        /// Return the descendant of <paramref name="go"/> whose path is <paramref name="descendantPath"/>.
        /// </summary>
        /// <param name="go">The GameObject whose descendant should be returned.</param>
        /// <param name="descendantPath">The relative path of the descendant to return.</param>
        /// <returns>The descendant of <paramref name="go"/> whose path is <paramref name="descendantPath"/>.</returns>
        public static GameObject GetDescendant(this GameObject go, string descendantPath)
        {
            return go.transform.GetDescendant(descendantPath);
        }

        /// <summary>
        ///     Given a GameObject (gameObject) and a type <typeparamref name="T" /> that inherits from component, iterate
        ///     through all children and return the first component found of that type.
        /// </summary>
        /// <typeparam name="T">The type of the child component to find.</typeparam>
        /// <param name="gameObjectToSearch">The GameObject we must search for a child component.</param>
        /// <returns>The first child component we could find among <paramref name="gameObjectToSearch" />'s children.</returns>
        public static T FindFirstComponentInChildren<T>(this GameObject gameObjectToSearch) where T : Component
        {
            return gameObjectToSearch.GetChildren().Select(child => child.GetComponent<T>())
                .FirstOrDefault(component => component != null);
        }

        /// <summary>
        ///     Given a GameObject (fromGameObject) and a relative path, return the object within (fromGameObject) named this
        ///     relative path.
        /// </summary>
        /// <param name="go">The GameObject from whose children we should search.</param>
        /// <param name="childStr">The name of the first child GameObject we find having that name we should return.</param>
        /// <returns>The first GameObject we find having a child named <paramref name="childStr" />.</returns>
        public static GameObject GetChild(this GameObject go, string childStr)
        {
            return go.GetChildren().FirstOrDefault(childObj => childObj.name == childStr);
        }

        /// <summary>
        ///     Given a game object (parent) and a string <paramref name="childStr" /> that potentially is the name of a game
        ///     object that (parent) has, create the child with that name if it doesn't exist, and then return that child.
        /// </summary>
        /// <param name="go">The GameObject from which we should find a child GameObject named <paramref name="childStr" />.</param>
        /// <param name="childStr">
        ///     The name of the child object to <paramref name="go" /> to retrieve, or create if we were unable
        ///     to find it.
        /// </param>
        /// <returns>The first child object we find parented to <paramref name="go" /> named <paramref name="childStr" />.</returns>
        public static GameObject GetChildAddIfNull(this GameObject go, string childStr)
        {
            GameObject childGameObj = go.FindFirstChild(childStr);
            if (childGameObj == null)
            {
                childGameObj = new GameObject(childStr);
                childGameObj.SetParent(go);
            }

            return childGameObj;
        }

        /// <summary>Given a GameObject (gameObject), return a list of all of its children.</summary>
        /// <param name="go">The GameObject whose children we should return.</param>
        /// <returns>The list of all of <paramref name="go" />'s children.</returns>
        public static IEnumerable<GameObject> GetChildren(this GameObject go)
        {
            // Initialize the list, get all of the game object's children, and then return the list.
            return (from Transform t in go.transform select t.gameObject).ToList();
        }

        /// <summary>Given a game object, return an array of all of its children.</summary>
        /// <param name="go">The GameObject whose array of children we should get.</param>
        /// <returns>The array of all of <paramref name="go" />'s children.</returns>
        public static GameObject[] GetChildrenArray(this GameObject go)
        {
            // Iterate through all of this game object's children's transforms and add each of the
            // child game objects to the return list.
            return (from Transform t in go.transform select t.gameObject).ToArray();
        }

        /// <summary>
        ///     Given a GameObject (gameObject) and the name of a potential (literal) grandchild
        ///     <paramref name="grandchildName" /> , return the first GameObject with a name of (grandChildName), and null if none
        ///     exists.
        /// </summary>
        /// <param name="go">The GameObject whose first grandchild we should return.</param>
        /// <param name="grandchildName">The name of the grandchild we should return.</param>
        /// <returns>The first grandchild of <paramref name="go" /> we find, named <paramref name="grandchildName" />.</returns>
        public static GameObject GetFirstGrandchild(this GameObject go, string grandchildName)
        {
            // Iterate through children and then grandchildren in a double loop.
            return go.GetChildren().SelectMany(childObj => childObj.GetChildren())
                .FirstOrDefault(candidateGrandchild => candidateGrandchild.name == grandchildName);
        }

        /// <summary>Given a GameObject (gameObject), return its grandparent GameObject.</summary>
        /// <param name="go">The GameObject whose grandparent should be retrieved.</param>
        /// <returns>The grandparent of the GameObject <paramref name="go" />.</returns>
        public static GameObject GetGrandparent(this GameObject go)
        {
            return go.GetParent().GetParent();
        }

        /// <summary>
        ///     Given a GameObject <paramref name="go" />, return that GameObject's sibling whose name is
        ///     <paramref name="siblingName" />.
        /// </summary>
        /// <param name="go">The GameObject whose sibling should be returned.</param>
        /// <param name="siblingName">The name of the sibling to return.</param>
        /// <returns>The GameObject <paramref name="go" />'s sibling whose name is <paramref name="siblingName" />.</returns>
        public static GameObject GetSibling(this GameObject go, string siblingName)
        {
            return go.GetParent().GetChild(siblingName);
        }

        /// <summary>
        ///     Given the GameObject <paramref name="go" />, return that GameObject's <paramref name="nthAncestor" /> most
        ///     immediate ancestor.
        /// </summary>
        /// <param name="go">The GameObject whose ancestor is being retrieved.</param>
        /// <param name="nthAncestor">The recency index of the ancestor to retrieve.</param>
        /// <returns>The <paramref name="nthAncestor" />th most immediate ancestor of <paramref name="go" />.</returns>
        public static GameObject GetNthAncestor(this GameObject go, int nthAncestor)
        {
            // Keep iterating over the GameObject's ancestors until we reach the appropriate index.
            GameObject returnAncestor = go;
            for (int i = 0; i < nthAncestor; i++) returnAncestor = returnAncestor.GetParent();

            return returnAncestor;
        }

        /// <summary>Given a game object <paramref name="go" />, return its parent.</summary>
        /// <param name="go">The GameObject whose parent object we should return.</param>
        /// <returns>The parent object of <paramref name="go" />.</returns>
        public static GameObject GetParent(this GameObject go)
        {
            return go.transform.parent.gameObject;
        }

        /// <summary>Return <paramref name="gameObject" />'s grandparent's <typeparamref name="T" /> component.</summary>
        /// <param name="gameObject">The GameObject whose grandparent's component we want to retrieve.</param>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <returns><paramref name="gameObject" />'s grandparent's <typeparamref name="T" /> component.</returns>
        public static T GetGrandparentComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetGrandparent().GetComponent<T>();
        }

        /// <summary>
        ///     Given a GameObject <paramref name="gameObject" />, return the component of type <typeparamref name="T" />
        ///     in its child named <paramref name='childName' />.
        /// </summary>
        /// <param name="gameObject">The GameObject whose named (in the parameters) child's component to retrieve.</param>
        /// <param name="childName">The name of the child whose component should be retrieved.</param>
        /// <typeparam name="T">The type of the component to retrieve.</typeparam>
        /// <returns>
        ///     The component of type <typeparamref name="T" /> in <paramref name="gameObject" />'s child named
        ///     <paramref name='childName' />.
        /// </returns>
        public static T GetComponentInChild<T>(this GameObject gameObject, string childName) where T : Component
        {
            return gameObject.GetChild(childName).GetComponent<T>();
        }


        /// <summary>
        ///     /// Given a game object (gameObject), set its parent to some parent value
        ///     <paramref
        ///         name="parentObject" />
        ///     . Maintain the object's global position if (worldPositionStays) is true; otherwise, make sure the child's local
        ///     position stays the same.
        /// </summary>
        /// <param name="gameObject">The GameObject whose parent object we should set.</param>
        /// <param name="parentObject">The parent object we should set GameObject's parent to.</param>
        /// <param name="setLocalScale">
        ///     The local scale we should set for <paramref name="gameObject" />, relative to
        ///     <paramref name="parentObject" />.
        /// </param>
        public static void SetParent(this GameObject gameObject, GameObject parentObject, Vector3 setLocalScale)
        {
            gameObject.SetParent(parentObject);
            gameObject.SetLocalScale(setLocalScale);
        }

        /// <summary>
        ///     Set the parent of GameObject (gameObject) to (parent). Keep the relative position iff (worldPositionStays) is
        ///     true.
        /// </summary>
        /// <param name="gameObject">The GameObject that we're parenting to (parent).</param>
        /// <param name="parent">The parent object we're parenting (gameObject) to.</param>
        /// <param name="globalPositionStays">
        ///     A flag indicating whether the relative position of the object should remain the same
        ///     after assignment.
        /// </param>
        public static void SetParent(this GameObject gameObject, GameObject parent, bool globalPositionStays = true)
        {
            gameObject.transform.SetParent(parent.transform, globalPositionStays);
        }

        /// <summary>
        ///     Given a transform (parentTransform) and a GameObject (gameObject), set (gameObject)'s parent transform to be
        ///     (parentTransform).
        /// </summary>
        /// <param name="go">The GameObject whose parent we're modifying.</param>
        /// <param name="parentTransform">The transform object belonging to the new parent of gameObject.</param>
        public static void SetParentTransform(this GameObject go, Transform parentTransform)
        {
            go.transform.parent = parentTransform;
        }

        /// <summary>
        /// Return the root ancestor of this GameObject, or itself if it has no parent.
        /// </summary>
        /// <param name="gameObject">The GameObject whose root ancestor should be returned.</param>
        /// <returns>The root ancestor of this GameObject, or itself if it has no parent.</returns>
        public static GameObject GetRootAncestor(this GameObject gameObject)
        {
            return gameObject.transform.root.gameObject;
        }

        /// <summary>
        /// Return the component of type <typeparamref name="T"/> of the descendant at the path <paramref
        /// name="descendantPath"/> relative to the provided GameObject <paramref name="gameObject"/>.
        /// </summary>
        /// <param name="gameObject">The GameObject whose descendant at the provided path should be returned.</param>
        /// <param name="descendantPath">The path of the descendant to return.</param>
        /// <returns>The component of type <typeparamref name="T"/> of the descendant at the path <paramref
        /// name="descendantPath"/> relative to the provided GameObject <paramref name="gameObject"/>.</returns>
        public static T GetDescendantComponent<T>(this GameObject gameObject, string descendantPath)
            where T : MonoBehaviour
        {
            return gameObject.GetDescendant(descendantPath).GetComponent<T>();
        }


    }

}