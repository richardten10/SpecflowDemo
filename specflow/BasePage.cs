using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text;
using System.Threading;

namespace specflow
{
    abstract class BasePage
    {

        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, new TimeSpan(20000));
        }

        public static By get(String attrib, String value)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("//*[@");
            builder.Append(attrib);
            builder.Append("=\"");
            builder.Append(value);
            builder.Append("\"]");
            return By.XPath(builder.ToString());
        }

        public By getN(String sort, String value, String N)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("(//*[@");
            builder.Append(sort);
            builder.Append("=\"");
            builder.Append(value);
            builder.Append("\"])");
            builder.Append("[");
            builder.Append(N);
            builder.Append("]");
            return By.XPath(builder.ToString());
        }

        public void waitForDisplay(By by)
        {
            bool check;
            for (int i = 0; i <= 20; i++)
            {
                try
                {
                    check = driver.FindElement(by).Displayed;
                    if (check)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
                catch (Exception e)
                {
                    Thread.Sleep(500);
                }
            }
        }

        public void waitForBoolean(Boolean bools)
        {
            // waits up to 10 seconds for boolean to be true (discounting driver wait time)
            for (int i = 0; i <= 20; i++)
            {
                bool freshBool = bools;
                if (freshBool)
                {
                    break;
                }
               Thread.Sleep(500);
            }
            waitForLoad(); 
        }

    

        public void waitForLoad()
        {
            // waits up to 10 seconds for the page to fully load
            for (int i = 0; i <= 20; i++)
            {
                bool loaded = ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");

                if (loaded)
                {
                    break;
                }
               
                if (i == 20)
                {
                    Console.WriteLine("Page never loaded");
                    throw new Exception(); 
                }

                Thread.Sleep(500);
            }
        }


        public void enterText(IWebElement element, String text)
        {
            element.SendKeys(text);
        }

        public void carefullyType(By by, String input)
        {
            // custom method for typing that corrects mistakes 
            int i = 0;
            int z = 0;
            while (i == 0)
            {
                if (driver.FindElement(by).GetAttribute("type").Equals("number") &&
                        input.Contains(","))
                {
                    i = i + 1;
                }
                driver.FindElement(by).SendKeys(input);
                if (driver.FindElement(by).GetAttribute("value").Equals(input) ||
                !(i == 0))
                {
                    i = i + 1;
                }
                else
                {
                    if (z < 20)
                    {
                        for (int j = 0; j < input.Length * 2; j++)
                        {
                            backspace();
                        }
                        z = z + 1;
                    }
                }
            }
        }

        public void pageDown()
        {
            IWebElement currentelement = driver.SwitchTo().ActiveElement();
            currentelement.SendKeys(Keys.PageDown);
        }

        public void pageUp()
        {
            IWebElement currentelement = driver.SwitchTo().ActiveElement();
            currentelement.SendKeys(Keys.PageUp);
        }

        public void backspace()
        {
            IWebElement currentelement = driver.SwitchTo().ActiveElement();
            currentelement.SendKeys(Keys.Backspace);
        }

        public void pressEnter()
        {
            IWebElement currentElement = driver.SwitchTo().ActiveElement();
            currentElement.SendKeys(Keys.Return);
        }

        public void waitAndClick(IWebElement element)
        {
            IWebElement clickable = wait.Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click(); 
        }

        public IWebElement waitForVisible(By by)
        {
            IWebElement visible = wait.Until(ExpectedConditions.ElementIsVisible(by));
            return visible; 
        }

        public void slowlyType(By by, String input)
        {
            int length = input.Length;

            for (int i = 0; i < length; i++)
            {
                char z = input.ToCharArray()[i];
                String y = z.ToString();
                driver.FindElement(by).SendKeys(y);
                Thread.Sleep(500);
            }
        }
    }
}
