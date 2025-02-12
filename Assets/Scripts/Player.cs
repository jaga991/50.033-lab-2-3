using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{


    //Singleton pattern
    //property
    public static Player Instance { get; private set;  }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChange;
    //addes clearcounter as argument to eventhandler
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private BoxCollider2D boxCollider;

    private Vector3 lastInterectDir;
    private bool isWalking;

    //reference to player interacted counter
    private ClearCounter selectedCounter;


    private void Awake()
    {
        //singleton pattern, single instance of player
        if (Instance != null) 
        {
            Debug.LogError("More than one Player Instance (Singleton pattern violation)");
        }
        Instance = this;
    }


    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        boxCollider = GetComponent<BoxCollider2D>(); // Get player's BoxCollider2D
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        // check selectedCounter variable if assigned value by HandleInteractions
        if(selectedCounter != null)
        {
            Debug.Log(selectedCounter.name);
            selectedCounter.Interact();
        } 
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector2 moveDir = inputVector.normalized;
        Vector2 boxSize = boxCollider.size;

        float rayDistance = moveSpeed * Time.deltaTime + 0.8f; // Slight buffer

        if (moveDir != Vector2.zero)
        {
            lastInterectDir = moveDir;
        }
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, lastInterectDir, rayDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject) // Ignore player
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.TryGetComponent<ClearCounter>(out ClearCounter clearCounter))
                {
                    //has clearcounter
                    //if current clearcounter is different from last selected counter
                    if (clearCounter != selectedCounter)
                    {
                        SetSelectedCounter(clearCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);

                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
    }
    private void HandleMovement()
    {
        // Get movement input
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector2 moveDir = inputVector.normalized;

        // Get player's collider size for BoxCast
        Vector2 boxSize = boxCollider.size;
        float rayDistance = moveSpeed * Time.deltaTime + 0.1f; // Slight buffer

        // Perform BoxCast to detect if diagonal movement is blocked
        bool isBlocked = IsBlocked(moveDir, boxSize, rayDistance);

        if (isBlocked && moveDir.x != 0 && moveDir.y != 0) // Moving diagonally
        {
            // Try moving only along the X-axis
            Vector2 moveDirX = new Vector2(moveDir.x, 0);
            bool isBlockedX = IsBlocked(moveDirX, boxSize, rayDistance);

            // Try moving only along the Y-axis
            Vector2 moveDirY = new Vector2(0, moveDir.y);
            bool isBlockedY = IsBlocked(moveDirY, boxSize, rayDistance);

            if (!isBlockedX) moveDir = moveDirX; // Move in X direction
            else if (!isBlockedY) moveDir = moveDirY; // Move in Y direction
            else moveDir = Vector2.zero; // Fully blocked, stop movement
        }
        else if (isBlocked) // Fully blocked in one direction
        {
            moveDir = Vector2.zero;
        }

        isWalking = moveDir != Vector2.zero;
        Debug.Log(isWalking);
        // Move player
        transform.position += (Vector3)moveDir * moveSpeed * Time.deltaTime;
    }

    private bool IsBlocked(Vector2 direction, Vector2 boxSize, float rayDistance)
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0f, direction, rayDistance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider != boxCollider) // Ignore self
            {
                return true; // Blocked
            }
        }
        return false; // Not blocked
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public bool IsWalking ()
    {
        return isWalking;
    }


}
