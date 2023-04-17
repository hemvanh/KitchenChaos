public class TrashCounter : BaseCounter {
    public override void Interact(Player player) {
        if (player.HasCurrentKitchenObject()) {
            player.GetCurrentKitchenObject().DestroySelf();
        }
    }
}
