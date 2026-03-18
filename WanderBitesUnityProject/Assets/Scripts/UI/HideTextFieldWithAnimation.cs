using UnityEngine;
using TMPro;

public class HideTextFieldWithAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI textToReset;

    public void Hide()
    {
        animator.ResetTrigger("HideButtonClicked");
        animator.SetTrigger("HideButtonClicked");
    }

    public void OnHidden()
    {
        // Textfeld resetten
        if (textToReset != null)
            textToReset.text = string.Empty;

        // Nodes ausblenden
        if (root != null)
            root.SetActive(false);
    }
}
