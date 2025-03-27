using UnityEngine;
using UnityEditor;

public class PlayerPrefsEditor : EditorWindow
{
    [MenuItem("Tools/PlayerPrefs Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PlayerPrefsEditor), false, "PlayerPrefs Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Management", EditorStyles.boldLabel);

        // ������ ��� ������� ���� ������
        if (GUILayout.Button("Clear All PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("��� ������ PlayerPrefs �������!");
        }

        GUILayout.Space(10);

        // ���� ��� �������� �� �����
        string keyToDelete = EditorGUILayout.TextField("Key to Delete", "");

        if (GUILayout.Button("Delete Key"))
        {
            if (PlayerPrefs.HasKey(keyToDelete))
            {
                PlayerPrefs.DeleteKey(keyToDelete);
                PlayerPrefs.Save();
                Debug.Log($"������ � ������ {keyToDelete} �������!");
            }
            else
            {
                Debug.Log($"���� {keyToDelete} �� ������.");
            }
        }
    }
}
