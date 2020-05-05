﻿using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityUtils
{

    /// <summary>
    /// A MonoBehaviour that contains a Text component that can be flashed.
    /// </summary>
    public abstract class TextFlasherMonoBehaviour : MonoBehaviour
    {

        /// <summary>
        /// A flag indicating the current flash state.
        /// </summary>
        private bool flashIsOn = false;

        /// <summary>
        /// The amount of time the flash has occurred for.
        /// </summary>
        private float flashElapsedSeconds;

        /// <summary>
        /// A flag indicating whether the flashing coroutine is happening.
        /// </summary>
        private bool coroutineIsHappening = false;

        /// <summary>
        /// The amount of time in seconds that has elapsed for the current flash period. Once this value reaches the maximum flash period duration, the flash should change colour.
        /// </summary>
        private float flashPeriodSecondsElapsed;

        /// <summary>
        /// A reference to the flashing coroutine that may be occurring.
        /// </summary>
        private IEnumerator _flashCoroutine;

        /// <summary>
        /// The text displaying the user's cash on hand.
        /// </summary>
        [FormerlySerializedAs("cashOnHandDisplay")] 
        public Text displayText;

        /// <summary>
        /// Return the colour that the text should be when the flash is on.
        /// </summary>
        /// <value>the colour that the text should be when the flash is on.</value>
        protected abstract Color flashOnColor { get; }

        /// <summary>
        /// Return the colour that the text should be when the flash is off.
        /// </summary>
        /// <value>The colour that the text should be when the flash is off.</value>
        protected abstract Color flashOffColor { get; }


        /// <summary>
        /// Flash this text.
        /// </summary>
        public void Flash()
        {
            // Prevent more than one flashing coroutine from occurring.
            this.StopFlashingIfHappening();

            // The flash hasn't happened yet so reset it.
            this.flashElapsedSeconds = 0f;
            
            // Store and start the flashing coroutine.
            this._flashCoroutine = this.GetFlashCoroutine();
            this.StartCoroutine(this._flashCoroutine);
        }

        /// <summary>
        /// Stop the flashing of this text if it's happening.
        /// </summary>
        protected void StopFlashingIfHappening()
        {
            if (this.coroutineIsHappening)
            {
                this.MarkCoroutineAsOff();
                this.StopCoroutine(this._flashCoroutine);
            }
        }

        /// <summary>
        /// Return a coroutine that flashes the attached text two different colours.
        /// </summary>
        /// <returns>a coroutine that flashes the attached text two different colours.</returns>
        private IEnumerator GetFlashCoroutine()
        {
            // Mark the start of this coroutine and flash the text for the provided amount of time.
            this.coroutineIsHappening = true;

            while (this.flashElapsedSeconds < flashTime)
            {
                // Alter the text colour every frame, depending on the flash flag.
                this.displayText.color = this.flashIsOn ? this.flashOnColor : this.flashOffColor;

                // Increment the elapsed flash time.
                this.flashElapsedSeconds += Time.deltaTime;

                // Increment the amount of time the flash has been in its current state. If the amount of time elapsed
                // exceeds the amount of time the flash state is allowed to be in its current state, then toggle
                // the flash flag and reset the flash period.
                this.flashPeriodSecondsElapsed += Time.deltaTime;
                if (this.flashPeriodSecondsElapsed >= flashPeriodTime)
                {
                    this.flashIsOn = !this.flashIsOn;
                    this.flashPeriodSecondsElapsed = 0;
                }

                // We're done for the current frame.
                yield return null;
            }

            // Now that we're done, stop the coroutine and reset the associated values.
            this.MarkCoroutineAsOff();
        }

        /// <summary>
        /// Return the amount of time that the text should flash a specific colour before shifting to the next colour.
        /// </summary>
        protected abstract float flashPeriodTime { get;}

        /// <summary>
        /// Return the amount of time the attached text should flash for when prompted.
        /// </summary>
        /// <value>the amount of time the attached text should flash for when prompted.</value>
        protected abstract float flashTime { get; }

        /// <summary>
        /// Mark this coroutine as being off, and moreover reset all flash variables as needed.
        /// </summary>
        private void MarkCoroutineAsOff()
        {
            // Reset all flash variables as needed.
            this.displayText.color = flashOffColor;
            this.flashIsOn = false;
            this.flashElapsedSeconds = 0;
            this.flashPeriodSecondsElapsed = 0;
            
            // Mark the coroutine as being off.
            this.coroutineIsHappening = false;
        }

    }

}