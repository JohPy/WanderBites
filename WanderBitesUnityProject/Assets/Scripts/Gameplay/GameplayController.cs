using UnityEngine;
using System.Collections.Generic;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    public TextAsset recipeJson;

    private RecipeData recipe;
    private int activeStepIndex = 0;

    // Assign in Inspector
    [SerializeField]
    public GameObject parentOfSceneObjects;

    private Dictionary<int, IInteractable> objectMap;

    void Awake()
    {
        LoadRecipe();
        BuildObjectMap();
    }

    void Start()
    {
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
