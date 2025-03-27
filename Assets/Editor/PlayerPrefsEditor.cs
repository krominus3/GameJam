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

        // Кнопка для очистки всех данных
        if (GUILayout.Button("Clear All PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("Все данные PlayerPrefs удалены!");
        }

        GUILayout.Space(10);

        // Поле для удаления по ключу
        string keyToDelete = EditorGUILayout.TextField("Key to Delete", "");

        if (GUILayout.Button("Delete Key"))
        {
            if (PlayerPrefs.HasKey(keyToDelete))
            {
                PlayerPrefs.DeleteKey(keyToDelete);
                PlayerPrefs.Save();
                Debug.Log($"Данные с ключом {keyToDelete} удалены!");
            }
            else
            {
                Debug.Log($"Ключ {keyToDelete} не найден.");
            }
        }
    }
}
