using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OurList : MonoBehaviour
{
    [SerializeField]
    private List<OurName> ourScripts;

    [ContextMenu("Выполнить все НАШИ скрипты")]
    public void ExecuteAllOurScripts()
    {
        UpdateOurScriptsList();
        foreach (var script in ourScripts)
        {
            script.Use();
        }
    }
    
    private void UpdateOurScriptsList()
    {
        ourScripts = FindObjectsOfType<OurName>().ToList();
    }
    
    private void Start()
    {
        UpdateOurScriptsList();
    }
}
