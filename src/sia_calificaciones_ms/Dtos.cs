using System;
using System.ComponentModel.DataAnnotations;

namespace Sia.Calificaciones.Service.Dtos
{

    public record GradeDto(Guid Id, int asig_id, string Name, int student_id, float percen, float value);

    //Create a grade
    public record CreateGradeDto([Required] int asig_id, string Name, [Required] int student_id, [Range(0, 1)] float percen, [Range(0, 5)] float value);

    //Update a grade
    public record UpdateGradeDto([Required] int asig_id, string Name, [Required] int student_id, [Range(0, 1)] float percen, [Range(0, 5)] float value);

    
    public record AsignatureDto(int Id, decimal creditos, string tipo, string periodo, bool consolidada, List<string> notas);

    //Create a asignature
    public record CreateAsignatureDto(int Id, decimal creditos, string tipo, string periodo, bool consolidada, List<string> notas);

    //Update a asignature
    public record UpdateAsignatureDto([Range(0, 1)] float percen, [Range(0, 5)] float value);

    public record HistoryDto(int student_id, int id_historia, int id_programa, float porcentaje, List<int> asignaturaCursada);
    
    //Create a history
    public record CreateHistoryDto([Required] int student_id, int id_historia, int id_programa, float porcentaje, List<int> asignaturaCursada);

    //Update a history
    public record UpdateHistoryDto(List<int> asignaturaCursada);

}