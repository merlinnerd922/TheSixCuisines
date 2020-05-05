﻿using BDT;
using Extend;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script to manage changing the amount of a certain type of object in the UI.
/// </summary>
/// <typeparam name="T">The type of object being incremented.</typeparam>
public abstract class MonoItemIncrementer<T> : MonoBehaviour
{

    /// <summary>The T instance that this button is meant to change the amount of.</summary>
    protected T _itemType;

    /// <summary>The Text component displaying the count for this item.</summary>
    protected Text _textComponent;

    /// <summary>Initialize this script's components upon which it relies on.</summary>
    public virtual void InitializeObjectComponents()
    {
        // Store a reference to the type of the object this incrementer is incrementing.
        this.InitializeItemReference();

        // Store a reference to the UI display object that manages this incrementer.
        this.InitializeUIDisplayComponents();

        // Store a reference to the Text component displaying the count for the type of object this incrementer is managing.
        this.InitializeUITextComponent();
    }

    /// <summary>
    /// Initialize the Text component that this incrementer modifies.
    /// </summary>
    protected abstract void InitializeUITextComponent();

    /// <summary>
    /// Initialize the UI components for displaying the components that this incrementer modifies.
    /// </summary>
    protected abstract void InitializeUIDisplayComponents();


    /// <summary>
    /// Store a reference to the type of the object this incrementer is incrementing.
    /// </summary>
    protected abstract void InitializeItemReference();

    /// <summary>
    /// Increment the amount of the count of item T that this button is an incrementer for by 1.</summary>
    public virtual void Increment()
    {
        // Store the new value of the count for this T after incrementing it by 1, and then set that new
        // information using the UI display scripting object.
        int incrementedValue = this.GetItemCount() + 1;
        this.SetItemAmountInUI(incrementedValue);
    }

    /// <summary>
    /// Set the amount of the item this incrementer is managing to <paramref name="incrementedValue"/>.
    /// </summary>
    /// <param name="incrementedValue">The value to set the amount of this incrementer's item type to. </param>
    protected abstract void SetItemAmountInUI(int incrementedValue);

    /// <summary>Decrement the amount of the T that this button is an incrementer for by 1.</summary>
    public virtual void Decrement()
    {
        // Store the new value of the count for this T after decrementing it by 1, and then set that new
        // information using the UI display scripting object. (Unless of course we don't have any).
        //
        // Ensure that the amount of this item does NOT go below 0.
        int currentItemAmount = this.GetItemCount();
        if (currentItemAmount > 0)
        {
            int incrementedValue = currentItemAmount - 1;
            this.SetItemAmountInUI(incrementedValue);
        }
    }

    /// <summary>Return the count for the item of type T that this incrementer is managing, as displayed in the UI.</summary>
    /// <returns>The count of the item of type T, as displayed in the UI.</returns>
    public virtual int GetItemCount()
    {
        // Parse the text component containing information on the count of item of type T that this button is managing.
        return int.Parse(this.GetTextString().Trim());
    }

    /// <summary>
    /// Return the text currently displayed inside the text component this incrementer is managing.
    /// </summary>
    /// <returns>the text currently displayed inside the text component this incrementer is managing.</returns>
    protected virtual string GetTextString()
    {
        return this._textComponent.text;
    }

}