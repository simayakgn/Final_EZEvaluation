using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class EvaluationModel
    {
        public Evaluation Record { get; set; }

        [DisplayName("Title")]
        public string Title => Record.Title;

        [DisplayName("Score")]
        public string Score => Record.Score.ToString("F");

        [DisplayName("Date")]
        public string Date => !Record.Date.HasValue ? string.Empty : Record.Date.Value.ToString("MM/dd/yyyy");
        
        [DisplayName("Description")]
        public string Description => Record.Description;
        [DisplayName("User")]
        public string User => Record.User?.UserName;

        [DisplayName("Evaluateds")]
        public string Evaluateds => string.Join("<br>", Record.EvaluatedEvaluations.Select(ee => ee.Evaluated.Name));

        [DisplayName("Evaluateds Count")]
        public int EvaluatedsCount => Record.EvaluatedEvaluations.Count;

        [DisplayName("Evaluated IDs")]
        public List<int> EvaluatedIds
        {
            get => Record?.EvaluatedEvaluations?.Select(ee => ee.EvaluatedId).ToList() ?? new List<int>();
            set => Record.EvaluatedEvaluations = value.Select(v => new EvaluatedEvaluation { EvaluatedId = v }).ToList();
        }


    }
}
