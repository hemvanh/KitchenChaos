using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                var waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                Debug.Log(waitingRecipeSO.recipeName);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject) {
        foreach (var waitingRecipeSO in waitingRecipeSOList) {
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // having the same number of ingredients
                bool plateIngredientsMatchRecipe = true;
                foreach (var ingredient in waitingRecipeSO.kitchenObjectSOList) {
                    //cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (var plateIngredient in plateKitchenObject.GetKitchenObjectSOList()) {
                        //cycling through all ingredients on the plate
                        if (ingredient == plateIngredient) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // This Recipe ingredient was not found on the Plate
                        plateIngredientsMatchRecipe = false;
                    }
                }
                if (plateIngredientsMatchRecipe) {
                    // Player delivers correct Recipe
                    Debug.Log("Player delivers correct Recipe");
                    waitingRecipeSOList.Remove(waitingRecipeSO);
                    return;
                }
            }
        }

        // No matches found!
        // Player did not deliver correct Recipe
        Debug.Log("Player did not deliver correct Recipe");
    }
}
