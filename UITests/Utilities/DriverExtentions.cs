using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace SeleniumExtensions
{
    public static class WebDriverExtensions
    {
        public static bool SwitchToWindowByTitle(this IWebDriver driver, string title, int timeoutInSeconds = 10)
        {
            if (driver == null)
                throw new ArgumentNullException(nameof(driver));

            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be null or empty", nameof(title));

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                return wait.Until(d =>
                {
                    foreach (var handle in d.WindowHandles)
                    {
                        d.SwitchTo().Window(handle);
                        if (d.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                    return false;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false; // Timeout reached without finding the window
            }
        }

        public static bool SwitchToWindowByExactTitle(this IWebDriver driver, string exactTitle, int timeoutInSeconds = 10)
        {
            if (driver == null)
                throw new ArgumentNullException(nameof(driver));

            if (string.IsNullOrEmpty(exactTitle))
                throw new ArgumentException("Exact title cannot be null or empty", nameof(exactTitle));

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

                return wait.Until(d =>
                {
                    foreach (var handle in d.WindowHandles)
                    {
                        d.SwitchTo().Window(handle);
                        if (d.Title.Equals(exactTitle, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                    return false;
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false; 
            }
        }
    }
}
