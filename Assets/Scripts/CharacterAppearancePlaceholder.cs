using UnityEngine;

/// <summary>
/// Reads the selected gender/name for the local player.
/// Replace this placeholder with your own boy/girl 3D character models and animations.
/// </summary>
public class CharacterAppearancePlaceholder : MonoBehaviour
{
    [SerializeField] private Renderer bodyRenderer;
    [SerializeField] private Color boyPlaceholderColor = new Color(0.18f, 0.42f, 0.78f);
    [SerializeField] private Color girlPlaceholderColor = new Color(0.78f, 0.28f, 0.48f);

    public string PlayerName { get; private set; }
    public bool IsBoy { get; private set; }

    private void Start()
    {
        IsBoy = PlayerPrefs.GetInt("CharacterGender", 0) == 0;
        PlayerName = PlayerPrefs.GetString("PlayerName", "খেলোয়াড়");

        if (bodyRenderer != null)
            bodyRenderer.material.color = IsBoy ? boyPlaceholderColor : girlPlaceholderColor;
    }
}
