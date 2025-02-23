﻿
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context)
{
    private readonly DataContext _context = context;






    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

        if (entity != null)
        {
            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }





    //public async Task<bool> DeleteAsync(int id)
    //{
    //    var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
    //    if (entity == null)
    //        return false;

    //    _context.Customers.Remove(entity);
    //    await _context.SaveChangesAsync();

    //    return true;
    //}











}
