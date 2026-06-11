using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private List<GameObject> panels;
    [SerializeField] private string startPanelName = "RegisterPanel"; 

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializePanels();
    }

    void Start()
    {
        
        InitializePanels();
    }

    private void InitializePanels()
    {
        
        if (panels == null || panels.Count == 0)
        {
            panels = new System.Collections.Generic.List<GameObject>();
            string[] defaultNames = new string[] { "LoginPanel", "RegisterPanel", "ServerListPanel", "ChatPanel", "ProfilePanel" };
            foreach (var name in defaultNames)
            {
                var go = GameObject.Find(name);
                if (go != null)
                    panels.Add(go);
            }
        }

        if (panels == null || panels.Count == 0)
        {
            Debug.LogWarning("UIManager: no se encontraron panels en la lista ni por nombre en la escena.");
            return;
        }

        
        string found = string.Join(", ", panels.ConvertAll(p => p != null ? p.name + (p.activeSelf ? "(active)" : "(inactive)") : "null"));
        Debug.Log("UIManager: panels detectados: " + found + ", startPanelName=" + startPanelName);

        
        HideAllPanels();
        GameObject start = panels.Find(p => p != null && p.name == startPanelName);
        if (start != null)
            start.SetActive(true);
        else
            panels[0].SetActive(true);

        
        var panelSet = new System.Collections.Generic.HashSet<GameObject>(panels);
        var allTransforms = GameObject.FindObjectsOfType<Transform>();
        foreach (var t in allTransforms)
        {
            var go = t.gameObject;
            if (go == null) continue;
            
            if (panelSet.Contains(go)) continue;
            
            if (go.name.IndexOf("Profile", System.StringComparison.OrdinalIgnoreCase) >= 0 && go.activeSelf)
            {
                go.SetActive(false);
                Debug.Log("UIManager: ocultando objeto relacionado con Profile encontrado: " + go.name);
            }
        }
    }

    public void ShowPanel(string panelName)
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(panel.name == panelName);
        }
    }

    public void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
