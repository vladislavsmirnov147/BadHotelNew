using System.Data;
using Microsoft.Data.SqlClient;

namespace BadHotel.Core;

public class SqlWorldStorage : IWorldStorage
{
    private readonly string _connectionString;

    public SqlWorldStorage(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task SaveAsync(string filePath, WorldDto world)
    {
        WorldMapper.FromDto(world);

        await using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        await using SqlTransaction transaction =
            (SqlTransaction)await connection.BeginTransactionAsync();

        try
        {
            await using (SqlCommand deleteWorkers = new(
                "DELETE FROM dbo.Workers",
                connection,
                transaction))
            {
                await deleteWorkers.ExecuteNonQueryAsync();
            }

            await using (SqlCommand deleteGroups = new(
                "DELETE FROM dbo.Groups",
                connection,
                transaction))
            {
                await deleteGroups.ExecuteNonQueryAsync();
            }

            Dictionary<string, int> groupIds = new();

            foreach (WorkerDto worker in world.Workers)
            {
                string groupName = worker.Group;

                if (string.IsNullOrWhiteSpace(groupName))
                {
                    throw new InvalidDataException(
                        "Töötaja grupp puudub.");
                }

                if (!groupIds.TryGetValue(groupName, out int groupId))
                {
                    await using SqlCommand insertGroup = new(
                        """
                        INSERT INTO dbo.Groups (Name)
                        OUTPUT INSERTED.Id
                        VALUES (@Name)
                        """,
                        connection,
                        transaction);

                    insertGroup.Parameters.Add(
                        "@Name", SqlDbType.NVarChar, 100).Value = groupName;

                    object? result = await insertGroup.ExecuteScalarAsync();

                    groupId = Convert.ToInt32(result);
                    groupIds.Add(groupName, groupId);
                }

                await using SqlCommand insertWorker = new(
                    """
                    INSERT INTO dbo.Workers
                        (Id, Type, Name, Energy, GroupId)
                    VALUES
                        (@Id, @Type, @Name, @Energy, @GroupId)
                    """,
                    connection,
                    transaction);

                insertWorker.Parameters.Add(
                    "@Id", SqlDbType.UniqueIdentifier).Value = worker.Id;

                insertWorker.Parameters.Add(
                    "@Type", SqlDbType.NVarChar, 50).Value = worker.Type;

                insertWorker.Parameters.Add(
                    "@Name", SqlDbType.NVarChar, 100).Value = worker.Name;

                insertWorker.Parameters.Add(
                    "@Energy", SqlDbType.Int).Value = worker.Energy;

                insertWorker.Parameters.Add(
                    "@GroupId", SqlDbType.Int).Value = groupId;

                await insertWorker.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<WorldDto> LoadAsync(string filePath)
    {
        WorldDto world = new();

        await using SqlConnection connection = new(_connectionString);
        await connection.OpenAsync();

        await using SqlCommand command = new(
            """
            SELECT
                w.Id,
                w.Type,
                w.Name,
                w.Energy,
                g.Name AS GroupName
            FROM dbo.Workers AS w
            INNER JOIN dbo.Groups AS g
                ON w.GroupId = g.Id
            ORDER BY w.Name, w.Id
            """,
            connection);

        await using SqlDataReader reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            WorkerDto worker = new()
            {
                Id = reader.GetGuid(0),
                Type = reader.GetString(1),
                Name = reader.GetString(2),
                Energy = reader.GetInt32(3),
                Group = reader.GetString(4)
            };

            world.Workers.Add(worker);
        }

        WorldMapper.FromDto(world);

        return world;
    }
}
