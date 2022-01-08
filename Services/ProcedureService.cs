using AutoMapper;
using DataAccess;
using Services.Models.Doctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProcedureService
    {
        List<ProcedureDto> GetAll();
    }

    public class ProcedureService : IProcedureService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProcedureService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ProcedureDto> GetAll()
        {
            var dbProcedures = _dbContext.Procedures.ToList();
            return _mapper.Map<List<ProcedureDto>>(dbProcedures);
        }
    }
}
