namespace Axxes.BullhornCRM.Attributes;

public class EntityAssociationAttribute : Attribute
{
    public string Name { get; }
    
    public EntityAssociationAttribute(string name)
    {
        Name = name;
    }
}