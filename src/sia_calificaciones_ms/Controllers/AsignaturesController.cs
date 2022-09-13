using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using Sia.Calificaciones.Service.Dtos;
using SIA.Calificaciones.Service.Repositories;
using SIA.Calificaciones.Service.EntitiesG;
using SIA.Calificaciones.Service.EntitiesA;


namespace SIA.Calificaciones.Service.ControllerA
{

    //Enables features for a better developer experience
    [ApiController]
    //Specifies the URL that this controller will map
    [Route("asignatures")]
    public class AsigsController : ControllerBase
    {

        private SqlConnection conn;

        //A variable for the DB
        private readonly GradesRepository gradesRepository = new();


        //Get all the asignatures
        [HttpGet]

        public async Task<IEnumerable<AsignatureDto>> GetAsColAsync()
        {
            var asign = (await gradesRepository.GetAllAsigAsync())
                        .Select(gradi => gradi.AsDtoA());
            return asign;
        }


        //Get the asignatures by id
        [HttpGet("{id}")]

        public async Task<ActionResult<AsignatureDto>> GetByAsAsync(int id)
        {
            var calif = await gradesRepository.GetAsignatureColAsync(id);

            if (calif == null)
            {
                return NotFound();
            }

            return calif.AsDtoA();
        }

        //Create asignature
        [HttpPost]
        [ActionName(nameof(PostAsigAsync))]
        public async Task<ActionResult<AsignatureDto>> PostAsigAsync(CreateAsignatureDto createAsignatureDto)
        {
            var asi = new Asignature
            {
                Id = createAsignatureDto.Id,
                creditos = createAsignatureDto.creditos,
                tipo = createAsignatureDto.tipo,
                periodo = createAsignatureDto.periodo,
                consolidada = createAsignatureDto.consolidada,
                notas = createAsignatureDto.notas
            };

            await gradesRepository.CreateAsigAsync(asi);

            return CreatedAtAction(nameof(PostAsigAsync), new { id = asi.Id }, asi);
        }


    }
}