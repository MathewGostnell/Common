namespace Common.Data.Contexts.Factories;

public class CommonDbContextFactory : BaseDbContextFactory<CommonDbContext>
{
    public override string ConnectionStringName => "CommonConnection";
}