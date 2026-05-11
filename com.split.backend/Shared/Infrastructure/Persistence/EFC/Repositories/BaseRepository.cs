using System.Reflection.Metadata;
using com.split.backend.Shared.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;


/// <summary>
///     Base repository for all repositories
/// </summary>
/// <remarks>
///     This class is used to define the basic CRUD operations for all repositories.
///     This class implements the IBaseRepository interface.
/// </remarks>
/// <typeparam name="TEntity">
///     The entity type for the repository
/// </typeparam>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{ 
    /// <summary>
    ///     Default constructor for the base repository
    /// </summary>
    protected readonly AppDbContext Context;

    protected BaseRepository(AppDbContext context)
    {
        this.Context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    // inheritedDoc
    public async Task<TEntity?> FindByIdAsync(int id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    //inheritedDoc
    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    // inheritedDoc
    public void Remove(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    // inheritedDoc
    public async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }
}