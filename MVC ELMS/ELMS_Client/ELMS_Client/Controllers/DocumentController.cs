using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ELMS_Client.Models;
using PagedList;
using PagedList.Mvc;

namespace ELMS_Client.Controllers
{
    public class DocumentController : Controller
    {
        //[TestController]
        #region TestContoller Methods
        public List<Document> Read()
        {
            List<Document> documentList = new List<Document>();
            return documentList;
        }

        public List<Document> CreateDocument(Document newDocument)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<Document>("Document_Details", newDocument);
            postTask.Wait();
            TempData["SuccessMessage"] = "Added Successfully";
            var result = postTask.Result;

            List<Document> documentList = new List<Document>();
            documentList.Add(newDocument);
            return documentList;
        }

        public List<Document> EditDocument(Document myDocument)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<Document>("Document_Details", myDocument);
            postTask.Wait();
            TempData["SuccessMessage"] = "Added Successfully";
            var result = postTask.Result;

            List<Document> documentList = new List<Document>();
            documentList.Add(myDocument);
            return documentList;
        }
        #endregion


        private static IEnumerable<Discipline> myDiscipline = null;
  


        public ActionResult GetDocuments( string myTitle, string myAuthor, string sortBy, int? page)
        {
            GetDiscipline();
            ViewBag.discipline = myDiscipline;
            IEnumerable<Document> myDocuments = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

               
                var responseTask = client.GetAsync("Document_Details");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Document>>();
                    readTask.Wait();

                    myDocuments = readTask.Result;
                }
                else
                {
                    //Error response received   
                    myDocuments = Enumerable.Empty<Document>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }


          
            if (myTitle == null )
            {
                myTitle = String.Empty;
            }
            if (myAuthor == null)
            {
                myAuthor = String.Empty;
            }
           

            myDocuments = myDocuments.Where(doc =>
            (doc.Title.StartsWith(myTitle) || myTitle == string.Empty ) &&
            (myAuthor == string.Empty || doc.Author == myAuthor || doc.Author.StartsWith(myAuthor))).ToList();


            ViewBag.TitleSort = String.IsNullOrEmpty(sortBy) ? "Title desc" : "";
            ViewBag.AuthorSort = sortBy == "Author" ? "Author desc" : "Author";



            switch (sortBy)
            {
                case "Title desc":
                    myDocuments = myDocuments.OrderByDescending(x => x.Title);
                    break;
                case "Author desc":
                    myDocuments = myDocuments.OrderByDescending(x => x.Author);
                    break;
                case "Author":
                    myDocuments = myDocuments.OrderBy(x => x.Author);
                    break;
                default:
                    myDocuments = myDocuments.OrderBy(x => x.Title);
                    break;
            }

            
            return View("GetDocuments", myDocuments.ToList().ToPagedList(page ?? 1, 2));
        }

     
        public ActionResult Create()
        {
            Session["Success"] = null;

            GetDiscipline();
            ViewBag.Discipline_Id = new SelectList(myDiscipline, "Discipline_Id", "Discipline_Name");
            if ((Session["admin"] != null && !(Boolean)Session["admin"]) || Session["admin"] == null )
            {
                return RedirectToAction("GetDocuments");
            }
            return View("Create");
        }
        [HttpPost]
        public ActionResult Create(Document newDocument)
        {
            GetDiscipline();
            ViewBag.Discipline_Id = new SelectList(myDiscipline, "Discipline_Id", "Discipline_Name", newDocument.Discipline_id);

            if (newDocument.Discipline_id == null)
            {
                //ModelState.AddModelError(String.Empty, "Discipline cannot be left blank");
                return View("Create", newDocument);
            }

            if (newDocument.Price == 0 || newDocument.Price == null)
            {
                newDocument.Doc_TypeId = 100;
            }
            else
            {
                newDocument.Doc_TypeId = 101;
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Document>("Document_Details", newDocument);
                postTask.Wait(); 
            var result = postTask.Result;
            
            if (result.IsSuccessStatusCode)
             {

                Session["Success"] = "Document Added Successfully!";
                return RedirectToAction("GetDocuments");
             }
            

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

        
            return View("Create",newDocument);
        }

        public ActionResult Edit(int id)
        {
            Session["Success"] = null;
            GetDiscipline();


            if ((Session["admin"] != null && !(Boolean)Session["admin"]) || Session["admin"] == null)
            {
                return RedirectToAction("GetDocuments");
            }
            Document myDocument = null;

             HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Document_Details/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Document>();
                    readTask.Wait();
                    

                myDocument = readTask.Result;
                ViewBag.Discipline_Id = new SelectList(myDiscipline, "Discipline_Id", "Discipline_Name",myDocument.Discipline_id);
            }
            
            return View(myDocument);
        }


        [HttpPost]
        public ActionResult Edit(Document myDocument)
        {
            GetDiscipline();
            ViewBag.Discipline_Id = new SelectList(myDiscipline, "Discipline_Id", "Discipline_Name", myDocument.Discipline_id);

            if (myDocument.Discipline_id == null)
            {
                //ModelState.AddModelError(String.Empty, "Discipline cannot be left blank");
                return View("Edit", myDocument);
            }
            if (myDocument.Price == 0 || myDocument.Price == null)
            {
                myDocument.Doc_TypeId = 100;
            }
            else
            {
                myDocument.Doc_TypeId = 101;
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<Document>("Document_Details/" + myDocument.Doc_id.ToString(), myDocument);
            putTask.Wait();

            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                Session["Success"] = "Document Modified Successfully!";
                //return View(myDocument);
                return RedirectToAction("GetDocuments");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

           
            return View("Edit",myDocument);
        }
        public ActionResult Delete(int id)
        {
            Session["Success"] = null;
            if ((Session["admin"] != null && !(Boolean)Session["admin"]) || Session["admin"] == null)
            {
                return RedirectToAction("GetDocuments");
            }
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Document_Details/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                
            if (result.IsSuccessStatusCode)
                {
                 Session["Success"] = "Document Deleted Successfully!";
                return RedirectToAction("GetDocuments");
                }
            

            return RedirectToAction("Edit");
        }
        public ActionResult Details(int id)
        {
            Session["Success"] = null;
            GetDiscipline();
            ViewBag.discipline = myDiscipline;
            Document myDocument = null;

            // using (var client = new HttpClient())
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //Called Member default GET All records  
            //GetAsync to send a GET request   
            // PutAsync to send a PUT request  
            var responseTask = client.GetAsync("Document_Details/"+id);
            responseTask.Wait();

            //To store result of web api response.   
            var result = responseTask.Result;

            //If success received   
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Document>();
                readTask.Wait();

                myDocument = readTask.Result;
                ViewBag.discount =(Double)myDocument.Price * 0.8;
            }
            else
            {
                //Error response received   
                myDocument = null;
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }

            return View("Details", myDocument);
        }

        public ActionResult Download(int id)
        {
            Session["Success"] = null;

            if (id == 100)
            {
                Session["Success"] = "Document Downloaded Successfully!";
                return RedirectToAction("GetDocuments");
            }
            

            if (Session["user"] == null)
            {
                Session["Register"] = "Premium documents can only be downloaded by Registered users. Kindly Login or SignUp if you don't have an account.";
                return RedirectToAction("Login","User");

            }

            User user = (User)Session["user"];

            if ( user.Credit_Card == null)
            {
                Session["Info"] = "Please Enter your Payment Details";
                return RedirectToAction("Edit", "User");
                
            }

            Session["Success"] = "Document Downloaded Successfully!";
            return RedirectToAction("GetDocuments");

        }

        public void GetDiscipline()
        {
             
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //Called Member default GET All records  
            //GetAsync to send a GET request   
            // PutAsync to send a PUT request  
            var responseTask = client.GetAsync("Disciplines");
            responseTask.Wait();

            //To store result of web api response.   
            var result = responseTask.Result;

            //If success received   
            if (result.IsSuccessStatusCode)
            {
                
                var readTask = result.Content.ReadAsAsync<IList<Discipline>>();
                readTask.Wait();

                myDiscipline = readTask.Result;
    
            }
            else
            {
                //Error response received   
                myDiscipline = Enumerable.Empty<Discipline>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}