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
using SIA.Calificaciones.Service.EntitiesH;


namespace SIA.Calificaciones.Service.ControllerH
{

    //Enables features for a better developer experience
    [ApiController]
    //Specifies the URL that this controller will map
    [Route("history")]
    public class HistsController : ControllerBase
    {

        private SqlConnection conn;

        //A variable for the DB
        private readonly GradesRepository gradesRepository = new();


        //Get all the asignatures
        [HttpGet]

        public async Task<IEnumerable<HistoryDto>> GetHisColAsync()
        {
            var his = (await gradesRepository.GetAllHAsync())
                        .Select(hisel => hisel.AsDtoH());
            return his;
        }


        //Get the asignatures by id
        [HttpGet("{id}")]

        public async Task<ActionResult<HistoryDto>> GetByHisAsync(int id)
        {
            var calif = await gradesRepository.GetHbyIdAsync(id);

            if (calif == null)
            {
                return NotFound();
            }

            return calif.AsDtoH();
        }

        //Create asignature
        [HttpPost]
        [ActionName(nameof(PostHisAsync))]

        public async Task<ActionResult<HistoryDto>> PostHisAsync(CreateHistoryDto createHistoryDto)
        {
            var asi = new History
            {
                id_historia = createHistoryDto.id_historia,
                student_id = createHistoryDto.student_id,
                id_programa = createHistoryDto.id_programa,
                porcentaje = createHistoryDto.porcentaje,
                asignaturaCursada = createHistoryDto.asignaturaCursada,
            };

            await gradesRepository.CreateHisAsync(asi);

            return CreatedAtAction(nameof(PostHisAsync), new { id = asi.id_historia }, asi);
        }


    }
}