using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    public event EventHandler OnPlayerGrabbedOject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!player.HasCurrentKitchenObject()) {
            // Player's not carrying anything
            var kitchenObject = Instantiate(kitchenObjectSO.prefab);
            kitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedOject?.Invoke(this, EventArgs.Empty);
        }
    }
}
