using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using Almotkaml.ArtificialLimbs.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class AmputationStatuesRepository : Repository<AmputationStatues>, IAmputationStatuesRepository
    {

        public AmputationStatuesRepository(ArtificialLimbsDbContext context)
            : base(context)
        {

        }
        public int? GetRefNO()
        {
            var MaxNO =  _context.AmputationStatues.Max(d => d.ReferenceNo) ;
            if (MaxNO == null) MaxNO = 0;
            var max = MaxNO + 1;
            return max;
        }

        public IEnumerable<AmputationStatues> Search(AmputationStatuesModel model, string State)
        {
            var Grid = _context.AmputationStatues.Include(d => d.device)
                                           .Include(d => d.patient)
                                            .Include(d => d.Technical).ToList();

            if (model.PatientID > 0)
                Grid = Grid.Where(d => d.PatientID == model.PatientID).ToList();

            if (model.DateFrom != null)
            {
                var _dateFrom = DateTime.Parse(model.DateFrom);
                Grid = Grid.Where(d => (Convert.ToDateTime(d.patient.RegistrationDate)) >= _dateFrom).ToList();
            }

            if (model.DateTo != null)
            {
                var _dateTo = DateTime.Parse(model.DateTo);
                Grid = Grid.Where(d => Convert.ToDateTime(d.patient.RegistrationDate) <= _dateTo).ToList();
            }

            if (State == "Recipt")
                Grid = Grid.Where(d => d.ReceiptDate != null).ToList();

            if (State == "Waiting")
                Grid = Grid.Where(d => d.ReceiptDate == null).ToList();

            return Grid;
        }

        public override IEnumerable<AmputationStatues> GetAll()
        {
            return _context.AmputationStatues.Include(d => d.patient)
                                        .Include(d => d.Technical)
                                        .Include(d => d.TechnicalAssistant)
                                        .Include(d => d.device)
                                        .Include(d=>d.AmputationReason).ToList();
        }

    }

}