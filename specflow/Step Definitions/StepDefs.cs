using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace specflow
{
    [Binding]
    public sealed class ExampleStepDefinitions
    {
        static DriverFactory driverFactory = new DriverFactory();
        static IWebDriver driver = driverFactory.getDriver();
        HomePage homepage = new HomePage(driver);
        BasketPage basketPage = new BasketPage(driver);

        [BeforeTestRun]
        public static void Before()
        {
            driver.Manage().Window.Maximize();
        }

        [Given(@"I am on AbelAndCole")]
        public void GivenIAmOnAbelAndCole()
        {
            homepage.goTo();
            homepage.verifyPageTitle();
            homepage.clickAcceptCookies(); 
        }


        [When(@"I search for brownies and add them to basket")]
        public void WhenISearchForBrowniesAndAddThemToBasket()
        {
            homepage.addBrowniesToBasketBySearch(); 
        }

        [When(@"I search for chips and add them to basket")]
        public void WhenISearchForChipsAndAddThemToBasket()
        {
            homepage.addChipsToBasketBySearch(); 
        }

        [When(@"I search for popcorn and add them to basket")]
        public void WhenISearchForPopcornAndAddThemToBasket()
        {
            homepage.addPopCornToBasketBySearch(); 
        }

        [Then(@"I will have brownies in my basket")]
        public void ThenIWillHaveBrowniesInMyBasket()
        {
            homepage.goToBasket();
            basketPage.assertItemInBasket("Brownie"); 
        }

        [Then(@"I will have chips in my basket")]
        public void ThenIWillHaveChipsInMyBasket()
        {
            homepage.goToBasket(); 
            basketPage.assertItemInBasket("Chip");
        }

        [Then(@"I will have popcorn in my basket")]
        public void ThenIWillHavePopcornInMyBasket()
        {
            homepage.goToBasket();
            basketPage.assertItemInBasket("Popcorn");
        }

        [When(@"I remove popcorn from my basket")]
        public void WhenIRemovePopcornFromMyBasket()
        {
            basketPage.deleteFromBasket("Popcorn");
        }

        [Then(@"I will not have popcorn in my basket")]
        public void ThenIWillNotHavePopcornInMyBasket()
        {
            basketPage.assertPopcornNotInBasket();

            basketPage.assertItemInBasket("Brownie");
            basketPage.assertItemInBasket("Chip");

            Thread.Sleep(5000);

            basketPage.show();
        }



        [AfterTestRun]
        public static void AfterWebFeature()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
