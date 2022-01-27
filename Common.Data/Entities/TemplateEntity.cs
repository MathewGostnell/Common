namespace Common.Data.Entities;

public class TemplateEntity : BaseEntity<int>
{
    public TemplateEntity()
    {
        Name = string.Empty;
        Summary = string.Empty;
        TemplateDefinitions = new HashSet<TemplateDefinitionEntity>();
    }

    public string Name
    {
        get;
        set;
    }

    public string Summary
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