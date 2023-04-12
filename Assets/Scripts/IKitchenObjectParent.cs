using UnityEngine;

public interface IKitchenObjectParent {
    public GameObject GetParentHoldPoint();
    public void SetCurrentKitchenObject(KitchenObject ko);
    public KitchenObject GetCurrentKitchenObject();
    public void ClearCurrentKitchenObject();
    public bool HasCurrentKitchenObject();
}
