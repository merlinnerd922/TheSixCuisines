using Extend;
using Helper;
using TSC.Game.Other;
using TSC.Game.SaveGame;
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
        /// The decor item that this UI element is denoting is unbought.
        /// </summary>
        private DecorAds decorItem;

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

        /// <summary>
        /// Purchase this decor item, if the player has enough money.
        /// </summary>
        public void BuyThisDecor()
        {
            // Store references to important variables.
            DecorDisplay display = this.GetGrandParentComponent<DecorDisplay>();
            GameSceneManager gameSceneManager = display.gameSceneManager;
            GameState gameState = gameSceneManager.gameState;
            
            // Prevent the purchase of the decor if the player doesn't have enough cash.
            if (gameState.cashOnHand < 25f)
            {
                return;
            }

            // Otherwise, purchase the decor and decrement the player's cash accordingly.
            gameSceneManager.SetCashOnHand(gameSceneManager.gameState.cashOnHand - 25f);
            gameState.decorAds.Add(decorItem);
            
            // Delete this GameObject immediately now that its stored item has been bought.
            Destroy(this.gameObject);
        }
        

        /// <summary>
        /// Initialize this unbought decor object.
        /// </summary>
        /// <param name="decorAd">The item to initialize this decor item with.</param>
        public void Initialize(DecorAds decorAd)
        {
            // Set this unbought decor object's sprite and text, before storing a reference to the decor item 
            // this item is storing.
            SetImage(decorAd.GetSprite());
            SetText(decorAd.ToTitleCaseSpacedString());
            this.decorItem = decorAd;
        }

    }

}