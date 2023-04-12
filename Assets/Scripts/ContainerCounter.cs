using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    public event EventHandler OnPlayerGrabbedOject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!player.HasCurrentKitchenObject()) {
            // Player's not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedOject?.Invoke(this, EventArgs.Empty);
        }
    }
}
