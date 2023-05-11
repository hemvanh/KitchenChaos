using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private GameObject kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractionDir;
    private BaseCounter selectedCounter;
    private KitchenObject currentKitchenObject;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("Player instance already existed!");
        }
        Instance = this;
    }
    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteracAlternateAction += GameInput_OnInteracAlternateAction;
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }
    private void GameInput_OnInteracAlternateAction(object sender, EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void HandleInteractions() {
        var inputVector = gameInput.GetMovementVectorNormalized();

        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractionDir = moveDir;
        }

        float interactDistance = 2f;

        // .RaycaseAll() -> returns all hit objects
        var isHit = Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit hitInfo, interactDistance, counterLayerMask);
        if (isHit) {
            if (hitInfo.transform.TryGetComponent(out BaseCounter baseCounter)) {
                // Hit Clear-Counter
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    private void HandleMovement() {
        var inputVector = gameInput.GetMovementVectorNormalized();

        var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float playerRadius = .7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // Cannot move toward moveDir

            // Attempt only X movement
            var moveDirX = new Vector3(moveDir.x, 0, 0);
            canMove = moveDirX.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove) {
                // Can move only on the X axis
                moveDir = moveDirX;
            } else {
                // Cannot move only on the X

                // Attempt only Z movement
                var moveDirZ = new Vector3(0, 0, moveDir.z);
                canMove = moveDirX.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // can move only on the Z
                    moveDir = moveDirZ;
                } else {
                    // Can not move in any direction
                }
            }

        }

        if (canMove) {
            transform.position += moveDistance * moveDir;
        }

        isWalking = moveDir != Vector3.zero;

        var rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public GameObject GetParentHoldPoint() {
        return kitchenObjectHoldPoint;
    }

    public void SetCurrentKitchenObject(KitchenObject ko) {
        // Player picks up something
        currentKitchenObject = ko;

        if (ko != null) {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
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
