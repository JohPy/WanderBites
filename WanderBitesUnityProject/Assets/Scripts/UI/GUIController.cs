using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Collections;

public class GUIController : MonoBehaviour
{
    public GameObject recipeListBackground; // Parent GameObject for the recipe steps
    public GameObject recipeStepPrefab; // TextMeshPro prefab for each recipe step
    public Sprite recipeBackgroundTiny;
    public Sprite recipeBackgroundShort;
    public Sprite recipeBackgroundMedium;
    public Sprite recipeBackgroundLong;
    public Sprite recipeBackgroundExtraLong;
    public Sprite recipeBackgroundLongest;

    public Canvas endscreenCanvas;

    private RecipeData recipe;

    public void InitializeGUI(RecipeData recipeData)
    {
        endscreenCanvas.gameObject.SetActive(false);
        recipe = recipeData;
        PopulateRecipeGUI();

        StartCoroutine(ApplyInitialFormatting());
    }

    IEnumerator ApplyInitialFormatting()
    {
        yield return null; // wait one frame to ensure all UI elements are instantiated

        FormatActiveStep(recipe.activeStep);
        for (int i = 0; i < recipe.activeStep; i++)
        {
            FormatCompletedStep(i);
        }
    }

    public void OnUpdate()
    {
        if (recipe.activeStep >= recipe.steps.Count)
        {
            TriggerEndscreen();
        } else
        {
            FormatCompletedStep(recipe.activeStep-1);
            FormatActiveStep(recipe.activeStep);
        }
    }

    void PopulateRecipeGUI()
    {
        foreach (Transform child in recipeListBackground.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Currently max supported steps = 25

        if (recipe.steps.Count > 21)
        {
            recipeListBackground.GetComponent<RawImage>().texture = recipeBackgroundLongest.texture;
            recipeListBackground.GetComponent<RawImage>().SetNativeSize();
        } else if (recipe.steps.Count > 18)
        {
            recipeListBackground.GetComponent<RawImage>().texture = recipeBackgroundExtraLong.texture;
            recipeListBackground.GetComponent<RawImage>().SetNativeSize();
        } else if (recipe.steps.Count > 15)
        {
            recipeListBackground.GetComponent<RawImage>().texture = recipeBackgroundLong.texture;
            recipeListBackground.GetComponent<RawImage>().SetNativeSize();
        } else if (recipe.steps.Count > 12)
        {
            recipeListBackground.GetComponent<RawImage>().texture = recipeBackgroundMedium.texture;
            recipeListBackground.GetComponent<RawImage>().SetNativeSize();

        } else if (recipe.steps.Count > 9)
        {
            recipeListBackground.GetComponent<RawImage>().texture = recipeBackgroundShort.texture;
            recipeListBackground.GetComponent<RawImage>().SetNativeSize();
        } else if (recipe.steps.Count <= 8)
        {
            recipeListBackground.GetComponent<RawImage>().texture = recipeBackgroundTiny.texture;
            recipeListBackground.GetComponent<RawImage>().SetNativeSize();
        }

        GameObject recipeTitle = Instantiate(recipeStepPrefab, recipeListBackground.transform);
        TextMeshProUGUI titleText = recipeTitle.GetComponent<TextMeshProUGUI>();
        titleText.text = "Recipe Steps:";
        titleText.fontStyle = FontStyles.Underline;
        titleText.fontSize = 26;
        titleText.rectTransform.sizeDelta = new Vector2(titleText.rectTransform.sizeDelta.x, titleText.rectTransform.sizeDelta.y + 8);

        foreach (RecipeStep step in recipe.steps)
        {
            GameObject stepText = Instantiate(recipeStepPrefab, recipeListBackground.transform);

            TextMeshProUGUI text = stepText.GetComponent<TextMeshProUGUI>();
            text.text = step.uiText;
        }
    }

    void FormatActiveStep(int stepIndex)
    {
        int childIndex = stepIndex + 1; // +1 to account for title at index 0
        Transform stepText = recipeListBackground.transform.GetChild(childIndex);
        stepText.gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Bold;
        stepText.gameObject.GetComponent<TextMeshProUGUI>().color = Color.blueViolet;
    }

    void FormatCompletedStep(int stepIndex)
    {
        int childIndex = stepIndex + 1; // +1 to account for title at index 0

        Transform stepText = recipeListBackground.transform.GetChild(childIndex);
        stepText.gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
        stepText.gameObject.GetComponent<TextMeshProUGUI>().color = Color.black;
        stepText.gameObject.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
    }

    void TriggerEndscreen()
    {
        endscreenCanvas.gameObject.SetActive(true);
    }
}