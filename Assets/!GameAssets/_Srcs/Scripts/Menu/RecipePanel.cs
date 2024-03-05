//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/06"
//----------------------------------------------------------------------

using TMPro;
using UnderworldCafe.CookingSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UnderworldCafe
{
    /// <summary>
    /// class for controlling the recipe panel UI
    /// </summary>
    public class RecipePanel : MonoBehaviour
    {
        [SerializeField] Image foodImage1;
        [SerializeField] TextMeshProUGUI foodName1;
        [SerializeField] TextMeshProUGUI[] ingredientString1;
        [SerializeField] Image foodImage2;
        [SerializeField] TextMeshProUGUI foodName2;
        [SerializeField] TextMeshProUGUI[] ingredientString2;

        [SerializeField] Button previousPageButton;
        [SerializeField] Button nextPageButton;

        [SerializeField] RecipePage[] recipePages;
        // Start is called before the first frame update

        int currentRecipePage = 0;

        Recipe.RecipeInformations[] currentRecipesInformation;
        private void Start()
        {
            if(recipePages.Length >1)//more than 1 pages
            {
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(true);
            }
            else
            {
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(false);
            }

            currentRecipesInformation = recipePages[0].recipes; //default food recipes is from 1st page
            UpdateRecipeDisplay();
        }

        void UpdateRecipeDisplay()
        {
            currentRecipesInformation = recipePages[currentRecipePage].recipes;

            if (string.IsNullOrEmpty(currentRecipesInformation[0].RecipeName))
            {
                foodImage1.gameObject.SetActive(false);
                foodName1.gameObject.SetActive(false);
                for (int i = 0; i < ingredientString1.Length; i++)//for every ingredient string in panel
                {
                    ingredientString1[i].gameObject.SetActive(false);
                }
            }
            else
            {
                foodImage1.gameObject.SetActive(true);
                foodName1.gameObject.SetActive(true);
                for (int i = 0; i < ingredientString2.Length; i++)//for every ingredient string in panel
                {
                    ingredientString1[i].gameObject.SetActive(true);
                }
                foodImage1.sprite = currentRecipesInformation[0].RecipeOutput.IngredientInformation.IngredientSprite; //foodImage1 appearance based on currentFoodRecipe[0]
                foodName1.text = currentRecipesInformation[0].RecipeName;//foodName1 text based on currentFoodRecipe[0]
                for (int i = 0; i < ingredientString1.Length; i++)//for every ingredient string in panel
                {
                    ingredientString1[i].text = "";
                    if (i < currentRecipesInformation[0].Requirements.Count)//ensures that it doesnt go over actual amount of ingredients list
                    {
                        ingredientString1[i].text = currentRecipesInformation[0].Requirements[i].IngredientInformation.Name;
                    }
                }
            }
            
            
            if(string.IsNullOrEmpty(currentRecipesInformation[1].RecipeName))
            {
                foodImage2.gameObject.SetActive(false);
                foodName2.gameObject.SetActive(false);
                for (int i = 0; i < ingredientString2.Length; i++)//for every ingredient string in panel
                {
                    ingredientString2[i].gameObject.SetActive(false);
                }
            }
            else
            {
                foodImage2.gameObject.SetActive(true);
                foodName2.gameObject.SetActive(true);
                for (int i = 0; i < ingredientString2.Length; i++)//for every ingredient string in panel
                {
                    ingredientString2[i].gameObject.SetActive(true);
                }
                foodImage2.sprite = currentRecipesInformation[1].RecipeOutput.IngredientInformation.IngredientSprite;//foodImage2 appearance based on currentFoodRecipe[1]
                foodName2.text = currentRecipesInformation[1].RecipeName;//foodName2 text based on currentFoodRecipe[1]
                for (int i = 0; i < ingredientString2.Length; i++)//for every ingredient string in panel
                {
                    ingredientString2[i].text = "";
                    if (i < currentRecipesInformation[1].Requirements.Count)//ensures that it doesnt go over actual amount of ingredients list
                    {
                        ingredientString2[i].text = currentRecipesInformation[1].Requirements[i].IngredientInformation.Name;
                    }
                }

            }

        }
        public void NextPage()
        {
            currentRecipePage++;

            UpdateRecipeDisplay();

            if(currentRecipePage ==0) //1st page
            {
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(true);
            }
            else if(currentRecipePage == recipePages.Length-1) //last page
            {
                previousPageButton.gameObject.SetActive(true);
                nextPageButton.gameObject.SetActive(false);
            }
            else //middle page
            {
                previousPageButton.gameObject.SetActive(true);
                nextPageButton.gameObject.SetActive(true);
            }
            
        }

        public void PrevPage()
        {
            currentRecipePage--;

            UpdateRecipeDisplay();

            if (currentRecipePage == 0) //1st page
            {
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(true);
            }
            else if (currentRecipePage == recipePages.Length - 1) //last page
            {
                previousPageButton.gameObject.SetActive(true);
                nextPageButton.gameObject.SetActive(false);
            }
            else //middle page
            {
                previousPageButton.gameObject.SetActive(true);
                nextPageButton.gameObject.SetActive(true);
            }
        }
    }
}
