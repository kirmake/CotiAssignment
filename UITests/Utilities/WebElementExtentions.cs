using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

public static class WebElementExtensions
{

    public static void Focus(this IWebElement element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element), "Element cannot be null.");
        }

        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;

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
            var actions = new Actions(driver);
            actions.MoveToElement(element).Click().Perform();
        }
        catch (Exception ex)
        {
            TestContext.WriteLine($"Actions click failed: {ex.Message}. Falling back to JavaScript click.");
            JavaScriptClick(element);
        }
    }

    public static void JavaScriptClick(this IWebElement element)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)((IWrapsDriver)element).WrappedDriver;
        js.ExecuteScript("arguments[0].click();", element);
        Console.WriteLine("Element clicked successfully using JavaScript.");
    }
}
