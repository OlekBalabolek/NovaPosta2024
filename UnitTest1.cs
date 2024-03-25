using System;

using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Support.UI;

using SeleniumExtras.WaitHelpers;


namespace NovaPosta2024
{
    public class Tests
    {
        IWebDriver driver;
        string ttnNumber = "20450866143311";
        string expectedParcelStatus = "Отримана";
        IWebElement inputTtnField;




        [SetUp]

        public void Initialize()

        {
            driver = new ChromeDriver();

        }


        [Test]
        public void Test()
        {
            driver.Url = "https://novaposhta.ua/";
            driver.Navigate().Refresh();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
           

            IWebElement inputTtnField = driver.FindElement(By.XPath("//*[@id='cargo_number']"));
            inputTtnField.SendKeys(ttnNumber);
            inputTtnField.Submit();

            wait.Until(ExpectedConditions.UrlToBe("https://tracking.novaposhta.ua/#/uk/chat/messages"));
            IWebElement okButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"chat\"]/div[2]/button")));
            okButton.Click();
            IWebElement status = driver.FindElement(By.XPath("//*[@id=\"chat\"]/header/div[2]/div[2]/div[2]"));
            string parcelStatus = status.GetAttribute("innerText");
            Assert.That(parcelStatus, Is.EqualTo("Отримана"));



            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            //inputTtnField = wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"cargo_number\"]")));



            /// IWebElement inputSend = driver.FindElement(By.XPath("//*[@id=\"top_block\"]/div[1]/div/div[2]/form/input[2]")));
            ///inputSend.Click();
        }



        [TearDown]


        public void FinishTest()
        {
            driver.Quit();
        }
    }
}
