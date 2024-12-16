using OpenQA.Selenium;

namespace UITests.Utilities
{
    internal static class DriverHelpers
    {
        public static void SwitchToWindowByTitle(this IWebDriver driver, string windowTitle, int timeoutInSeconds = 10)
        {
            if (string.IsNullOrWhiteSpace(windowTitle))
                throw new ArgumentException("Window title cannot be null or empty.", nameof(windowTitle));

            var timeout = DateTime.Now.AddSeconds(timeoutInSeconds);

            while (DateTime.Now < timeout)
            {
                var matchingHandle = driver.WindowHandles
                    .FirstOrDefault(handle =>
                    {
                        driver.SwitchTo().Window(handle);
                        return driver.Title.Equals(windowTitle, StringComparison.OrdinalIgnoreCase);
                    });

                if (matchingHandle != null)
                {
                    driver.SwitchTo().Window(matchingHandle);
                    return;
                }

                Task.Delay(500).Wait();
            }
        }
    }
}
