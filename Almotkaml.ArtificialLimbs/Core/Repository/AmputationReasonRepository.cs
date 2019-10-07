using Almotkaml.ArtificialLimbs.Core.Domain;
using Almotkaml.ArtificialLimbs.Core.Extensions;
using Almotkaml.ArtificialLimbs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Almotkaml.ArtificialLimbs.Core.Repository
{
    public class AmputationReasonRepository : Repository<AmputationReason>, IAmputationReasonRepository
    {
        protected readonly ArtificialLimbsDbContext _Context;
        private readonly UnitOfWork _unitOfWork;
        public AmputationReasonRepository(ArtificialLimbsDbContext context)
            : base(context)
        {
            _Context = context;
            _unitOfWork = new UnitOfWork(_Context);
        }

        public bool AmputationReasonExist(string _reason)
        {
            return _context.AmputationReasons.Where(t=>t.Reason == _reason).Any();
        }

        public int Find(string _reason)
        {
            var id = _context.AmputationReasons.Where(r => r.Reason == _reason).Select(d => d.AmputationReasonID).FirstOrDefault();
            //var id =  _unitOfWork.AmputationReason.GetAll().ToList().Select(d=>d.AmputationReasonID).FirstOrDefault();
            return id;
        }

        public int AddNew(string _reason)
        {
            var reason = AmputationReason.New(_reason);
            _unitOfWork.AmputationReason.Add(reason);
            _unitOfWork.Complete();
            return reason.AmputationReasonID;
        }
    }
}