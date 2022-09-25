namespace Axxes.BullhornCRM.Attributes;

internal class EntityHistoryNameAttribute : Attribute
{
    public string Name { get; }
    
    public EntityHistoryNameAttribute(string name)
    {
        Name = name;
    }
}