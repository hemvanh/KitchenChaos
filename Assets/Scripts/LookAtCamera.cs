using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    // This Camera is already cached by default, so it wont loop through the objects
    // to get the Camera every frame
    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
    }
}
