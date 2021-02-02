using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ELMS_Client;
using ELMS_Client.Controllers;
using ELMS_Client.Models;

namespace ELMS_Client.Tests.Controllers
{
    [TestClass]
    public class DocumentControllerTest
    {
        //[TestMethod]
        //public void Index()
        //{
        //    // Arrange
        //    DocumentController controller = new DocumentController();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Index",result.ViewName);
        //}


        [TestMethod]
        public void CreateDocument()
        {
            //Arrange
            DocumentController hc = new DocumentController();
            Document doc = new Document { Doc_id = 1008, Title = "ABC", Description = "Qwerty", Discipline_id = 101, Author = "Abc", Date = DateTime.Parse("08-09-2020"), Price = 1000, Doc_TypeId = 100 };

            //Act
            IEnumerable<Document> doc1 = hc.CreateDocument(doc);

            //Assert
            Assert.IsTrue(doc1.Contains(doc));
        }



        [TestMethod]
        public void EditDocument()
        {
            //Arrange
            DocumentController gc = new DocumentController();
            Document cus = new Document { Doc_id = 1008, Title = "ABC", Description = "qwerty", Discipline_id = 101, Author = "zxcv", Date = DateTime.Parse("08-09-2020"), Price = 1000, Doc_TypeId = 100 };

            //Act

            IEnumerable<Document> custs = gc.CreateDocument(cus);

            //Assert
            if (custs.Contains(cus))
            {
                cus.Title = "ABCD";
                IEnumerable<Document> guests2 = gc.EditDocument(cus);
                Assert.AreEqual(guests2.First(p => p.Doc_id == cus.Doc_id).Title, "ABCD");
            }
        }


        [TestMethod]
        public void About()
        {
            // Arrange
            DocumentController controller = new DocumentController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            DocumentController controller = new DocumentController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
