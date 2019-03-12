using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Threading;

namespace specflow
{
    class BasketPage : BasePage
    {

        public BasketPage(IWebDriver driver) : base(driver)
        {
           
        }

        By basketItem = get("class", "product-name");

        public void assertItemInBasket(String item)
        {
            bool check = false;
            IList<IWebElement> items = driver.FindElements(basketItem);

            foreach (IWebElement element in items)
            {
                if (element.Text.Contains(item))
                {
                    check = true;
                }
                else
                {
                    Console.WriteLine(element.Text);
                }
            }

            Assert.True(check);
        }

        public void deleteFromBasket(String item)
        {
            IList<IWebElement> items = driver.FindElements(basketItem);
            int i = 1;
            foreach (IWebElement element in items)
            {
                if (element.Text.Contains(item))
                {
                    waitAndClick(driver.FindElement(getN("class", "closing-x", i.ToString())));
                    waitForLoad();
                }
                else
                {
                    Console.WriteLine(element.Text);
                    i = i + 1;
                }
            }
        }

   

    public void assertPopcornNotInBasket()
        {
        driver.Navigate().Refresh();
        waitForLoad();

        bool check = true;
        IList<IWebElement> items = driver.FindElements(basketItem);

        foreach (IWebElement element in items)
        {
                Console.WriteLine(element.Text);
                if (element.Text.Contains("Popcorn"))
            {
                check = false;
            }
        }

        Assert.True(check);

        }

        public void show()
        {
            Thread.Sleep(2500);
            pageDown();
            Thread.Sleep(2500);
            pageUp();
            Thread.Sleep(2500);
        }



        
    }

   
}
