using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Diets;
using Domain.Entities.Exercises;

namespace Domain.Entities.ExerciseDocuments;

public class ExerciseDocument
{
     [ForeignKey("ExerciseId")]
     public int? ExerciseId { get; set; }
     public Exercise? Exercise { get; set; }
    public string Document { get; set; }
}