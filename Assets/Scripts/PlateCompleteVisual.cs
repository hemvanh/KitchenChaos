using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {
    // struct is Value Types, just like int, float, enum, bool, ...
    // to make it appear in Unity inspector, just add attribute [Serializable]
    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_To_GameObjectList;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (var ingredientPair in kitchenObjectSO_To_GameObjectList) {
            ingredientPair.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (var ingredientPair in kitchenObjectSO_To_GameObjectList) {
            if (ingredientPair.kitchenObjectSO == e.kitchenObjectSO) {
                ingredientPair.gameObject.SetActive(true);
            }
        }
    }
}
