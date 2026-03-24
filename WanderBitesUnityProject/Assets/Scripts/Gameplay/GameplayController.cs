using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;

public class GameplayController : MonoBehaviour
{
    private RecipeData recipe;

    private Dictionary<int, IInteractable> objectMap;

    private int activeStepIndex = 0;

    // Assign in Inspector
    [SerializeField]
    public TextAsset recipeJson;
    
    [SerializeField]
    public GameObject parentOfSceneObjects;

    [SerializeField]
    public GUIController guiController;

    [SerializeField]
    public AudioPlayer audioPlayer;

    void Awake()
    {
        LoadRecipe();
        BuildObjectMap();

        if (guiController != null)
        {
            guiController.InitializeGUI(recipe);
        }
        else
        {
            Debug.LogError("GUIController not assigned!");
        }
    }

    void Start()
    {
        audioPlayer.playSound();
        if (objectMap.TryGetValue(0, out var interactable))
        {
            interactable.EnableInteractionForCurrentStep();
        }
    }

    void LoadRecipe()
    {
        recipe = JsonUtility.FromJson<RecipeData>(recipeJson.text);
    }

    void BuildObjectMap()
    {
        objectMap = new Dictionary<int, IInteractable>();

        foreach (Transform child in parentOfSceneObjects.transform)
        {
            foreach (var interactable in child.GetComponents<IInteractable>())
            {
                objectMap[interactable.GetStep()] = interactable;
            }
        }
    }

    public RecipeStep GetCurrentStep()
    {
        return recipe.steps[activeStepIndex];
    }

    public void CompleteStep()
    {
        activeStepIndex++;

        guiController.OnUpdate(activeStepIndex); // Notify GUI of step change

        if (activeStepIndex >= recipe.steps.Count)
        {
            Debug.Log("Recipe Complete");
            return;
        }

        if (objectMap.TryGetValue(activeStepIndex, out var nextInteractable))
        {
            nextInteractable.EnableInteractionForCurrentStep();
        }
    }

    public void OnInteractionCompleted(IInteractable interactable)
    {
        Debug.Log($"Interaction completed for step {interactable.GetStep()}");
        CompleteStep();
    }
}
