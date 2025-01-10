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
    public class EvaluatedService : Service, IService<Evaluated, EvaluatedModel>
    {
        public EvaluatedService(Db db) : base(db)
        {
        }

        public Service Create(Evaluated record)
        {
            if (_db.Evaluateds.Any(e => e.Name.ToUpper().Trim() == record.Name.ToUpper().Trim() &&
                                e.Surname.ToUpper().Trim() == record.Surname.ToUpper().Trim()))
            {
                return Error("An evaluated with the same name and surname already exists!");
            }

            _db.Evaluateds.Add(record);
            _db.SaveChanges();

            return Success("Evaluated record created successfully.");
        }

        public Service Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<EvaluatedModel> Query()
        {
            return _db.Evaluateds.Select(e => new EvaluatedModel
            {
                Record = e 
            });
        }

        public Service Update(Evaluated record)
        {
            throw new NotImplementedException();
        }
    }
}
