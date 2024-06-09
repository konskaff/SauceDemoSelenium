using OpenQA.Selenium;
using SeleniumProject.Config;
using SeleniumProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.TestCases
{
    internal class Test2 : TestSetUp
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
        public void VerifyProductNames()
        {
            preconditionsUtil.RunAllPreconditionsTest2();

            (string Name1, int Price1) = AddAndGetRandomProductToCart();
            (string Name2, int Price2) = AddAndGetRandomProductToCart();
            preconditionsUtil.GoToCart();
            preconditionsUtil.PressCheckoutButton();

            FillInForm();
            Wait();

            assertionsUtil.AssertOverviewPage();
            assertionsUtil.AssertCartItemName(Name1);
            assertionsUtil.AssertCartItemName(Name2);

            Console.WriteLine("Test actions (verification of product names) completed successfully.");
        }

        [Test, Order(2)]
        public void VerifyProductPrices()
        {
            preconditionsUtil.RunAllPreconditionsTest2();

            (string Name1, int Price1) = AddAndGetRandomProductToCart();
            (string Name2, int Price2) = AddAndGetRandomProductToCart();
            preconditionsUtil.GoToCart();
            preconditionsUtil.PressCheckoutButton();

            FillInForm();
            Wait();

            assertionsUtil.AssertOverviewPage();
            assertionsUtil.AssertCartItemPrice(Name1, Price1);
            assertionsUtil.AssertCartItemPrice(Name2, Price2);
            assertionsUtil.AssertCartTotalPrice(Price1 + Price2);

            Console.WriteLine("Test actions (verification the prices of the product) completed successfully.");
        }


        private (string, int) AddAndGetRandomProductToCart()
        {
            IWebElement baseDiv = driver.FindElement(By.ClassName("inventory_list"));
            IList<IWebElement> inventoryItems = baseDiv.FindElements(By.ClassName("inventory_item")); //all the item components
            int random = new Random().Next(0, inventoryItems.Count);
            IWebElement chosenItem = inventoryItems[random];  //the component of the spcefic chosen item

            string chosenItemName = chosenItem.FindElements(By.ClassName("inventory_item_name"))[0].Text; //save the name of the chosen item
            string chosenItemPrice = chosenItem.FindElements(By.ClassName("inventory_item_price"))[0].Text; //save the price of the chosen item

            chosenItem.FindElements(By.TagName("button"))[0].Click();  //click the add to cart button of the chosen item

            Wait();

            int intChosenPrice = ParsePrice(chosenItemPrice);
            Console.WriteLine("The chosen item is: " + chosenItemName + ". " +
                "The price of the chosen item is: " + chosenItemPrice + " (" + intChosenPrice + ").");

            return (chosenItemName, intChosenPrice);
        }

        public static int ParsePrice(string price)
        {
            int result = 0;
            string removedNonNumeric = price.Replace("$", "").Replace(",", "").Replace(".", "");
            int.TryParse(removedNonNumeric, out result);   //result = the integer form of the price (e.g. input: "$29.99", output: 2999)
            return result;
        }

        private void FillInForm()
        {
            driver.FindElement(By.Id("first-name")).Click();
            driver.FindElement(By.Id("first-name")).SendKeys("Konstantina");
            driver.FindElement(By.Id("last-name")).Click();
            driver.FindElement(By.Id("last-name")).SendKeys("Kaffe");
            driver.FindElement(By.Id("postal-code")).Click();
            driver.FindElement(By.Id("postal-code")).SendKeys("54351");
            driver.FindElement(By.Id("continue")).Click();
            Wait();
        }
    }
}
