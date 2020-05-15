using Helper;
using Helper.ExtendSpace;
using UnityEngine;
using UnityUtils;

namespace TSC.Game.HUDOptions
{

    /// <summary>
    /// A shop containing new dishes that the player can purchase.
    /// </summary>
    public class NewDishesDisplay : HUDDisplay
    {
        
        /// <summary>
        /// The scene manager for the game scene.
        /// </summary>
        public GameSceneManager gameSceneManager;


        /// <summary>
        /// The GameObject holding all the dishes in this menu.
        /// </summary>
        public GameObject dishHolder;

        /// <summary>
        /// A prefab for a menu item on this new dishes display.
        /// </summary>
        public GameObject unboughtDishPrefab;
        
        /// <summary>
        /// Load all dishes that the user has NOT bought on this page.
        /// </summary>
        public void LoadUnboughtDishes()
        {
            // Clear out the existing menu.
            this.dishHolder.DestroyAllChildren();
            
            // For each unbought dish, load it.
            foreach (Dish dish in this.gameSceneManager.gameState.GetUnboughtDishes())
            {
                // Instantiate a GameObject representing the dish.
                GameObject instantiate = Instantiate(this.unboughtDishPrefab);
                this.dishHolder.AddChild(instantiate, false);
                
                // Initialize the dish's image and text display. 
                UnboughtDish unboughtDish =  instantiate.GetComponent<UnboughtDish>();
                unboughtDish.dishImage.sprite = GameSceneManager.GetDishSprite(dish);
                unboughtDish.dishText.text = dish.ToTitleCaseSpacedString();
            }
        }

    }

}