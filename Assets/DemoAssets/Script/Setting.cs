using System.Collections.Generic;
using UnityEngine;

public enum EKeyInputs
{
    Up, 
    Left,
    Down,
    Right,
    Interact
}

public static class Setting
{
    public static float MouseSensitivity = 3f;

    public static Dictionary<EKeyInputs, KeyCode> CurrentKeyValues = new Dictionary<EKeyInputs, KeyCode>();
    
}
