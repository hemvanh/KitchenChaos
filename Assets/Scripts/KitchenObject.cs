using UnityEngine;
using UnityEngine.Rendering;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent newParent) {
        if (kitchenObjectParent != null) {
            kitchenObjectParent.ClearCurrentKitchenObject();
        }

        if (newParent.HasCurrentKitchenObject()) {
            Debug.LogError("The parent already has a Kitchen Object");
        }

        kitchenObjectParent = newParent;
        
        kitchenObjectParent.SetCurrentKitchenObject(this);

        transform.parent = kitchenObjectParent.GetParentHoldPoint().transform;
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }
}
