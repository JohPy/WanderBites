using UnityEngine;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;
using Unity.VisualScripting;

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
    public GUIController guiController;

    // UI Elements

    public GameObject recipeList;
    public GameObject recipeStep;
    public Sprite smallList;
    public Sprite mediumList;
    public Sprite longList;
    public Sprite extraLongList;
    public Sprite veeeeryLongList;
    public Sprite longestList;
    
    void Awake()
    {
        LoadRecipe();
        BuildObjectMap();
        this.guiController = new GUIController(this);
        PopulateRecipeUI();
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

    public RecipeData GetRecipe()
    {
        return recipe;
    }

    public RecipeStep GetCurrentStep()
    {
        return recipe.steps[activeStepIndex];
    }

    public void CompleteStep()
    {
        FormatCompletedStep(GetCurrentStep().id);

        activeStepIndex++;

        FormatActiveStep(GetCurrentStep().id);

        if (activeStepIndex >= recipe.steps.Count)
        {
            Debug.Log("Recipe Complete");
        }
    }

    public void PopulateRecipeUI()
    {
        //GameObject text = Instantiate(recipeStep, recipeList.transform);
        //text.GetComponentAtIndex<TextMeshProUGUI>(2).text = "Test";

        foreach (Transform child in recipeList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Currently max supported steps = 25

        if (recipe.steps.Count > 21)
        {
            recipeList.GetComponent<RawImage>().texture = longestList.texture;
            recipeList.GetComponent<RawImage>().SetNativeSize();
        } else if (recipe.steps.Count > 18)
        {
            recipeList.GetComponent<RawImage>().texture = veeeeryLongList.texture;
            recipeList.GetComponent<RawImage>().SetNativeSize();
        } else if (recipe.steps.Count > 15)
        {
            recipeList.GetComponent<RawImage>().texture = extraLongList.texture;
            recipeList.GetComponent<RawImage>().SetNativeSize();
        } else if (recipe.steps.Count > 12)
        {
            recipeList.GetComponent<RawImage>().texture = longList.texture;
            recipeList.GetComponent<RawImage>().SetNativeSize();

        } else if (recipe.steps.Count > 9)
        {
            recipeList.GetComponent<RawImage>().texture = mediumList.texture;
            recipeList.GetComponent<RawImage>().SetNativeSize();
        } else if (recipe.steps.Count <= 8)
        {
            recipeList.GetComponent<RawImage>().texture = smallList.texture;
            recipeList.GetComponent<RawImage>().SetNativeSize();
        }

        GameObject recipeTitle = Instantiate(recipeStep, recipeList.transform);
        TextMeshProUGUI titleText = recipeTitle.GetComponent<TextMeshProUGUI>();
        titleText.text = "Recipe Steps:";
        titleText.fontStyle = FontStyles.Underline;
        titleText.fontSize = 26;
        titleText.rectTransform.sizeDelta = new Vector2(titleText.rectTransform.sizeDelta.x, titleText.rectTransform.sizeDelta.y + 8);

        foreach (RecipeStep step in recipe.steps)
        {
            GameObject stepText = Instantiate(recipeStep, recipeList.transform);

            TextMeshProUGUI text = stepText.GetComponent<TextMeshProUGUI>();
            text.text = step.uiText;
        }
    }

    public void FormatActiveStep(int stepIndex)
    {
        Transform stepText = recipeList.transform.GetChild(stepIndex);
        stepText.gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        stepText.gameObject.GetComponent<TextMeshProUGUI>().color = Color.blueViolet;
    }
    public void FormatCompletedStep(int stepIndex)
    {
        Transform stepText = recipeList.transform.GetChild(stepIndex);
        stepText.gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        stepText.gameObject.GetComponent<TextMeshProUGUI>().color = Color.grey;
        stepText.gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
        //stepText.gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
    }

    public void Update()
    {
        FormatActiveStep(3);
        FormatCompletedStep(2);
    }
}
