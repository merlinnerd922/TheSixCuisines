using UnityEngine;
using UnityEngine.UI;

namespace TSC.Game.HUDMenus.Display
{

    /// <summary>
    /// A class representing an unbought decor UI item in the unbought decor UI menu.
    /// </summary>
    public class UnboughtDecor : MonoBehaviour
    {

        /// <summary>
        /// The image for the decor item.
        /// </summary>
        public Image image;

        /// <summary>
        /// The text display for the decor item.
        /// </summary>
        public Text textObject;

        /// <summary>
        /// Set the image of this decor item to <paramref name="decorSprite"/>.
        /// </summary>
        /// <param name="decorSprite">The decor item to set the image to.</param>
        public void SetImage(Sprite decorSprite)
        {
            this.image.sprite = decorSprite;
        }

        /// <summary>
        /// Set the text display of this decor item to <paramref name="decorText"/>.
        /// </summary>
        /// <param name="decorText">The value to set this decor item's text display to.</param>
        public void SetText(string decorText)
        {
            this.textObject.text = decorText;
        }

    }

}