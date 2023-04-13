using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    public override void Interact(Player player) {
        if (!HasCurrentKitchenObject()) {
            // There is no kitchen Object here
            if (player.HasCurrentKitchenObject()) {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetCurrentKitchenObject().GetKitchenObjectSO())) {
                    // Player carrying something that can be chopped
                    player.GetCurrentKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                }
            } else {
                // Player's not carrying anything
            }
        } else {
            // There's already a Kitchen Object on the counter
            if (player.HasCurrentKitchenObject()) {
                // Player's carrying something
            } else {
                // Player's not carrying anything
                GetCurrentKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasCurrentKitchenObject() && HasRecipeWithInput(GetCurrentKitchenObject().GetKitchenObjectSO())) {
            // There's a Kitchen object here AND it can be cut
            cuttingProgress++;
            var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetCurrentKitchenObject().GetKitchenObjectSO());

            if (cuttingRecipeSO.cuttingProgressRequired <= cuttingProgress) {
                var outputKitchenObjectSO = GetOutputForInput(GetCurrentKitchenObject().GetKitchenObjectSO());
                GetCurrentKitchenObject().DestroySelf();

                // Calling static function of KitchenObject class
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputIngredient) {
        var cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputIngredient);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        return null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input) {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == input)
                return cuttingRecipeSO;
        }
        return null;
    }
}