using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item_SO")]
public class SO_Item : ScriptableObject 
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _smallIcon; // inventory
    [SerializeField] private Sprite _bigIcon;
    [SerializeField] private string _description;
    [SerializeField] private ItemType _type;
    [SerializeField] private int _adCost;

    public string Name => _name;
    public Sprite SmallIcon => _smallIcon;
    public Sprite BigIcon => _bigIcon;
    public string Description => _description;
    public ItemType Type => _type;
    public int AdCost => _adCost;
}
