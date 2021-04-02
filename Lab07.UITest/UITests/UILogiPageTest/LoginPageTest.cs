using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace Lab07.UITest.UITests.UILogiPageTest
{
    /// <summary>
    /// Before runnig the UI tests, running the application without debugging (CTRL+F5) because a running server is required for UI tests
    /// </summary>
    public class LoginPageTest : IDisposable
    {
        private readonly ChromeDriverService _driverService;
        private readonly IWebDriver _driver;

        public LoginPageTest()
        {
            string pathToWebdriver = @"D:\Repositories\Lab01\lab-06-mvc\Lab07.UITest\webdriver"; // set full path to webdriver.exe Lab07.UITest/webdriver
            _driverService = ChromeDriverService.CreateDefaultService(pathToWebdriver);
            _driver = new ChromeDriver(_driverService);
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(100);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Theory]
        [InlineData("", "The Login field is required.")]
        [InlineData("123", "Login must have at least 4 characters")]
        public void Login_InvalidLogin_ReturnErrorMessage(string login, string expectedError)
        {
            _driver.Navigate().GoToUrl("http://localhost:55113/Account/Login");

            _driver.FindElement(By.Id("Login")).SendKeys(login);
            _driver.FindElement(By.Id("Password")).SendKeys("123456");
            _driver.FindElement(By.Id("LoginBtn")).Click();

            Assert.Contains(expectedError, _driver.PageSource);
        }

        [Theory]
        [InlineData("", "The Password field is required.")]
        [InlineData("123", "Login or password is incorrect")]
        public void Login_InvalidPassword_ReturnErrorMessage(string password, string expectedError)
        {
            _driver.Navigate().GoToUrl("http://localhost:55113/Account/Login");

            _driver.FindElement(By.Id("Login")).SendKeys("admin");
            _driver.FindElement(By.Id("Password")).SendKeys(password);
            _driver.FindElement(By.Id("LoginBtn")).Click();

            Assert.Contains(expectedError, _driver.PageSource);
        }

        [Theory]
        [InlineData("admin", "qwerty")]
        [InlineData("ponny", "qwerty")]
        public void Login_ValidCredentials_RedirectToHomeIndexPage(string login, string password)
        {
            _driver.Navigate().GoToUrl("http://localhost:55113/Account/Login");

            _driver.FindElement(By.Id("Login")).SendKeys(login);
            _driver.FindElement(By.Id("Password")).SendKeys(password);
            _driver.FindElement(By.Id("LoginBtn")).Click();

            Assert.Equal("bus station \"HAPPY JOURNEY\"", _driver.Title);
            Assert.Equal("http://localhost:55113/Home/Index", _driver.Url);
            Assert.Contains("Log out", _driver.PageSource);
            Assert.Contains(login, _driver.PageSource);
        }
    }
}