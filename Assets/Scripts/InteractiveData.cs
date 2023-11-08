using UnityEngine;

[CreateAssetMenu(menuName = "InteractiveData", fileName = "InteractiveData")]
public class InteractiveData : ScriptableObject
{
    public enum Type { Pickable, InteractOnce, InteractMulti, Indirect };

    public Type                 type;
    public bool                 startsOn = true;
    public string               inventoryName;
    public Sprite               inventoryIcon;
    public InteractiveData[]    requirements;
    public string               requirementsMessage;
    public string[]             interactionMessages;
}
