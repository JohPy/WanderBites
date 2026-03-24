using System.Collections.Generic;

[System.Serializable]
public class RecipeStep
{
    public int id;
    public string action;
    public string target;
    public string source;
    public string uiText;
}

[System.Serializable]
public class RecipeData
{
    public string chapterId;
    public int activeStep;
    public bool isCompleted;
    public List<RecipeStep> steps;
}
