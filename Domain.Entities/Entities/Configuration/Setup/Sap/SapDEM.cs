using Ardalis.GuardClauses;
using Domain.Commons;
using Domain.Markers;

namespace Domain.Entities.Entities.Configuration.Setup.Sap;

public class SapDEM : AuditableDEM, IAggregateRoot, IActivateable
{
    public string SapCode { get; private set; }
    public string DbVersion { get; private set; }
    public string DbPort { get; private set; }
    public string SldServer { get; private set; }
    public string ServerName { get; private set; }
    public string LicensePort { get; private set; }
    public string IpAddress { get; private set; }
    public string? Version { get; private set; }
    public string DbName { get; private set; }
    public string DbUser { get; private set; }
    public string DbPassword { get; private set; }
    public string SapUser { get; private set; }
    public string SapPassword { get; private set; }
    public bool Active { get; private set; }

    SapDEM() { }

    SapDEM(
        string sapCode,
        string dbVersion,
        string dbPort,
        string sldServer,
        string serverName,
        string licensePort,
        string ipAddress,
        string dbName,
        string dbUser,
        string dbPassword,
        string sapUser,
        string sapPassword,
        string? version = null,
        bool active = true)
    {
        SapCode = Guard.Against.NullOrEmpty(sapCode, nameof(sapCode), "SAP code cannot be null or empty");
        DbVersion = Guard.Against.NullOrEmpty(dbVersion, nameof(dbVersion), "DB version cannot be null or empty");
        DbPort = Guard.Against.NullOrEmpty(dbPort, nameof(dbPort), "DB port cannot be null or empty");
        SldServer = Guard.Against.NullOrEmpty(sldServer, nameof(sldServer), "SLD server cannot be null or empty");
        ServerName = Guard.Against.NullOrEmpty(serverName, nameof(serverName), "Server name cannot be null or empty");
        LicensePort = Guard.Against.NullOrEmpty(licensePort, nameof(licensePort), "License port cannot be null or empty");
        IpAddress = Guard.Against.NullOrEmpty(ipAddress, nameof(ipAddress), "IP address cannot be null or empty");
        DbName = Guard.Against.NullOrEmpty(dbName, nameof(dbName), "DB name cannot be null or empty");
        DbUser = Guard.Against.NullOrEmpty(dbUser, nameof(dbUser), "DB user cannot be null or empty");
        DbPassword = Guard.Against.NullOrEmpty(dbPassword, nameof(dbPassword), "DB password cannot be null or empty");
        SapUser = Guard.Against.NullOrEmpty(sapUser, nameof(sapUser), "SAP user cannot be null or empty");
        SapPassword = Guard.Against.NullOrEmpty(sapPassword, nameof(sapPassword), "SAP password cannot be null or empty");
        Version = version;
        Active = active;
    }

    public static SapDEM Create(
        string sapCode,
        string dbVersion,
        string dbPort,
        string sldServer,
        string serverName,
        string licensePort,
        string ipAddress,
        string dbName,
        string dbUser,
        string dbPassword,
        string sapUser,
        string sapPassword,
        string? version = null,
        bool active = true)
    {
        return new(
            sapCode,
            dbVersion,
            dbPort,
            sldServer,
            serverName,
            licensePort,
            ipAddress,
            dbName,
            dbUser,
            dbPassword,
            sapUser,
            sapPassword,
            version,
            active);
    }

    public SapDEM Update(
        string sapCode,
        string dbVersion,
        string dbPort,
        string sldServer,
        string serverName,
        string licensePort,
        string ipAddress,
        string dbName,
        string dbUser,
        string dbPassword,
        string sapUser,
        string sapPassword,
        string? version,
        bool active)
    {
        SapCode = Guard.Against.NullOrEmpty(sapCode, nameof(sapCode), "SAP code cannot be null or empty");
        DbVersion = Guard.Against.NullOrEmpty(dbVersion, nameof(dbVersion), "DB version cannot be null or empty");
        DbPort = Guard.Against.NullOrEmpty(dbPort, nameof(dbPort), "DB port cannot be null or empty");
        SldServer = Guard.Against.NullOrEmpty(sldServer, nameof(sldServer), "SLD server cannot be null or empty");
        ServerName = Guard.Against.NullOrEmpty(serverName, nameof(serverName), "Server name cannot be null or empty");
        LicensePort = Guard.Against.NullOrEmpty(licensePort, nameof(licensePort), "License port cannot be null or empty");
        IpAddress = Guard.Against.NullOrEmpty(ipAddress, nameof(ipAddress), "IP address cannot be null or empty");
        DbName = Guard.Against.NullOrEmpty(dbName, nameof(dbName), "DB name cannot be null or empty");
        DbUser = Guard.Against.NullOrEmpty(dbUser, nameof(dbUser), "DB user cannot be null or empty");
        DbPassword = Guard.Against.NullOrEmpty(dbPassword, nameof(dbPassword), "DB password cannot be null or empty");
        SapUser = Guard.Against.NullOrEmpty(sapUser, nameof(sapUser), "SAP user cannot be null or empty");
        SapPassword = Guard.Against.NullOrEmpty(sapPassword, nameof(sapPassword), "SAP password cannot be null or empty");
        Version = version;
        Active = active;

        return this;
    }

    public SapDEM Activate()
    {
        Active = true;
        return this;
    }

    public SapDEM Deactivate()
    {
        Active = false;
        return this;
    }
}
