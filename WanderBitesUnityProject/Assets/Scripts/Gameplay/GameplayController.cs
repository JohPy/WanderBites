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

    private Dictionary<string, GameObject> objectMap;

    void Awake()
    {
        LoadRecipe();
        BuildObjectMap();
    }

    void LoadRecipe()
    {
        recipe = JsonUtility.FromJson<RecipeData>(recipeJson.text);
    }

    void BuildObjectMap()
    {
        objectMap = new Dictionary<string, GameObject>();

        foreach (Transform child in parentOfSceneObjects.transform)
        {
            GameObject obj = child.gameObject;
            objectMap[obj.name.ToLower()] = obj;
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
        }
    }
}
