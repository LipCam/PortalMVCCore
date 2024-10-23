﻿using PortalMVCCore.DAL.Entities;
using PortalMVCCore.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PortalMVCCore.DAL.Repositories
{
    public class ProgramasRepository : IProgramasRepository
    {
        private IRepositoryBase<PROGRAMAS_TAB> _repositoryBase;

        public ProgramasRepository(IRepositoryBase<PROGRAMAS_TAB> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<PROGRAMAS_TAB> GetAll(Expression<Func<PROGRAMAS_TAB, bool>> filter)
        {
            return _repositoryBase.GetAll(filter);
        }
    }
}