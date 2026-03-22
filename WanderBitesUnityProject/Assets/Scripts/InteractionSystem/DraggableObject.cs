using UnityEngine;

/* 
Inspired by: https://www.youtube.com/watch?v=izag_ZHwOtM
*/
public class DraggableObject : MonoBehaviour, IInteractable
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;
    private InteractionState _interactionState = InteractionState.Idle;
    [SerializeField]
    private Sprite _interactedSprite;
    [SerializeField]
    private Sprite _completedSprite;
    private SpriteRenderer _renderer;

    public bool EnableInteractionForCurrentStep()
    {
        if (_interactionState == InteractionState.Idle)
        {
            _interactionState = InteractionState.Ready;
            return true;
        }
        return false;
    }

    public bool CanInteract()
    {
        return _interactionState == InteractionState.Ready;
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Store the original position
        originalPosition = transform.position; 
    }

    private void Update()
    {
        if (isDragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        // todo: uncomment when the gameplay verfication is implementeds
        // if (_interactionState != InteractionState.Ready) return;
        _interactionState = InteractionState.Interacting;
        _renderer.sprite = _interactedSprite;
        // Remember the offset between the object's center and the click position
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseUp()
    {
        // todo: check if object is at the correct position aka where the other object is that it is meant to interact with
        isDragging = false;
        // Reset to original position when released
        transform.position = originalPosition;
    }
}