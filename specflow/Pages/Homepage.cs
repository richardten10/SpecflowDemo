using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace specflow
{
    class HomePage : BasePage
    {

        public HomePage(IWebDriver driver): base(driver)
        {
           
        }

        [FindsBy(How = How.Name, Using = "q")]
        public IWebElement searchBar { get; set; }

        By searchField = get("type", "search");
        By firstResult = get("class", "title");
        By addButton = get("class", "add_to_basket_section ");
        By basketIcon = get("href", "/basket");
        By acceptCookies = get("onclick", "CookieControl.notifyAccept()");
        

        String brownieDescription = "Brownie Mini Bites";
        String chipDescription = "Hummus Chips";
        String popcornDescription = "Simply Salted";

        public void goTo()
        {
                driver.Navigate().GoToUrl("https://www.abelandcole.co.uk/pantry/chocolate-snacks");
                waitForLoad();
        }

        public void clickAcceptCookies()
        {
            waitAndClick(driver.FindElement(acceptCookies));

        }

        public void verifyPageTitle()
        {
            Console.WriteLine(driver.Title);
            Assert.AreEqual(driver.Title, "Pantry | Chocolate & snacks | Abel & Cole");
        }

        public void searchFor(String term)
        {
            String URL = driver.Url;
            if (!URL.Equals("https://www.abelandcole.co.uk/pantry/chocolate-snacks"))
            {
                goTo(); 
            }
           slowlyType(searchField, term); 
        }

        public void addBrowniesToBasketBySearch()
        {
            // searchFor("browni");
            // Thread.Sleep(1000);
            // driver.FindElement(searchField).SendKeys("e");

            searchFor("brownie");

            // Thread.Sleep(5000);

            //  waitForBoolean(driver.FindElement(firstResult).Text.Contains(brownieDescription));

            // pageDown(); 


            waitForDisplay(firstResult);


            if (driver.FindElement(firstResult).Text.Contains(brownieDescription))
            {
                Thread.Sleep(5000);
                waitAndClick(driver.FindElement(addButton));
            }
            else
            {
                Console.WriteLine("Could not find item");
                Console.WriteLine(driver.FindElement(firstResult).Text);
                throw new Exception(); 
            }
        }

        public void addChipsToBasketBySearch()
        {
            searchFor("humm");
            // Thread.Sleep(2000);
            //  driver.FindElement(searchField).SendKeys("m");

            // Thread.Sleep(5000);

            // pageDown(); 

            waitForDisplay(firstResult);

            if (driver.FindElement(firstResult).Text.Contains(chipDescription))
            {
                waitAndClick(driver.FindElement(addButton));
            }
            else
            {
                Console.WriteLine("Could not find item");
                throw new Exception();
            }
        }

        public void addPopCornToBasketBySearch()
        {
            searchFor("pop");
            //  Thread.Sleep(2000);
            //  driver.FindElement(searchField).SendKeys("p");

            waitForDisplay(firstResult);

           // Thread.Sleep(5000);

            if (driver.FindElement(firstResult).Text.Contains(popcornDescription))
            {
                waitAndClick(driver.FindElement(addButton));
            }
            else
            {
                Console.WriteLine("Could not find item");
                throw new Exception();
            }
        }

        public void goToBasket()
        {
            waitAndClick(driver.FindElement(basketIcon));
            waitForLoad();

            String URL = driver.Url;
            Console.WriteLine(URL);
            waitForBoolean(URL.Equals("https://www.abelandcole.co.uk/SignUp"));

            Assert.True(URL.Equals("https://www.abelandcole.co.uk/SignUp"));
        }

    }
}
