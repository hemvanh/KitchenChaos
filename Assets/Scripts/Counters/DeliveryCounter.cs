public class DeliveryCounter : BaseCounter {

    // Making this a singleton to make the SoundFX
    // playing above it
    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
    public override void Interact(Player player) {
        if (player.HasCurrentKitchenObject()) {
            if (player.GetCurrentKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                // Only accept Plates

                DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
                player.GetCurrentKitchenObject().DestroySelf();
            }
        }
    }
}
