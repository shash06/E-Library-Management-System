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
    public class Document_TypeController : ApiController
    {
        private libraryEntities db = new libraryEntities();

        // GET: api/Document_Type
        public IQueryable<Document_Type> GetDocument_Type()
        {
            return db.Document_Type;
        }

    }
}