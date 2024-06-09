using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using SeleniumProject.Config;
using System.Reflection.Metadata;

namespace SeleniumProject.Util
{
    public class PreconditionsUtil
    {
        protected IWebDriver driver;

        public PreconditionsUtil(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        public void RunAllPreconditionsTest1()
        {
            Login();
            AddProductToCart();
            GoToCart();
            PressCheckoutButton();
        }

        public void RunAllPreconditionsTest2()
        {
            Login();
        }


        private void Login()
        {
            driver.FindElement(By.Id("user-name")).Click();
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            TestSetUp.Wait();
        }


        private void AddProductToCart()
        {
            /*IWebElement baseDiv = driver.FindElement(By.ClassName("inventory_list"));
            IWebElement inventoryItem = baseDiv.FindElements(By.ClassName("inventory_item"))[0];
            IWebElement itemDescription = inventoryItem.FindElements(By.ClassName("inventory_item_description"))[0];
            IWebElement priceBar = itemDescription.FindElements(By.ClassName("pricebar"))[0];
            priceBar.FindElements(By.TagName("button"))[0].Click();*/
            IWebElement addToCartBtn = driver.FindElement(By.CssSelector("[id^='add-to-cart-']"));
            addToCartBtn.Click();
            TestSetUp.Wait();
        }


        public void GoToCart()
        {
            IWebElement cartCotainer = driver.FindElement(By.Id("shopping_cart_container"));
            cartCotainer.FindElements(By.ClassName("shopping_cart_link"))[0].Click();
            TestSetUp.Wait();
        }


        public void PressCheckoutButton()
        {
            driver.FindElement(By.Id("checkout")).Click();
            TestSetUp.Wait();
        }


    }
}


