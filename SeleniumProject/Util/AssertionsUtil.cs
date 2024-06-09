using OpenQA.Selenium;
using SeleniumProject.TestCases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject.Util
{
    internal class AssertionsUtil
    {
        protected IWebDriver driver;

        public AssertionsUtil(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void AssertErrorMessageIs(string message)
        {

            IWebElement errorMessage = driver.FindElements(By.ClassName("error-message-container"))[0];
            IWebElement header3 = errorMessage.FindElements(By.TagName("h3"))[0];

            Assert.That(header3.Text, Is.EqualTo("Error: " + message + " is required"));
            Console.WriteLine("Error: " + message + " is required.");
        }

        public void AssertErrorMessageElementDoesNotExist()
        {
            IList<IWebElement> errorMessage = driver.FindElements(By.ClassName("error-message-container"));
            Assert.That(errorMessage.Count, Is.EqualTo(0), "The element should not exist.");
            Console.WriteLine("No error message is displayed.");
        }

        public void AssertCurrentUrlIs(string url)
        {
            string currentUrl = driver.Url;
            Assert.That(currentUrl, Is.EqualTo(url), "Current URL is not the expected: " + url);
            Console.WriteLine("Current page: " + url);
        }

        public void AssertOverviewPage()
        {
            IWebElement pageHeader = driver.FindElements(By.ClassName("title"))[0];
            Assert.That(pageHeader.Text, Is.EqualTo("Checkout: Overview"));
            Console.WriteLine("You are redirected to the: Checkout: Overview");
        }

        public void AssertCartItemName(string itemName)
        {
            IList<IWebElement> cartItems = driver.FindElements(By.ClassName("cart_item"));
            for (int i = 0; i < cartItems.Count; i++)
            {
                IWebElement cartItem = cartItems[i];   //cartItem is every product component added to the cart
                string cartItemName = cartItem.FindElements(By.ClassName("inventory_item_name"))[0].Text;
                if (cartItemName == itemName)
                {
                     Console.WriteLine("The item added to the cart matches the item displayed in the overview.");
                     return;
                }
            }
            Console.WriteLine("The item added to the cart does not match any of the items displayed in the overview. " + itemName);
            Assert.Fail("The item added to the cart does not match any of the items displayed in the overview. " + itemName);
        }

        public void AssertCartItemPrice(string itemName, int itemPrice)
        {
            IList<IWebElement> cartItems = driver.FindElements(By.ClassName("cart_item"));
            for (int i = 0; i < cartItems.Count; i++)
            {
                IWebElement cartItem = cartItems[i];   //cartItem is every product component added to the cart
                string cartItemName = cartItem.FindElements(By.ClassName("inventory_item_name"))[0].Text;
                if (cartItemName == itemName)
                {
                    string cartItemPrice = cartItem.FindElements(By.ClassName("inventory_item_price"))[0].Text;  //the item price component
                    if(Test2.ParsePrice(cartItemPrice).Equals(itemPrice))
                    {
                        Console.WriteLine("The price of the item added to the cart matches the price of the item displayed in the overview.");
                        return;
                    } 
                }
            }
            Console.WriteLine("The price of the item added to the cart does not match the price of the item displayed in the overview. " + itemPrice);
            Assert.Fail("The price of the item added to the cart does not match the price of the item displayed in the overview. " + itemPrice);
        }

        public void AssertCartTotalPrice(int actualTotalPrice)
        {
            string ItemTotalPriceString = driver.FindElements(By.ClassName("summary_subtotal_label"))[0].Text;
            string itemTotalPrice = ItemTotalPriceString.Replace("Item total: ", "");
            int expectedTotalPrice = Test2.ParsePrice(itemTotalPrice);
            Assert.That(expectedTotalPrice, Is.EqualTo(actualTotalPrice));
            Console.WriteLine("The actual total price is: " + actualTotalPrice + "." +
                " The expected total price is: " + expectedTotalPrice + ".");

        }

    }
}
