using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {
    private const string CUT = "Cut";
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        cuttingCounter.OnChopping += CuttingCounter_OnChopping;
    }

    private void CuttingCounter_OnChopping(object sender, System.EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
