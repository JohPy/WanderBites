using UnityEngine;

/* 
Inspired by: https://www.youtube.com/watch?v=izag_ZHwOtM
*/
public class DraggableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int _step;

    [SerializeField]
    private GameObject target;
    
    [SerializeField]
    private float _dropAlignDistance = 1f;
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition;
    private InteractionState _interactionState = InteractionState.Idle;
    [SerializeField]
    private Sprite _interactedSprite;
    [SerializeField]
    private Sprite _completedSprite;
    private SpriteRenderer _renderer;
    private GameplayController _gameplayController;
    private Collider2D _targetCollider;

    public bool EnableInteractionForCurrentStep()
    {
        if (_interactionState == InteractionState.Idle)
        {
            _interactionState = InteractionState.Ready;
            Debug.Log($"Enabled interaction for step {_step}");
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
        _gameplayController = FindFirstObjectByType<GameplayController>();
        if (target != null)
            _targetCollider = target.GetComponent<Collider2D>();
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
            if (_targetCollider != null)
            {
                Debug.Log(Vector2.Distance(transform.position, _targetCollider.bounds.center));
            }
        }
    }

    private void OnMouseDown()
    {
        if (_interactionState != InteractionState.Ready) return;
        _interactionState = InteractionState.Interacting;
        _renderer.sprite = _interactedSprite;
        // Remember the offset between the object's center and the click position
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;

        if (target != null && _targetCollider != null && Vector2.Distance(transform.position, _targetCollider.bounds.center) <= _dropAlignDistance)
        {
            _renderer.sprite = _completedSprite;
            _interactionState = InteractionState.Completed;
            _gameplayController?.OnInteractionCompleted(this);
        }
        else
        {
            _interactionState = InteractionState.Ready; // Allow retrying
        }

        transform.position = originalPosition;
    }

    public int GetStep()
    {
        return _step;
    }

    public InteractionMode GetInteractionMode()
    {
        return InteractionMode.Drag;
    }
}