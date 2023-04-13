using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player) {
        if (!HasCurrentKitchenObject()) {
            // There is no kitchen Object here
            if (player.HasCurrentKitchenObject()) {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetCurrentKitchenObject().GetKitchenObjectSO())) {
                    // Player carrying something that can be chopped
                    player.GetCurrentKitchenObject().SetKitchenObjectParent(this);
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
            var outputKitchenObjectSO = GetOutputForInput(GetCurrentKitchenObject().GetKitchenObjectSO());
            GetCurrentKitchenObject().DestroySelf();

            // Calling static function of KitchenObject class
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return true;
        }
        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputIngredient) {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputIngredient)
                return cuttingRecipeSO.output;
        }
        return null;
    }
}