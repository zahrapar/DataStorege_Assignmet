using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contexts;

namespace Business.Helpers;

public static class UniqueIdentifierGenerator
{
    public static async Task<int> GenerateProjectNumberAsync(DataContext context)
    {
        var lProject = await context.Projects
            .OrderByDescending(p => p.ProjectNumber)
            .FirstOrDefaultAsync();

        return lProject == null ? 1 : lProject.ProjectNumber + 1;
    }
}



//public static class UniqueIdentifierGenerator
//{
//    public static string GenerateProjectNumber()
//    {
//        Random random = new Random();
//        return random.Next(1000, 9999).ToString();
//    }
//}