using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasCurrentKitchenObject()) {
            // There is no kitchen Object here
            if (player.HasCurrentKitchenObject()) {
                // Player is carrying something
                player.GetCurrentKitchenObject().SetKitchenObjectParent(this);
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
}
