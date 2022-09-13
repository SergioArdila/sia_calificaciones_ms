namespace SIA.Calificaciones.Service.EntitiesH
{
    public class History
    {
        public int student_id { get; set; }

        public int id_historia { get; set; }

        public int id_programa { get; set; }

        public float porcentaje { get; set; }

        public List<int> asignaturaCursada {get; set;}
    }
}