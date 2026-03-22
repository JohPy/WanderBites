using UnityEngine;

public class GUIController : MonoBehaviour
{
    public GameObject recipeList;
    public GameObject recipeStep;
    public Sprite smallList;
    public Sprite mediumList;
    public Sprite longList;
    public Sprite extraLongList;
    public Sprite veeeeryLongList;
    public Sprite longestList;

    private GameplayController gameplayController;

    public GUIController(GameplayController gameplayController)
    {
        this.gameplayController = gameplayController;
    }

    
}