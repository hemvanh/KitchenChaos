using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private GameObject counterTopPoint;

    private KitchenObject currentKitchenObject;

    // Make it static so the Event is applied to ANY object
    // Any CuttingCounter would trigger the SAME PlacedHere event
    // Only 1 Event handler for the any object of this class
    public static event EventHandler OnAnyObjectPlacedHere;

    // make it virtual so child classes could have theirs own implementation
    public virtual void Interact(Player player) {
        Debug.Log("BaseCounter.Interact(); should not be called");
    }
    public virtual void InteractAlternate(Player player) {
        Debug.Log("BaseCounter.InteractAlternate(); should not be called");
    }
    public GameObject GetParentHoldPoint() {
        return counterTopPoint;
    }

    public void SetCurrentKitchenObject(KitchenObject ko) {
        currentKitchenObject = ko;

        if (ko != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
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
