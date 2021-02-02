using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ELMS_WebAPI.Models;

namespace ELMS_WebAPI.Controllers
{
    public class DisciplinesController : ApiController
    {
        private libraryEntities db = new libraryEntities();

        // GET: api/Disciplines
        public IQueryable<Discipline> GetDisciplines()
        {
            return db.Disciplines;
        }

    }
}