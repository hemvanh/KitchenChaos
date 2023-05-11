using TMPro;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsCountDownToStartActive()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Update() {
        countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownToStartTimer()).ToString();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
}
