using System;

namespace SIA.Calificaciones.Service.EntitiesG
{
    
    public class Grade
    {
        public Guid Id { get; set;}

        public int asig_id { get; set;}

        public string Name { get; set;}

        public int student_id { get; set;}

        public float percen { get; set;}

        public float value { get; set;}
    }

}