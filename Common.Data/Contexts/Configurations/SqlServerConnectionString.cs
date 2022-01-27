namespace Common.Data.Contexts.Configurations;

using Common.Data.Contracts;

public class SqlServerConnectionString : IConnectionString
{
    public bool AllowAsynchronousProcessing
    {
        get;
    }

    public bool AllowMultipleActiveResultsSets
    {
        get;
    }

    public string Database
    {
        get;
    }

    public bool IsAlwaysEncrypted
    {
        get;
    }

    public bool IsTrustedConnection
    {
        get;
    }

    public int PacketSize
    {
        get;
    }

    public string Password
    {
        get;
    }

    public string Server
    {
        get;
    }

    public string? ServerInstanceName
    {
        get;
    }

    public int? ServerPortNumber
    {
        get;
    }

    public string UserId
    {
        get;
    }

    public SqlServerConnectionString(
        string database,
        string server,
        string userId,
        string password,
        bool allowAsynchronousProcessing = true,
        bool allowMultipleActiveResultsSets = true,
        bool isAlwaysEncrypted = true,
        bool isTrustedConnection = true,
        int packetSize = 4096,
        string? serverInstanceName = null,
        int? serverPortNumber = null)
    {
        AllowAsynchronousProcessing = allowAsynchronousProcessing;
        AllowMultipleActiveResultsSets = allowMultipleActiveResultsSets;
        Database = database;
        IsAlwaysEncrypted = isAlwaysEncrypted;
        IsTrustedConnection = isTrustedConnection;
        PacketSize = packetSize;
        Password = password;
        Server = server;
        ServerInstanceName = serverInstanceName;
        ServerPortNumber = serverPortNumber;
        UserId = userId;
    }

    private string GetColumnEncryptionSetting()
    {
        string settingValue = IsAlwaysEncrypted
            ? "enabled"
            : "disabled";

        return $"Column Encryption Setting={settingValue}";
    }

    private string GetServerString()
        => GetServerPortString(
            GetServerInstanceString(
                $"Server={Server}"));

    private string GetServerInstanceString(
        string serverConfiguration)
        => string.IsNullOrWhiteSpace(ServerInstanceName)
            ? serverConfiguration
            : $"{serverConfiguration}\\{ServerInstanceName}";

    private string GetServerPortString(
        string serverConfiguration)
        => ServerPortNumber is null
            ? serverConfiguration
            : $"{serverConfiguration},{ServerPortNumber}";

    public string GetConnectionString()
        => string.Join(
            ";",
            GetServerString(),
            $"Database={Database}",
            $"UserId={UserId}",
            $"Password={Password}",
            $"Trusted_Connection={IsTrustedConnection}",
            $"MultipleActiveResultSets={AllowMultipleActiveResultsSets}",
            $"Asynchronous Processing={AllowAsynchronousProcessing}",
            $"Packet Size={PacketSize}",
            GetColumnEncryptionSetting());
}