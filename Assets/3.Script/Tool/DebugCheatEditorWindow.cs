#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class DebugCheatEditorWindow : EditorWindow
{
    private int addGoldAmount = 1000;
    private bool godMode;

    [MenuItem("Tools/Debug Cheat Panel")]
    public static void Open()
    {
        GetWindow<DebugCheatEditorWindow>("Debug Cheat");
    }

    private void OnGUI()
    {
        GUILayout.Space(10);

        EditorGUILayout.LabelField("=== Runtime Info ===", EditorStyles.boldLabel);

        if (GameManager.Instance == null)
        {
            EditorGUILayout.HelpBox("GameManager가 없음", MessageType.Info);
            return;   
        }

        if (!GameManager.Instance.isPlayGame)
        {
            EditorGUILayout.HelpBox("Play 모드에서만 동작함", MessageType.Info);
            return;
        }

        DrawRuntimeInfo();

        GUILayout.Space(15);
        EditorGUILayout.LabelField("=== Cheat Actions ===", EditorStyles.boldLabel);

        DrawCheatButtons();
    }

    // =============================
    // Runtime Info
    // =============================

    void DrawRuntimeInfo()
    {
        int stage = GetCurrentStage();
        int gold = GetCurrentGold();

        EditorGUILayout.LabelField("Current Stage", stage.ToString());
        EditorGUILayout.LabelField("Current Gold", gold.ToString());
    }

    // =============================
    // Buttons
    // =============================

    void DrawCheatButtons()
    {
        addGoldAmount = EditorGUILayout.IntField("Add Gold Amount", addGoldAmount);

        if (GUILayout.Button("Add Gold"))
        {
            AddGold(addGoldAmount);
        }

        if (GUILayout.Button("Spawn Random Monster"))
        {
            SpawnRandomMonster();
        }

        if (GUILayout.Button("Go Next Stage"))
        {
            GoNextStage();
        }

        GUILayout.Space(10);

        bool newGod = EditorGUILayout.Toggle("God Mode", godMode);
        if (newGod != godMode)
        {
            godMode = newGod;
            SetGodMode(godMode);
        }
    }

    int GetCurrentStage()
    {
        return GameManager.Instance.stageData;
    }

    int GetCurrentGold()
    {
        return GameManager.Instance.Money;
    }

    void AddGold(int amount)
    {
        GameManager.Instance.Money += amount;
    }

    void SpawnRandomMonster()
    {
        GameManager.Instance.SpawnRendomStageFish();
    }

    void GoNextStage()
    {
        NextStage.Instance.LoadNextStage();
    }

    void SetGodMode(bool value)
    {
        GameManager.Instance.isNoDamage = value;
    }
}
#endif
