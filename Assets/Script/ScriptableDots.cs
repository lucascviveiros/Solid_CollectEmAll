using UnityEngine;

[CreateAssetMenu(fileName = "New Dot", menuName = "Dot/Create New Dot")]
public class ScriptableDots : ScriptableObject
{
    public DotTypes dotType;
    public Color materialColor;
}

