using UnityEngine;
using System.Collections;


public class ClickableObject : MonoBehaviour, IInteractable
{
    [SerializeField]
    private int _step;
    private InteractionState _interactionState = InteractionState.Idle;
    [SerializeField]
    private float _interactDuration = 5f;
    [SerializeField]
    private Sprite _interactedSprite;
    [SerializeField]
    private Sprite _completedSprite;
    private SpriteRenderer _renderer;
    private GameplayController _gameplayController;

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

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _gameplayController = FindFirstObjectByType<GameplayController>();
    }

    public bool CanInteract()
    {
        return _interactionState == InteractionState.Ready;
    }

    public bool Interact()
    {
        Debug.Log($"Attempted interaction on object for step {_step} with interaction state {_interactionState}");
        if (_interactionState != InteractionState.Ready) return false;
        _interactionState = InteractionState.Interacting;
        AnimateInteraction();
        StartCoroutine(InteractionRoutine());
        return true;
    }

    private IEnumerator InteractionRoutine()
    {
        yield return new WaitForSeconds(_interactDuration);

        AnimateCompleted();
        _interactionState = InteractionState.Completed;
        _gameplayController?.OnInteractionCompleted(this);
    }

    private void AnimateInteraction()
    {
        _renderer.sprite = _interactedSprite;
    }

    private void AnimateCompleted()
    {
        _renderer.sprite = _completedSprite;
    }

    public int GetStep()
    {
        return _step;
    }

    public InteractionMode GetInteractionMode()
    {
        return InteractionMode.Click;;
    }
}