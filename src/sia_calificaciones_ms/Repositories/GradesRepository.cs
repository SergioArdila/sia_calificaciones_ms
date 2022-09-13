using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SIA.Calificaciones.Service.EntitiesG;
using SIA.Calificaciones.Service.EntitiesH;
using SIA.Calificaciones.Service.EntitiesA;



namespace SIA.Calificaciones.Service.Repositories
{
    public class GradesRepository
    {

        private const string collectionNameGrades = "grades";
        private const string collectionNameHistory = "history";
        private const string collectionNameAsignature = "asignatura";


        private readonly IMongoCollection<Grade> dbCollectionGrades;
        private readonly IMongoCollection<History> dbCollectionHistory;
        private readonly IMongoCollection<Asignature> dbCollectionAsignature;

        private readonly FilterDefinitionBuilder<Grade> filterBuilderGrade = Builders<Grade>.Filter;
        private readonly FilterDefinitionBuilder<Asignature> filterBuilderAsignature = Builders<Asignature>.Filter;
        private readonly FilterDefinitionBuilder<History> filterBuilderHistory = Builders<History>.Filter;

        public GradesRepository()
        {

            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("SIA_InfoAcademica_db");
            dbCollectionGrades = database.GetCollection<Grade>(collectionNameGrades);
            dbCollectionHistory = database.GetCollection<History>(collectionNameHistory);
            dbCollectionAsignature = database.GetCollection<Asignature>(collectionNameAsignature);
        }


        public async Task<IReadOnlyCollection<Grade>> GetAllAsync()
        {
            return await dbCollectionGrades.Find(filterBuilderGrade.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Asignature>> GetAllAsigAsync()
        {

            return await dbCollectionAsignature.Find(filterBuilderAsignature.Empty).ToListAsync();
        }

        public async Task<Grade> GetAsync(Guid id)
        {
            FilterDefinition<Grade> filter = filterBuilderGrade.Eq(entity => entity.Id, id);
            return await dbCollectionGrades.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Asignature> GetAsignatureColAsync(int asig_id)
        {
            FilterDefinition<Asignature> filter = filterBuilderAsignature.Eq(entity => entity.Id, asig_id);
            return await dbCollectionAsignature.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Grade>> GetStudentAsync(int student)
        {
            FilterDefinition<Grade> filter = filterBuilderGrade.Eq(entity => entity.student_id, student);
            return await dbCollectionGrades.Find(filter).ToListAsync();
        }

        public async Task<IReadOnlyCollection<Grade>> GetAsignatureAsync(int asig_id)
        {
            FilterDefinition<Grade> filter = filterBuilderGrade.Eq(entity => entity.asig_id, asig_id);
            return await dbCollectionGrades.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Grade entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollectionGrades.InsertOneAsync(entity);
        }

        public async Task CreateAsigAsync(Asignature entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollectionAsignature.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Grade entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<Grade> filterGrade = filterBuilderGrade.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollectionGrades.ReplaceOneAsync(filterGrade, entity);
        }

        public async Task UpdateAsigAsync(Asignature entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<Asignature> filterGrade = filterBuilderAsignature.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollectionAsignature.ReplaceOneAsync(filterGrade, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Grade> filter = filterBuilderGrade.Eq(entity => entity.Id, id);
            await dbCollectionGrades.DeleteOneAsync(filter);
        }


        public async Task<IReadOnlyCollection<History>> GetAllHAsync()
        {
            return await dbCollectionHistory.Find(filterBuilderHistory.Empty).ToListAsync();
        }

        public async Task<History> GetHbyIdAsync(int id)
        {
            FilterDefinition<History> filter = filterBuilderHistory.Eq(entity => entity.id_historia, id);
            return await dbCollectionHistory.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<History>> GetHbyStudentAsync(int student)
        {
            FilterDefinition<History> filter = filterBuilderHistory.Eq(entity => entity.student_id, student);
            return await dbCollectionHistory.Find(filter).ToListAsync();
        }

        public async Task CreateHisAsync(History entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollectionHistory.InsertOneAsync(entity);
        }
    }

}