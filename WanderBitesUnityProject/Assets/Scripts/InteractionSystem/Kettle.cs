using System.Collections;
using UnityEngine;

public enum KettleState
{
    Idle,
    Boiling,
    Ready
}

public class Kettle : MonoBehaviour, IInteractable
{
    [SerializeField] private float _boilDuration = 5f;
    [SerializeField] private Sprite _defaultSprite; // source: https://pixabay.com/illustrations/kettle-teapot-old-kettle-cooking-7449648/
    [SerializeField] private Sprite _interactedSprite;
    private SpriteRenderer _renderer;
    private KettleState _kettleState = KettleState.Idle;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    
    public bool CanInteract()
    {
        return _kettleState == KettleState.Idle;
    }

    public bool Interact()
    {
        if (!CanInteract()) return false;

        _kettleState = KettleState.Boiling;
        AnimateBoiling();
        StartCoroutine(BoilRoutine());
        return true;
    }

    private IEnumerator BoilRoutine()
    {
        yield return new WaitForSeconds(_boilDuration);

        AnimateReady();
        _kettleState = KettleState.Ready;
    }

    private void AnimateBoiling()
    {
        _renderer.sprite = _interactedSprite;
    }

    private void AnimateReady()
    {
        _renderer.sprite = _defaultSprite;
    }
}
