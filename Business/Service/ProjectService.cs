using Business.Factories;
using Business.Helpers;
using Business.Models;
using Data.Contexts;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Service
{
    public class ProjectService
    {
        private readonly ProjectRepository _projectRepository;
        private readonly DataContext _context;

        public ProjectService(ProjectRepository projectRepository, DataContext context)
        {
            _projectRepository = projectRepository;
            _context = context;
        }

     
        public async Task CreateProjectAsync(ProjectForm form)
        {
            try
            {
                var projectNumber = await UniqueIdentifierGenerator.GenerateProjectNumberAsync(_context);
                var projectEntity = ProjectFactory.Create(form);
                projectEntity?.SetProjectNumber(projectNumber);

                if (projectEntity != null)
                {
                    await _projectRepository.AddAsync(projectEntity);
                    Console.WriteLine("Project created successfully.");
                }
                else
                {
                    Console.WriteLine("failed:invalid data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error  {ex.Message}");
            }
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            try
            {
                var projectEntities = await _projectRepository.GetAsync();
                return projectEntities.Select(ProjectFactory.Create);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error  {ex.Message}");
                return null;
            }
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            try
            {
                var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
                return ProjectFactory.Create(projectEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error  {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateProjectAsync(Project project)
        {
            try
            {
                var projectEntity = await _projectRepository.GetAsync(x => x.Id == project.Id);
                if (projectEntity != null)
                {
                    projectEntity.Title = project.Title;
                    projectEntity.Description = project.Description;
                    projectEntity.StartDate = project.StartDate;
                    projectEntity.EndDate = project.EndDate;
                    projectEntity.CustomerId = project.CustomerId;
                    projectEntity.Status = (ProjectEntity.ProjectStatus)project.Status;

                    await _projectRepository.UpdateAsync(projectEntity);
                    Console.WriteLine(" updated successfully.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                return false;
            }
        }
    }
}





































