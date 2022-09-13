using Sia.Calificaciones.Service.Dtos;
using SIA.Calificaciones.Service.EntitiesG;
using SIA.Calificaciones.Service.EntitiesA;
using SIA.Calificaciones.Service.EntitiesH;

namespace Extensions
{
    public static class ExtensionsClass
    {
        public static GradeDto AsDto(this Grade grade)
        {
            return new GradeDto(grade.Id, grade.asig_id, grade.Name, grade.student_id, grade.percen, grade.value);
        }

        public static AsignatureDto AsDtoA(this Asignature asignature)
        {
            return new AsignatureDto(asignature.Id, asignature.creditos, asignature.tipo, asignature.periodo, asignature.consolidada, asignature.notas);
        }

        public static HistoryDto AsDtoH(this History history)
        {
            return new HistoryDto(history.student_id, history.id_historia, history.id_programa, history.porcentaje, history.asignaturaCursada);
        }
    }
}