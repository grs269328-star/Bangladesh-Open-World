using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// বাংলা ছেলে/মেয়ে চরিত্র বাছাই মেনু। আলাদা Start/Menu scene-এ একটি Empty GameObject-এ লাগান.
/// Play scene-এর নাম Inspector-এ দিন।
/// </summary>
public class CharacterSelectionMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Village";
    [SerializeField] private string defaultPlayerName = "খেলোয়াড়";

    private bool isBoy = true;
    private string playerName;

    private void Awake()
    {
        isBoy = PlayerPrefs.GetInt("CharacterGender", 0) == 0;
        playerName = PlayerPrefs.GetString("PlayerName", defaultPlayerName);
    }

    private void OnGUI()
    {
        float panelWidth = Mathf.Min(420, Screen.width - 32);
        float panelHeight = 300;
        Rect panel = new Rect((Screen.width - panelWidth) / 2,
            (Screen.height - panelHeight) / 2, panelWidth, panelHeight);

        GUILayout.BeginArea(panel, GUI.skin.box);
        GUILayout.Space(12);
        GUILayout.Label("বাংলাদেশ ওপেন ওয়ার্ল্ড");
        GUILayout.Label("তোমার চরিত্র বেছে নাও");
        GUILayout.Space(8);

        GUILayout.BeginHorizontal();
        if (GUILayout.Toggle(isBoy, "ছেলে", GUI.skin.button, GUILayout.Height(55)))
            isBoy = true;
        if (GUILayout.Toggle(!isBoy, "মেয়ে", GUI.skin.button, GUILayout.Height(55)))
            isBoy = false;
        GUILayout.EndHorizontal();

        GUILayout.Space(12);
        GUILayout.Label("তোমার নাম:");
        playerName = GUILayout.TextField(playerName ?? "", 18, GUILayout.Height(38));

        GUILayout.Space(18);
        if (GUILayout.Button("গ্রামে প্রবেশ করো", GUILayout.Height(52)))
        {
            PlayerPrefs.SetInt("CharacterGender", isBoy ? 0 : 1);
            PlayerPrefs.SetString("PlayerName",
                string.IsNullOrWhiteSpace(playerName) ? defaultPlayerName : playerName.Trim());
            PlayerPrefs.Save();
            SceneManager.LoadScene(gameSceneName);
        }

        GUILayout.EndArea();
    }
}
