using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightDemo;

public class NunitPlaywright : Microsoft.Playwright.NUnit.PageTest
{
    [SetUp]
    public async Task Setup()
    {        
        await Page.GotoAsync("http://www.eaapp.somee.com");
    }

    [Test]
    public async Task Test1()
    {
        await Page.ClickAsync("text=Login");
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAApp.jpg"
        });
        await Page.FillAsync("#UserName", "admin");       
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAAppLogin.jpg"
        });
        await Page.FillAsync("#Password", "password");        
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAAppPass.jpg"
        });
        await Page.ClickAsync("text=Log in");
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAAppClick.jpg"
        });
        await Expect(Page.Locator("text='Employee Details'")).ToBeVisibleAsync();
    }
}