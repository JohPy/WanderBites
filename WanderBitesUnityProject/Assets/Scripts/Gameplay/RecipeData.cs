using System.Collections.Generic;

[System.Serializable]
public class RecipeStep
{
    public int id;
    public string action;
    public string target;
    public string tool;
    public string source;
    public string uiText;
}

[System.Serializable]
public class RecipeData
{
    public string chapterId;
    public List<RecipeStep> steps;
}
