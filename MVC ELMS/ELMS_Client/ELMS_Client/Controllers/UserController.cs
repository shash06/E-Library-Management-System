using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ELMS_Client.Models;

namespace ELMS_Client.Controllers
{
    public class UserController : Controller
    {

        //[TestController]
        #region TestContoller Methods
        public List<User> Read()
        {
            List<User> userList = new List<User>();
            return userList;
        }

        public List<User> CreateUser(User newUser)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<User>("User_Details", newUser);
            postTask.Wait();

            var result = postTask.Result;

            List<User> userList = new List<User>();
            userList.Add(newUser);
            return userList;
        }

        public List<User> EditUser(User myUser)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<User>("User_Details", myUser);
            postTask.Wait();

            var result = postTask.Result;

            List<User> userList = new List<User>();
            userList.Add(myUser);
            return userList;
        }
        #endregion



        // GET: User/Details/5
        public ActionResult Details()
        {
            if(Session["user"]==null)
            {
                return RedirectToAction("Login");
            }
            User myUser = (User)Session["user"];

        

            // using (var client = new HttpClient())
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //Called Member default GET All records  
            //GetAsync to send a GET request   
            // PutAsync to send a PUT request  
            var responseTask = client.GetAsync("User_Details/" + myUser.User_id);
            responseTask.Wait();

            //To store result of web api response.   
            var result = responseTask.Result;

            //If success received   
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<User>();
                readTask.Wait();

                myUser = readTask.Result;
            }
            else
            {
                //Error response received   
                myUser = null;
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }

            return View("Details", myUser);
        }

        public ActionResult Register()
        {
            
            Session.Clear();
            return View("Register");
        }
        
        [HttpPost]
        public ActionResult Register(User newUser)
        {
            newUser.Subscription = false;
            newUser.Admin = false;
            HttpClient client = new HttpClient();
            
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            var responseTask = client.GetAsync("User_Details/" + newUser.Email_id);
            responseTask.Wait();

            //To store result of web api response.   
            var myResult = responseTask.Result;

            //If success received   
            if (myResult.IsSuccessStatusCode)
            {
                var readTask = myResult.Content.ReadAsAsync<User>();
                readTask.Wait();

                if(readTask.Result==null)
                {
                    ModelState.AddModelError(string.Empty, "Email ID already exists!");
                    return View("Register", newUser);
                }
            }

            //HTTP POST
            var postTask = client.PostAsJsonAsync<User>("User_Details", newUser);
                postTask.Wait();

                var result = postTask.Result;
         
                if (result.IsSuccessStatusCode)
                {
                //Session["success"] = "User Registered Successfully!";
                Session["Register"]  = "Registation Successful!\nLogin with your credentials";
                    return RedirectToAction("Login");

                }
            

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View("Register", newUser);
        }

        //Get
        public ActionResult Login()
        {
            //Session.Clear();

            Session["user"] = null;
            Session["subscription"] = null;
            Session["admin"] = null;
            Session["name"] = null;
            return View("Login");
        }
        //Post
        [HttpPost]
        public ActionResult Login(Login myLogin)
        {
            HttpClient client = new HttpClient();
            
                client.BaseAddress = new Uri("https://localhost:44310/api/");

              
                var responseTask = client.GetAsync("User_Details?email=" + myLogin.Email+"&password="+myLogin.Password);
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();

                    User myUser = readTask.Result;
                
                     Session["user"] = myUser;
                     Session["subscription"] = myUser.Subscription;
                     Session["admin"] = myUser.Admin;
                     Session["name"] = myUser.Name;
                     return RedirectToAction("GetDocuments", "Document");
                 }

            Session.Clear();

            ModelState.AddModelError(string.Empty, "Please Check your Email or Password.");

            return View("Login", myLogin);
        }


        // GET: User/Edit/5
        public ActionResult Edit()
        {
            if(Session["user"]==null)
            {
                return RedirectToAction("Login");
            }
            User myUser = (User)Session["user"];

            return View(myUser);
        }

        [HttpPost]
        public ActionResult Edit(User myUser)
        {
            myUser.Subscription = (Boolean)Session["subscription"];
            myUser.Admin = (Boolean)Session["admin"];

            bool flag = true;

            if((myUser.Bank_Name == null || myUser.Bank_Name == String.Empty) && myUser.Credit_Card == null)
            {
                flag = false;
            }
            if(flag)
            {
                if (((myUser.Bank_Name == null || myUser.Bank_Name == String.Empty) && myUser.Credit_Card != null) || (myUser.Credit_Card == null && (myUser.Bank_Name != null || myUser.Bank_Name != String.Empty)))
                {
                    ModelState.AddModelError(string.Empty, "Payment Details Incomplete");
                    return View("Edit", myUser);
                }
            }
          


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<User>("User_Details/" + myUser.User_id.ToString(), myUser);
            putTask.Wait();


            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                Session["user"] = myUser;
                Session["subscription"] = myUser.Subscription;
                Session["admin"] = myUser.Admin;
                Session["name"] = myUser.Name;

                Session["Success"] = "Details Modified Successfully!";
                return RedirectToAction("Details");
                //return RedirectToAction("GetDocuments","Document");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(myUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            User myUser = (User)Session["user"];
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //HTTP DELETE
            var deleteTask = client.DeleteAsync("User_Details/" + myUser.User_id.ToString());
            deleteTask.Wait();

            var result = deleteTask.Result;
            if (result.IsSuccessStatusCode)
            {
                  
                Session.Clear();
                Session["Success"] = "Profile Deleted Successfully!";
                
            }


            return RedirectToAction("GetDocuments","Document");
        }

        public ActionResult EditSubscription()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            User myUser = (User)Session["user"];
           
            if((Boolean)Session["subscription"])
            {
                myUser.Subscription = false;
                Session["subscription"] = false;
            }
            else
            {
                if (myUser.Credit_Card == null)
                {
                    Session["Info"] = "Please Enter your Payment Details";
                    return RedirectToAction("Edit");
                }
                myUser.Subscription = true;
                Session["subscription"] = true;
            }

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44310/api/");

            //HTTP POST
            var putTask = client.PutAsJsonAsync<User>("User_Details/" + myUser.User_id.ToString(), myUser);
            putTask.Wait();


            var result = putTask.Result;
            if (result.IsSuccessStatusCode)
            {
                Session["user"] = myUser;
                Session["subscription"] = myUser.Subscription;
                Session["admin"] = myUser.Admin;
                Session["name"] = myUser.Name;
                if(myUser.Subscription)
                {
                    Session["Success"] = "Congratulations! You're a Subscriber now.";
                }
                else
                {
                    Session["Success"] = "Your Subscription has ended!";
                }
                
                return RedirectToAction("Details");
                //return RedirectToAction("GetDocuments","Document");
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");


            return RedirectToAction("Details");

        }
    }
}
