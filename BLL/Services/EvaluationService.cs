using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EvaluationService : Service, IService<Evaluation, EvaluationModel>
    {
        public EvaluationService(Db db) : base(db)
        {
        }
        public IQueryable<EvaluationModel> Query()
        {
            return _db.Evaluations
                .Include(e => e.User)
                .Include(e => e.EvaluatedEvaluations)
                .ThenInclude(ee => ee.Evaluated)
                .OrderByDescending(e => e.Date)
                .ThenBy(e => e.Title)
                .Select(e => new EvaluationModel
                {
                    Record = e
                });
        }
        public Service Create(Evaluation record)
        {
            if (_db.Evaluations.Any(e => e.Title.ToLower() == record.Title.ToLower().Trim() &&
                                 e.Date == record.Date &&
                                 e.UserId == record.UserId))
            {
                return Error("An evaluation with the same title, date, and user already exists!");
            }

            record.Title = record.Title?.Trim();
            _db.Evaluations.Add(record);
            _db.SaveChanges();
            return Success("Evaluation created successfully.");
        }
        public Service Update(Evaluation record)
        {
            if (_db.Evaluations.Any(e => e.Id != record.Id &&
                                 e.Title.ToLower() == record.Title.ToLower().Trim() &&
                                 e.Date == record.Date &&
                                 e.UserId == record.UserId))
            {
                return Error("An evaluation with the same title, date, and user already exists!");
            }

            var entity = _db.Evaluations
                .Include(e => e.EvaluatedEvaluations)
                .SingleOrDefault(e => e.Id == record.Id);

            if (entity is null)
                return Error("Evaluation not found!");

            _db.EvaluatedEvaluations.RemoveRange(entity.EvaluatedEvaluations);

            entity.Title = record.Title?.Trim();
            entity.Date = record.Date;
            entity.Score = record.Score;
            entity.Description = record.Description;
            entity.UserId = record.UserId;
            entity.EvaluatedEvaluations = record.EvaluatedEvaluations;

            _db.Evaluations.Update(entity);
            _db.SaveChanges();
            return Success("Evaluation updated successfully.");
        }
        public Service Delete(int id)
        {
            var entity = _db.Evaluations
                .Include(e => e.EvaluatedEvaluations)
                .SingleOrDefault(e => e.Id == id);

            if (entity is null)
                return Error("Evaluation not found!");
            _db.EvaluatedEvaluations.RemoveRange(entity.EvaluatedEvaluations);

            _db.Evaluations.Remove(entity);
            _db.SaveChanges();
            return Success("Evaluation deleted successfully.");

        }
    }
}
