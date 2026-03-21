using UnityEngine;

public enum InteractionState
{
    Idle, // this means the object is not ready to be interacted with as it is not the correct step in the recipe and hasn't been interacted with yet
    Ready, // this means the object is ready to be interacted with as it is the correct step in the recipe
    Interacting, // this means the object is currently being interacted with
    Completed // this means the object has been interacted with correctly and the interaction is complete
}
public interface IInteractable
{
    public bool EnableInteractionForCurrentStep();
    public bool CanInteract();
}