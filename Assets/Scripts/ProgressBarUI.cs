using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {

    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    // Cant expose an Interface to Unity inspector
    // so we need to create a placeholder for the GameObject
    // and get the Interface from Start()
    private IHasProgress hasProgress;

    // Accessing an external reference, do it in Start(), not Awake()
    private void Start() {
        // get the Interface from GameObject
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null) {
            Debug.LogError("This GameObject" + hasProgressGameObject +
                " does not have a component that implements IHasProgress");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;

        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
