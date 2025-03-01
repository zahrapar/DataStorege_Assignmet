using Business.Models;
using Business.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationConsoleApp
{
    public class MenuDialogs
    {
        private readonly CustomerService _customerService;
        private readonly ProjectService _projectService;

        public MenuDialogs(CustomerService customerService, ProjectService projectService)
        {
            _customerService = customerService;
            _projectService = projectService;
        }
        public async Task MenuOptions()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Create a customer");
                Console.WriteLine("2. Create project");
                Console.WriteLine("3. Edit project");
                Console.WriteLine("4. View projects");
                Console.WriteLine("5. Exit");

                var option = Console.ReadLine();

                await Task.Run(async () =>
                {
                    switch (option)
                    {
                        case "1":
                            await CreateCustomer();
                            break;

                        case "2":
                            await CreateProject();
                            break;

                        case "3":
                            await EditProject();
                            break;

                        case "4":
                            await ViewProjects();
                            Console.ReadLine();
                            break;

                        case "5":
                            QuitOption();
                            break;
                        default:
                            InvalidOption();
                            break;
                    }
                });
            }
        }


        private async Task CreateCustomer()
        {
            Console.WriteLine("Enter customer name:");
            var customerName = Console.ReadLine();

            var customerForm = new CustomerRegistrationForm { CustomerName = customerName };

            await _customerService.CreateCustomerAsync(customerForm);
        }



        private async Task CreateProject()
        {
            try
            {
                Console.WriteLine("Enter title of the project:");
                var title = Console.ReadLine();

                Console.WriteLine("Enter description for the project:");
                var description = Console.ReadLine();


                DateTime startDate;
                while (true)
                {
                    Console.WriteLine("Enter start date for project in this format yyyy-mm-dd:");
                    if (DateTime.TryParse(Console.ReadLine(), out startDate))
                        break;
                    else
                        Console.WriteLine("Invalid format. enter a valid format.");
                }

                DateTime endDate;
                while (true)
                {
                    Console.WriteLine("Enter end date for project in this format yyyy-mm-dd::");
                    if (DateTime.TryParse(Console.ReadLine(), out endDate))
                        break;
                    else
                        Console.WriteLine("Invalid format, please enter a valid  format.");
                }
                //Console.WriteLine("Enter start date for project yyyy-mm-dd:");
                //var startDate = DateTime.Parse(Console.ReadLine());

                //Console.WriteLine("Enter end date yyyy-mm-dd:");
                //var endDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter c ustomer ID for the project:");
                var customerId = int.Parse(Console.ReadLine());

                Console.WriteLine("choose status for the project (1-Pending, 2-In Progress, 3-Completed):");
                var statusId = int.Parse(Console.ReadLine());

                var projectForm = new ProjectForm
                {
                    Title = title,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    CustomerId = customerId,
                    Status = statusId
                };

                await _projectService.CreateProjectAsync(projectForm);
                Console.WriteLine("Project created successfuly");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid format, enter the correct value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }




        private async Task EditProject()
        {
            try
            {
                Console.WriteLine("Enter the Id of the project to edit:");
                if (!int.TryParse(Console.ReadLine(), out var pId))
                {
                    Console.WriteLine("Invalid,  enter a valid project Id.");
                    return;
                }


                var project = await _projectService.GetProjectByIdAsync(pId);

                if (project != null)
                {

                    Console.WriteLine($"Now updating this project: {project.Title}  , Enter new title for this project if you want:");
                    var title = Console.ReadLine();
                    if (!string.IsNullOrEmpty(title))
                        project.Title = title;

                    Console.WriteLine("Enter new description");
                    var description = Console.ReadLine();
                    if (!string.IsNullOrEmpty(description))
                        project.Description = description;

                    Console.WriteLine("Enter start date in this format :yyyy-mm-dd ");
                    var startDate = Console.ReadLine();
                    if (DateTime.TryParse(startDate, out var startDateN))
                        project.StartDate = startDateN;

                    Console.WriteLine("Enter end date in this format :yyyy-mm-dd");
                    var endDate = Console.ReadLine();
                    if (DateTime.TryParse(endDate, out var endDateN))
                        project.EndDate = endDateN;

                    Console.WriteLine("Enter new status id for project : 1 - Pending, 2 - In Progress, 3 - Completed");
                    var status = int.Parse(Console.ReadLine());
                    project.Status = status;

                    var newProject = await _projectService.UpdateProjectAsync(project);

                    if (newProject)
                    {
                        Console.WriteLine("edited successfully");
                    }
                    else
                    {
                        Console.WriteLine("Failed ");
                    }
                }
                else
                {
                    Console.WriteLine(" not found project");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }



        private async Task ViewProjects()
        {
            Console.WriteLine("All projects:");
            var projects = await _projectService.GetProjectsAsync();

            if (!projects.Any())
            {
                Console.WriteLine("No projects found");
                return;
            }

            Console.WriteLine("List of projects:");

            foreach (var project in projects)
            {
                var customer = await _customerService.GetCustomerByIdAsync(project.CustomerId);
                string customerName = customer.CustomerName;
                string status = project.Status switch
                {
                    1 => "Pending",
                    2 => "In Progress",
                    3 => "Completed"
                };

                Console.WriteLine($"Project Number: {project.ProjectNumber}\n" +
                  $"Title: {project.Title}\n" +
                  $"Description: {project.Description}\n" +
                  $"Start Date: {project.StartDate}\n" +
                  $"End Date: {project.EndDate}\n" +
                  $"Status: {status}\n" +
                  $"Customer: {customerName}\n" +
                  "----------------------------------");

        
            }

            //Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }




        private void QuitOption()
        {
            Console.Clear();
            Console.Write("Do you want to exit this application (y/n): ");
            var option = Console.ReadLine();

            if (option?.Equals("y", StringComparison.CurrentCultureIgnoreCase) == true)
            {
                Environment.Exit(0);
            }
        }



        private void InvalidOption()
        {
            Console.WriteLine("You must enter a valid option.");
            Console.ReadKey();
        }



    }
}

























