using UnityEngine;

public class BaseCounter : MonoBehaviour ,IKitchenObjectParent{
    [SerializeField] private GameObject counterTopPoint;

    private KitchenObject currentKitchenObject;

    // make it virtual so child classes could have theirs own implementation
    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact(); should not be called");
    }
    public GameObject GetParentHoldPoint() {
        return counterTopPoint;
    }

    public void SetCurrentKitchenObject(KitchenObject ko) {
        currentKitchenObject = ko;
    }

    public KitchenObject GetCurrentKitchenObject() {
        return currentKitchenObject;
    }
    public void ClearCurrentKitchenObject() {
        currentKitchenObject = null;
    }

    public bool HasCurrentKitchenObject() {
        return currentKitchenObject != null;
    }
}
