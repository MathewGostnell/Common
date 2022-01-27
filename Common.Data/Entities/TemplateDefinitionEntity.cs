namespace Common.Data.Entities;

public class TemplateDefinitionEntity : BaseEntity<int>
{
    public int TemplateId
    {
        get;
        set;
    }

    public int NodeId
    {
        get;
        set;
    }

    public virtual TemplateEntity? Template
    {
        get;
        set;
    }

    public virtual NodeEntity? Node
    {
        get;
        set;
    }
}