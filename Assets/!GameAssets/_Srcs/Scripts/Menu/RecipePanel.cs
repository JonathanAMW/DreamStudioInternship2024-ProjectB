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
        [Header("Recipe Page Components")]
        [SerializeField] Image foodImage1;
        [SerializeField] TextMeshProUGUI foodName1;
        [SerializeField] TextMeshProUGUI[] ingredientString1;
        [SerializeField] Image foodImage2;
        [SerializeField] TextMeshProUGUI foodName2;
        [SerializeField] TextMeshProUGUI[] ingredientString2;
        [Space(20)]

        [Header("Code Page Components")]
        [SerializeField] Image codeImage;
        [SerializeField] TextMeshProUGUI codeName;
        [SerializeField] TextMeshProUGUI description;
        [Space(20)]

        [SerializeField] Button previousPageButton;
        [SerializeField] Button nextPageButton;

        [SerializeField] GameObject recipeObjects;
        [SerializeField] GameObject codeObjects;

        [SerializeField] RecipePage[] recipePages;
        [SerializeField] RecipePage[] codePages;
        // Start is called before the first frame update

        int currentRecipePage = 0;
        int currentCodePage = 0;

        FoodRecipe[] currentFoodRecipes;
        FoodRecipe[] currentCode;
        private void Start()
        {

            OpenRecipeSection();

            for (int i = 0; i < recipePages.Length; i++)
            {
                for (int j = 0; j < recipePages[i].recipes.Length; j++)
                {
                    if (recipePages[i].recipes[j] != null)
                    {
                        recipePages[i].recipes[j].InitializeRecipeVisual();
                    }
                   
                }
            }
            for (int i = 0; i < codePages.Length; i++)
            {
                for (int j = 0; j < codePages[i].recipes.Length; j++)
                {
                    Debug.Log(codePages[i].recipes[j].name);
                    codePages[i].recipes[j].InitializeRecipeVisual();
                }
            }
        }

        void UpdateRecipeDisplay()
        {
           

            if(recipeObjects.activeSelf)
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
                    foodImage1.gameObject.SetActive(true);
                    foodName1.gameObject.SetActive(true);
                    for (int i = 0; i < ingredientString2.Length; i++)//for every ingredient string in panel
                    {
                        ingredientString1[i].gameObject.SetActive(true);
                    }
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


                if (currentFoodRecipes[1] == null)
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
                return;
            }
            
            if(codeObjects.activeSelf)
            {
                currentCode= codePages[currentCodePage].recipes;
                if (currentCode[0] == null)
                {
                    codeImage.gameObject.SetActive(false);
                    codeName.gameObject.SetActive(false);
                    description.gameObject.SetActive(false);
                }
                else
                {
                    codeImage.gameObject.SetActive(true);
                    codeName.gameObject.SetActive(true);
                    description.gameObject.SetActive(true);

                    codeImage.sprite = currentCode[0].foodImage; //foodImage1 appearance based on currentFoodRecipe[0]
                    codeName.text = currentCode[0].foodName;//foodName1 text based on currentFoodRecipe[0]
                    description.text = currentCode[0].description;
                }
            }

        }
        public void NextPage()
        {
            

           

            if(recipeObjects.activeSelf)
            {
                currentRecipePage++;
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
                UpdateRecipeDisplay();
                return;
            }
            else if(codeObjects.activeSelf)
            {
                currentCodePage++;
                if (currentCodePage == 0) //1st page
                {
                    previousPageButton.gameObject.SetActive(false);
                    nextPageButton.gameObject.SetActive(true);
                }
                else if (currentCodePage == codePages.Length - 1) //last page
                {
                    previousPageButton.gameObject.SetActive(true);
                    nextPageButton.gameObject.SetActive(false);
                }
                else //middle page
                {
                    previousPageButton.gameObject.SetActive(true);
                    nextPageButton.gameObject.SetActive(true);
                }
                UpdateRecipeDisplay();
                return;
            }
            

        }

        public void PrevPage()
        {
            

            
            if(recipeObjects.activeSelf)
            {
                currentRecipePage--;
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
                UpdateRecipeDisplay();
                return;
            }
            if (codeObjects.activeSelf)
            {
                currentCodePage--;
                if (currentCodePage == 0) //1st page
                {
                    previousPageButton.gameObject.SetActive(false);
                    nextPageButton.gameObject.SetActive(true);
                }
                else if (currentCodePage == codePages.Length - 1) //last page
                {
                    previousPageButton.gameObject.SetActive(true);
                    nextPageButton.gameObject.SetActive(false);
                }
                else //middle page
                {
                    previousPageButton.gameObject.SetActive(true);
                    nextPageButton.gameObject.SetActive(true);
                }
                UpdateRecipeDisplay();
                return;
            }
        }

        public void OpenRecipeSection()
        {
            if (recipePages.Length > 1 && recipeObjects.activeSelf)//more than 1 pages on recipe pages
            {
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(true);
            }
            else
            {
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(false);
            }

            currentRecipePage = 0;
            recipeObjects.SetActive(true);
            codeObjects.SetActive(false);
            UpdateRecipeDisplay();
        }

        public void OpenCodeSection()
        {
            if (codePages.Length > 1)//more than 1 pages on code pages
            {
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(true);
            }
            else
            {
                previousPageButton.gameObject.SetActive(false);
                nextPageButton.gameObject.SetActive(false);
            }
            currentCodePage = 0;
            recipeObjects.SetActive(false);
            codeObjects.SetActive(true);
            UpdateRecipeDisplay();
        }
    }
}
