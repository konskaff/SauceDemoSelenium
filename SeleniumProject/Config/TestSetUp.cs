
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumProject.Util;
#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method



namespace SeleniumProject.Config
{
    [TestFixture]
    public class TestSetUp
    {
        public const int Delay = 1;
        public IWebDriver driver;
        //private VideoRecorder videoRecorder;


        [SetUp]        // [OneTimeSetUp] on Continuing from One Scenario to Another
        public void Setup()
        {
            try
            {
                WebDriverSetup();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during WebDriver setup: " + ex.Message);
                throw;
            }

            //videoRecorder = new VideoRecorder();
            //videoRecorder.StartRecording();

        }

        [TearDown]      //[OneTimeTearDown] on Continuing from One Scenario to Another
        public void Cleanup()
        {
            driver.Quit();
            Console.WriteLine("WebDriver cleaned up successfully.");
            //videoRecorder.StopRecording();
        }
        private void WebDriverSetup()
        {
          
            driver = new ChromeDriver(@"C:\Users\kkaffe\source\repos\SauceDemoSelenium\SeleniumProject\Drivers");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            Console.WriteLine("-> Opening " + driver.Url);
            Console.WriteLine("WebDriver initialized successfully.");
        }

        public static void Wait()
        {
            Thread.Sleep(TimeSpan.FromSeconds(Delay));
        }
    }
}
