using System;
using ELMS_Client.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ELMS_Client.Models;
using System.Collections.Generic;
using System.Linq;

namespace ELMS_Client.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void RegisterUser()
        {
            //Arrange
            UserController hc = new UserController();
            User doc = new User { Name = "ABC", Address = "Qwerty", Phone_Number = 101, Interest = "zxcv", Email_id = "elms1234@gmail.com", Password = "abcd", Bank_Name = "State", Credit_Card = 12345678, Subscription = true, Admin = false };

            //Act
            IEnumerable<User> doc1 = hc.CreateUser(doc);

            //Assert
            Assert.IsTrue(doc1.Contains(doc));
        }


        [TestMethod]
        public void EditUser()
        {
            //Arrange
            UserController gc = new UserController();
            User cus = new User { User_id = 1006, Name = "ABC", Address = "qwerty", Phone_Number = 101, Interest = "zxcv", Email_id = "abc1234@gmail.com", Password = "abcd", Bank_Name = "State", Credit_Card = 12345678, Subscription = true, Admin = false };

            //Act

            IEnumerable<User> custs = gc.CreateUser(cus);

            //Assert
            if (custs.Contains(cus))
            {
                cus.Name = "ABC";
                IEnumerable<User> guests2 = gc.EditUser(cus);
                Assert.AreEqual(guests2.First(p => p.User_id == cus.User_id).Name, "ABC");
            }
        }
    }
}
