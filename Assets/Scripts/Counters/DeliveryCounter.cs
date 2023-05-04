public class DeliveryCounter : BaseCounter {
    public override void Interact(Player player) {
        if (player.HasCurrentKitchenObject()) {
            if (player.GetCurrentKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                player.GetCurrentKitchenObject().DestroySelf();
            }
        }
    }
}
