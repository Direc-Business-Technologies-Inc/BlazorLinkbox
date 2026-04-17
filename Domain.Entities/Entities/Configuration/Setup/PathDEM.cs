using Ardalis.GuardClauses;
using Domain.Commons;
using Domain.Markers;

namespace Domain.Entities.Configuration.Setup;

public class PathDEM : AuditableDEM, IAggregateRoot, IActivateable
{
    public string PathCode { get; private set; }
    public string LocalPath { get; private set; }
    public string FileSearchOption { get; private set; }
    public string? BackupPath { get; private set; }
    public string? ErrorPath { get; private set; }
    public string? RemotePath { get; private set; }
    public string? RemoteServer { get; private set; }
    public string? RemoteIpAddress { get; private set; }
    public string? RemotePort { get; private set; }
    public string? RemoteUserId { get; private set; }
    public string? RemotePassword { get; private set; }
    public bool Active { get; private set; }

    PathDEM() { }

    PathDEM(
        string pathCode,
        string localPath,
        string fileSearchOption,
        string backupPath,
        string errorPath,
        string remotePath,
        string remoteServer,
        string remoteIpAddress,
        string remotePort,
        string remoteUserId,
        string remotePassword,
        bool active = true)
    {
        PathCode = Guard.Against.NullOrEmpty(pathCode, nameof(pathCode), "Path code cannot be null or empty");
        LocalPath = Guard.Against.NullOrEmpty(localPath, nameof(localPath), "Local path cannot be null or empty");
        FileSearchOption = Guard.Against.NullOrEmpty(fileSearchOption, nameof(fileSearchOption), "File search option cannot be null or empty");
        BackupPath = backupPath;
        ErrorPath = errorPath;
        RemotePath = remotePath;
        RemoteServer = remoteServer;
        RemoteIpAddress = remoteIpAddress;
        RemotePort = remotePort;
        RemoteUserId = remoteUserId;
        RemotePassword = remotePassword;
        Active = active;
    }

    public static PathDEM Create(
        string pathCode,
        string localPath,
        string fileSearchOption,
        string backupPath,
        string errorPath,
        string remotePath,
        string remoteServer,
        string remoteIpAddress,
        string remotePort,
        string remoteUserId,
        string remotePassword,
        bool active = true)
    {
        return new(
            pathCode,
            localPath,
            fileSearchOption,
            backupPath,
            errorPath,
            remotePath,
            remoteServer,
            remoteIpAddress,
            remotePort,
            remoteUserId,
            remotePassword,
            active);
    }

    public PathDEM Update(
        string pathCode,
        string localPath,
        string fileSearchOption,
        string backupPath,
        string errorPath,
        string remotePath,
        string remoteServer,
        string remoteIpAddress,
        string remotePort,
        string remoteUserId,
        string remotePassword,
        bool active)
    {
        PathCode = Guard.Against.NullOrEmpty(pathCode, nameof(pathCode), "Path code cannot be null or empty");
        LocalPath = Guard.Against.NullOrEmpty(localPath, nameof(localPath), "Local path cannot be null or empty");
        FileSearchOption = Guard.Against.NullOrEmpty(fileSearchOption, nameof(fileSearchOption), "File search option cannot be null or empty");
        BackupPath = backupPath;
        ErrorPath = errorPath;
        RemotePath = remotePath;
        RemoteServer = remoteServer;
        RemoteIpAddress = remoteIpAddress;
        RemotePort = remotePort;
        RemoteUserId = remoteUserId;
        RemotePassword = remotePassword;
        Active = active;

        return this;
    }

    public PathDEM Activate()
    {
        Active = true;
        return this;
    }

    public PathDEM Deactivate()
    {
        Active = false;
        return this;
    }
}
