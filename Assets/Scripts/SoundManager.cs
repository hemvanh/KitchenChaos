using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private void Awake() {
        Instance = this;
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {

        // This is for simple game with simple sound playing
        // If we need complex mixer, better make a prefab with Audio Source component
        // to utilise all the complex mixer properties
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyChopping += CuttingCounter_OnAnyChopping;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.trash, (sender as TrashCounter).transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectDrop, (sender as BaseCounter).transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectPickup, (sender as Player).transform.position);
    }

    private void CuttingCounter_OnAnyChopping(object sender, System.EventArgs e) {
        // Play the sound on that specific CuttingCounter
        PlaySound(audioClipRefsSO.chop, (sender as CuttingCounter).transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {

        PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    public void PlayFootstepSound(Vector3 position, float volume) {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }
}
