namespace Common.Data.Entities;

public class NodeTypeEntity : BaseEntity<int>
{
    public NodeTypeEntity()
    {
        Name = string.Empty;
        RegularExpression = string.Empty;
    }

    public string Name
    {
        get;
        set;
    }

    public string RegularExpression
    {
        get;
        set;
    }
}