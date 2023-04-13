using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    // Accessing an external reference, do it in Start(), not Awake()
    private void Start() {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        barImage.fillAmount = 0f;
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;
    }
}
