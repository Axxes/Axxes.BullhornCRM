namespace Axxes.BullhornCRM.Attributes;

internal class EntityNameAttribute : Attribute
{
    public string Name { get; }
    
    public EntityNameAttribute(string name)
    {
        Name = name;
    }
}