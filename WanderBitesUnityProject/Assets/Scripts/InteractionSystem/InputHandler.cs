using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        #if UNITY_EDITOR
        Debug.Log(rayHit.collider.gameObject.name);
        #endif

        var interactable = rayHit.collider.GetComponent<ClickableObject>();
        if (interactable == null) return;
        interactable.Interact();
    }
}