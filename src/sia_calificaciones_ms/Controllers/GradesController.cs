using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Extensions;
using Sia.Calificaciones.Service.Dtos;
using SIA.Calificaciones.Service.Repositories;
using SIA.Calificaciones.Service.EntitiesG;
using SIA.Calificaciones.Service.EntitiesA;


namespace SIA.Calificaciones.Service.ControllerG
{

    //Enables features for a better developer experience
    [ApiController]
    //Specifies the URL that this controller will map
    [Route("grades")]
    public class CalifsController : ControllerBase
    {
        //SQL configuration

        //A variable for the MongoDB
        private readonly GradesRepository gradesRepository = new();

        //Get all the grades
        [HttpGet]

        public async Task<IEnumerable<GradeDto>> GetAsync()
        {
            var grades = (await gradesRepository.GetAllAsync())
                        .Select(gradi => gradi.AsDto());
            return grades;
        }


        //Get the grades by id
        [HttpGet("id/{id}")]

        public async Task<ActionResult<GradeDto>> GetByIdAsync(Guid id)
        {
            var calif = await gradesRepository.GetAsync(id);

            if (calif == null)
            {
                return NotFound();
            }

            return calif.AsDto();
        }


        //Get the grades by student
        [HttpGet("student/{student}")]

        public async Task<IEnumerable<GradeDto>> GetByStudentAsync(int student)
        {
            var studenti = (await gradesRepository.GetStudentAsync(student))
                        .Select(std => std.AsDto());
            return studenti;
        }

        //Get the grades by asignature
        [HttpGet("asignature/{asignature}")]

        public async Task<IEnumerable<GradeDto>> GetByAsignatureAsync(int asignature)
        {
            var asiggrade = (await gradesRepository.GetAsignatureAsync(asignature))
                        .Select(asig => asig.AsDto());
            return asiggrade;
        }


        //Create a grade
        [HttpPost]
        [ActionName(nameof(PostAsync))]
        public async Task<ActionResult<GradeDto>> PostAsync(CreateGradeDto createGradeDto)
        {
            bool found = false;
            
            var calif = new Grade
            {
                asig_id = createGradeDto.asig_id,
                Name = createGradeDto.Name,
                student_id = createGradeDto.student_id,
                percen = createGradeDto.percen,
                value = createGradeDto.value
            };

            Asignature asig = await gradesRepository.GetAsignatureColAsync(createGradeDto.asig_id);


            await gradesRepository.CreateAsync(calif);
            string uid = Convert.ToString(calif.Id);

            asig.notas.Add(uid);

            await gradesRepository.UpdateAsigAsync(asig);

            return CreatedAtAction(nameof(PostAsync), new { id = calif.Id.ToString("N") }, calif);
            

            return NotFound();

        }

        //Update a grade
        [HttpPut("{id}")]

        public async Task<IActionResult> PutAsync(Guid id, UpdateGradeDto updateGradeDto)
        {
            var existing = await gradesRepository.GetAsync(id);

            if (existing == null)
            {
                return NotFound();
            }

            existing.asig_id = updateGradeDto.asig_id;
            existing.Name = updateGradeDto.Name;
            existing.student_id = updateGradeDto.student_id;
            existing.percen = updateGradeDto.percen;
            existing.value = updateGradeDto.value;



            await gradesRepository.UpdateAsync(existing);

            return NoContent();
        }

        //Delete a grade
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var calif = await gradesRepository.GetAsync(id);

            if (calif == null)
            {
                return NotFound();
            }

            Asignature asig = await gradesRepository.GetAsignatureColAsync(calif.asig_id);
            
            string uid = Convert.ToString(calif.Id);
            await gradesRepository.RemoveAsync(calif.Id);

            asig.notas.Remove(uid);

            await gradesRepository.UpdateAsigAsync(asig);

            return NoContent();
        }

    }
}