using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Security.Cryptography.X509Certificates;

public static class WebElementExtensions
{

    public static void Focus(this IWebElement element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Element cannot be null.");
        }

        // Retrieve the WebDriver from the element
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;

        // Use JavaScript to focus on the element
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        js.ExecuteScript("arguments[0].focus();", element);
    }

    public static void MoveAndClick(this IWebElement element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Element cannot be null.");
        }

        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;


        try
        {
            // Scroll the element into view
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView({ block: 'center', inline: 'center' });", element);

            // Use Actions to click at the center of the element
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Click().Perform();
            Console.WriteLine("Element clicked successfully using Actions.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Actions click failed: {ex.Message}. Falling back to JavaScript click.");
            JavaScriptClick(element);
            Console.WriteLine("Element clicked successfully using JavaScript.");
        }
    }

    public static void JavaScriptClick(this IWebElement element) {
        IJavaScriptExecutor js = (IJavaScriptExecutor)((IWrapsDriver)element).WrappedDriver;
        js.ExecuteScript("arguments[0].click();", element);
        Console.WriteLine("Element clicked successfully using JavaScript.");
    }
}
