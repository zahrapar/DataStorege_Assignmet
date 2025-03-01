
using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class ProjectFactory
    {
  
        public static ProjectEntity? Create(ProjectForm? form) =>
            form == null ? null : new ProjectEntity
            {
                Title = form.Title,
                Description = form.Description,
                StartDate = form.StartDate,
                EndDate = form.EndDate,
                CustomerId = form.CustomerId,
                Status = (ProjectEntity.ProjectStatus)form.Status
            };

     
        public static Project? Create(ProjectEntity? entity) =>
            entity == null ? null : new Project
            {
                Id = entity.Id,
                ProjectNumber = entity.ProjectNumber,
                Title = entity.Title,
                Description = entity.Description,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                CustomerId = entity.CustomerId,
                Status = (int)entity.Status
            };
    }
}
