using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumProject.Config;
using SeleniumProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.TestCases
{
    internal class Test1 : TestSetUp
    {
        private PreconditionsUtil preconditionsUtil;
        private AssertionsUtil assertionsUtil;

        [SetUp]
        public void TestSetup()
        {
            preconditionsUtil = new PreconditionsUtil(driver);
            assertionsUtil = new AssertionsUtil(driver);
        }

        [Test, Order(1)]
        public void LeaveAllFieldsEmpty()
        {
            preconditionsUtil.RunAllPreconditionsTest1();
            FillInForm("", "", "");
            Wait();
            assertionsUtil.AssertErrorMessageIs("First Name");
            Console.WriteLine("Test actions (leave empty all mandatory fields) completed successfully.");
        }

        [Test, Order(2)]
        public void LeaveEmptyFirstName()
        {
            preconditionsUtil.RunAllPreconditionsTest1();
            FillInForm("", "Kaffe", "54351");
            Wait();
            assertionsUtil.AssertErrorMessageIs("First Name");
            Console.WriteLine("Test actions (leave empty first name field) completed successfully.");
        }

        [Test, Order(3)]
        public void LeaveEmptyLastName()
        {
            preconditionsUtil.RunAllPreconditionsTest1();
            FillInForm("Konstantina", "", "54351");
            Wait();
            assertionsUtil.AssertErrorMessageIs("Last Name");
            Console.WriteLine("Test actions (leave empty last name field) completed successfully.");
        }

        [Test, Order(4)]
        public void LeaveEmptyPostalCode()
        {
            preconditionsUtil.RunAllPreconditionsTest1();
            FillInForm("Konstantina", "Kaffe", "");
            Wait();
            assertionsUtil.AssertErrorMessageIs("Postal Code");
            Console.WriteLine("Test actions (leave emtpy postal code field) completed successfully.");
        }

        [Test, Order(5)]
        public void HappyPath()
        {
            preconditionsUtil.RunAllPreconditionsTest1();
            FillInForm("Konstantina", "Kaffe", "54351");
            Wait();
            assertionsUtil.AssertErrorMessageElementDoesNotExist();
            assertionsUtil.AssertCurrentUrlIs("https://www.saucedemo.com/checkout-step-two.html");
            assertionsUtil.AssertOverviewPage();
            Console.WriteLine("Test actions (happy path - fill in all fields with valid data) completed successfully.");
        }

        private void FillInForm(string firstName, string lastName, string postalCode)
        {
            driver.FindElement(By.Id("first-name")).Click();
            driver.FindElement(By.Id("first-name")).SendKeys(firstName);
            driver.FindElement(By.Id("last-name")).Click();
            driver.FindElement(By.Id("last-name")).SendKeys(lastName);
            driver.FindElement(By.Id("postal-code")).Click();
            driver.FindElement(By.Id("postal-code")).SendKeys(postalCode);
            driver.FindElement(By.Id("continue")).Click();
        }
    }
}
