using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    protected readonly DataContext _context =context;
    protected readonly DbSet<TEntity> _db= context.Set<TEntity>();

    //public BaseRepository(DataContext context)
    //{
    //    _context = context;
    //    _db = context.Set<TEntity>();
    //}

    public async Task AddAsync(TEntity entity)
    {
        await _db.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        var entities = await _db.ToListAsync();
        return entities;
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _db.FirstOrDefaultAsync(expression);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _db.Update(entity);
        await _context.SaveChangesAsync();
    }


    public async Task RemoveAsync(TEntity entity)
    {
        _db.Remove(entity);
        await _context.SaveChangesAsync();
    }

}