
using BadHotel.Core;
using Microsoft.EntityFrameworkCore;

namespace BadHotel.Core.Persistence;

public class EfWorldStorage : IWorldStorage
{
    private readonly DbContextOptions<HotelDbContext> _options;

    public EfWorldStorage(DbContextOptions<HotelDbContext> options)
    {
        _options = options;
    }

    public async Task SaveAsync(string filePath, WorldDto world)
    {
        ArgumentNullException.ThrowIfNull(world);

        await using var context = new HotelDbContext(_options);
        await using var transaction =
            await context.Database.BeginTransactionAsync();

        await context.Workers.ExecuteDeleteAsync();
        await context.Groups.ExecuteDeleteAsync();

        var groups = world.Workers
            .Select(worker => worker.Group)
            .Distinct()
            .Select(name => new GroupEntity { Name = name })
            .ToList();

        context.Groups.AddRange(groups);
        await context.SaveChangesAsync();

        var groupIds = groups.ToDictionary(
            group => group.Name,
            group => group.Id);

        List<WorkerEntity> workers = world.Workers
            .Select(worker =>
            {
                WorkerEntity entity = worker.Type switch
                {
                    nameof(Receptionist) => new ReceptionistEntity(),
                    nameof(Cleaner) => new CleanerEntity(),
                    nameof(Cook) => new CookEntity(),
                    _ => throw new ArgumentException(
                        $"Tundmatu töötaja tüüp: {worker.Type}")
                };

                entity.Id = worker.Id;
                entity.Name = worker.Name;
                entity.Energy = worker.Energy;
                entity.GroupId = groupIds[worker.Group];

                return entity;
            })
            .ToList();

        context.Workers.AddRange(workers);

        await context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task<WorldDto> LoadAsync(string filePath)
    {
        await using var context = new HotelDbContext(_options);

        var workers = await context.Workers
            .AsNoTracking()
            .Include(worker => worker.Group)
            .ToListAsync();

        return new WorldDto
        {
            Workers = workers.Select(worker => new WorkerDto
            {
                Id = worker.Id,
                Type = worker.Type,
                Name = worker.Name,
                Energy = worker.Energy,
                Group = worker.Group.Name
            }).ToList()
        };
    }
}
