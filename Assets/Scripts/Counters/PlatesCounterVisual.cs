using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;
    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start() {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlatePicked += PlatesCounter_OnPlatePicked;
    }

    private void PlatesCounter_OnPlatePicked(object sender, System.EventArgs e) {
        var plateToBePicked = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateToBePicked);
        Destroy(plateToBePicked );
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
        var plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
 