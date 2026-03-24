using UnityEngine;

public class PersistenceUtil
{
    public static void SaveRecipe(RecipeData recipe, string filePath)
    {
        string json = JsonUtility.ToJson(recipe, true);
        System.IO.File.WriteAllText(filePath, json);
        Debug.Log("Saved recipe to: " + filePath);
    }

    public static RecipeData LoadRecipe(string filePath)
    {
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            Debug.Log("Loaded recipe from: " + filePath);
            return JsonUtility.FromJson<RecipeData>(json);
        }
        else
        {
            Debug.LogWarning("No saved recipe found at: " + filePath);
            return null;
        }
    }
}