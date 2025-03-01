
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    //public string ProjectNumber { get; set; } = null!;
    public int ProjectNumber { get; private set; }
    public string Title { get; set; } = null!;
    public string? Description{ get; set; } 

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

    public ProjectStatus Status { get; set; }


    //public int StatusId { get; set; }
    //public StatusTypeEntity Status { get; set; } = null!;

    //public int UserId { get; set; }
    //public UserEntity User { get; set; } = null!;

    //public int ProductId { get; set; }
    //public ProductEntity Product { get; set; } = null!;



    public void SetProjectNumber(int projectNumber)
    {
        if (ProjectNumber != 0)
        {
            throw new InvalidOperationException(" cannot be changed.");
        }

        ProjectNumber = projectNumber;
    }


    public enum ProjectStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3
    }

}



