//----------------------------------------------------------------------
// Author   : "Vanessa"
// Created  : "2024/02/06"
//----------------------------------------------------------------------

using TMPro;
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

        FoodRecipe[] currentFoodRecipes;
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

            currentFoodRecipes = recipePages[currentRecipePage].recipes; //default food recipes is from 1st page
            UpdateRecipeDisplay();
        }

        void UpdateRecipeDisplay()
        {
            currentFoodRecipes = recipePages[currentRecipePage].recipes;

            if (currentFoodRecipes[0] == null)
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
                foodImage1.sprite = currentFoodRecipes[0].foodImage; //foodImage1 appearance based on currentFoodRecipe[0]
                foodName1.text = currentFoodRecipes[0].foodName;//foodName1 text based on currentFoodRecipe[0]
                for (int i = 0; i < ingredientString1.Length; i++)//for every ingredient string in panel
                {
                    ingredientString1[i].text = "";
                    if (i < currentFoodRecipes[0].ingredients.Length)//ensures that it doesnt go over actual amount of ingredients list
                    {
                        ingredientString1[i].text = currentFoodRecipes[0].ingredients[i];
                    }
                }
            }
            
            
            if(currentFoodRecipes[1] == null)
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
                foodImage2.sprite = currentFoodRecipes[1].foodImage; //foodImage2 appearance based on currentFoodRecipe[1]
                foodName2.text = currentFoodRecipes[1].foodName;//foodName2 text based on currentFoodRecipe[1]
                for (int i = 0; i < ingredientString2.Length; i++)//for every ingredient string in panel
                {
                    ingredientString2[i].text = "";
                    if (i < currentFoodRecipes[1].ingredients.Length)//ensures that it doesnt go over actual amount of ingredients list
                    {
                        ingredientString2[i].text = currentFoodRecipes[1].ingredients[i];
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
