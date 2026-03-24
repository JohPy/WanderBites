using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem.Utilities;
using UnityEditor;

public class GameplayController : MonoBehaviour
{
    [SerializeField]
    public TextAsset recipeJson;

    private RecipeData recipe;

    // Assign in Inspector
    [SerializeField]
    public GameObject parentOfSceneObjects;
    public GUIController guiController;

    private Dictionary<int, IInteractable> objectMap;

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
        if (objectMap.TryGetValue(recipe.activeStep, out var interactable))
        {
            interactable.EnableInteractionForCurrentStep();
        }
    }

    void LoadRecipe()
    {
        string path = AssetDatabase.GetAssetPath(recipeJson);
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Recipe JSON file not found at path: " + path);
            return;
        }
        recipe = PersistenceUtil.LoadRecipe(path);
    }

    void SaveRecipe()
    {
        string path = AssetDatabase.GetAssetPath(recipeJson);
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Recipe JSON file not found at path: " + path);
            return;
        }
        PersistenceUtil.SaveRecipe(recipe, path);
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
        return recipe.steps[recipe.activeStep];
    }

    public void CompleteStep()
    {
        recipe.activeStep++;
        SaveRecipe();

        guiController.OnUpdate(); // Notify GUI of step change

        if (recipe.activeStep >= recipe.steps.Count)
        {
            recipe.isCompleted = true;
            SaveRecipe();
            Debug.Log("Recipe Complete");
            return;
        }

        if (objectMap.TryGetValue(recipe.activeStep, out var nextInteractable))
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
