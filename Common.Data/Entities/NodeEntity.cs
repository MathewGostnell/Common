namespace Common.Data.Entities;

public class NodeEntity : BaseEntity<int>
{
    public NodeEntity()
    {
        CodeName = string.Empty;
        DisplayName = string.Empty;
        TemplateDefinitions = new HashSet<TemplateDefinitionEntity>();
    }

    public string CodeName
    {
        get;
        set;
    }

    public string DisplayName
    {
        get;
        set;
    }

    public virtual ICollection<TemplateDefinitionEntity> TemplateDefinitions
    {
        get;
        set;
    }
}