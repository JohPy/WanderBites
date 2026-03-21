using UnityEngine;
using System.Collections;


public class ClickableObject : MonoBehaviour, IInteractable
{
    private InteractionState _interactionState = InteractionState.Idle;
    [SerializeField]
    private float _interactDuration = 5f;
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

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public bool CanInteract()
    {
        return _interactionState == InteractionState.Ready;
    }

    public bool Interact()
    {
        // todo: uncomment when the gameplay verfication is implementeds
        // if (!CanInteract()) return false;
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
    }

    private void AnimateInteraction()
    {
        _renderer.sprite = _interactedSprite;
    }

    private void AnimateCompleted()
    {
        _renderer.sprite = _completedSprite;
    }
}