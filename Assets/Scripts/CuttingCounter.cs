using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;
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
            } else {
                // Player's not carrying anything
                GetCurrentKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasCurrentKitchenObject()) {
            // There's a Kitchen object here
            GetCurrentKitchenObject().DestroySelf();

            // Calling static function of KitchenObject class
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}