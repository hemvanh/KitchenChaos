using UnityEngine;

public class PlateIconUI : MonoBehaviour {
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        UpdateVisual();
    }
    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    private void UpdateVisual() {
        foreach (Transform child in transform) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (var kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
            var iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);

            // Get a SCRIPT compoment to call its public function
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
