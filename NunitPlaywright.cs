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
    public async Task LoginTest()
    {
        //await Page.ClickAsync("text=Login");
        //Page.SetDefaultTimeout(10);
        
        var lnkLogin = Page.Locator("text=Login");
        await lnkLogin.ClickAsync();

        await Page.FillAsync("#UserName", "admin");       
        await Page.FillAsync("#Password", "password");        
        //await Page.ClickAsync("text=Log in");
        var btnLogin = Page.Locator("input", new PageLocatorOptions { HasTextString = "Log in"});
        await btnLogin.ClickAsync();

        await Expect(Page.Locator("text='Employee Details'")).ToBeVisibleAsync();
    }
}