using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {

    // Make it static so the Event is applied to ANY object
    // Any CuttingCounter would trigger the SAME Chop event
    // Only 1 Event handler for the any object of this class
    public static event EventHandler OnAnyChopping;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnChopping;

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

                    var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetCurrentKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressRequired,
                    });
                }
            } else {
                // Player's not carrying anything
            }
        } else {
            // There's already a Kitchen Object on the counter
            if (player.HasCurrentKitchenObject()) {
                // Player's carrying something
                if (player.GetCurrentKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetCurrentKitchenObject().GetKitchenObjectSO())) {
                        GetCurrentKitchenObject().DestroySelf();
                    }
                }
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
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressRequired,
            });
            OnChopping?.Invoke(this, EventArgs.Empty);
            OnAnyChopping?.Invoke(this, EventArgs.Empty);

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