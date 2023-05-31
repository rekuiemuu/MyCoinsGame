using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class AssetItem : ScriptableObject, IInventoryItem
{
    [SerializeField] private string _name;
    [SerializeField] private Texture2D _uiIcon;

    public string Name => _name;

    public Texture2D UIIcon => _uiIcon;
}

public interface IInventoryItem
{
    string Name { get; }
    Texture2D UIIcon { get; }
}
