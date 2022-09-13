namespace SIA.Calificaciones.Service.EntitiesA
{
    public class Asignature
    {
        public int Id { get; set;}

        public decimal creditos { get; set;}

        public string tipo { get; set;}

        public string periodo { get; set;}

        public bool consolidada { get; set;}

        public List<string> notas {get; set;}
    }
}