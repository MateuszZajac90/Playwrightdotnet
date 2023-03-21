using Microsoft.Playwright;
using NUnit.Framework;
using PlaywrightDemo.Pages;

namespace PlaywrightDemo;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test2()
    {
        //Playwright
        using var playwright = await Playwright.CreateAsync();

        //Browser 
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        //Page
        var page = await browser.NewPageAsync(); 
        await page.GotoAsync("http://www.eaapp.somee.com");
        await page.ClickAsync("text=Login");
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAApp.jpg"
        });
        await page.FillAsync("#UserName", "admin");       
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAAppLogin.jpg"
        });
        await page.FillAsync("#Password", "password");        
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAAppPass.jpg"
        });
        await page.ClickAsync("text=Log in");
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "EAAppClick.jpg"
        });
        var isExist = await page.Locator("text='Employee Details'").IsVisibleAsync();

        Assert.IsTrue(isExist);
    }
    
    [Test]
    public async Task Test3WithPOM()
    {
        //Playwright
        using var playwright = await Playwright.CreateAsync();

        //Browser 
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        //Page
        var page = await browser.NewPageAsync(); 
        await page.GotoAsync("http://www.eaapp.somee.com");
        
        var loginPage = new LoginPageUpgraded(page);
        
        await loginPage.ClickLogin();
        await loginPage.Login("admin", "password");
        var isExist = await loginPage.IsEmployeeDetailsExsists();
        Assert.IsTrue(isExist);
    }

    [Test]
    public async Task Test4Network()
    {
        //Playwright
        using var playwright = await Playwright.CreateAsync();

        //Browser 
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        //Page
        var page = await browser.NewPageAsync(); 
        await page.GotoAsync("http://www.eaapp.somee.com");
        
        var loginPage = new LoginPageUpgraded(page);
        
        await loginPage.ClickLogin();

        var waitResponse = page.WaitForResponseAsync("**/Employee");
        await loginPage.ClickEmployeeList();
        var getResponse = await waitResponse;

        var response = await page.RunAndWaitForResponseAsync(async () => 
        {
            await loginPage.ClickEmployeeList();
        }, x => x.Url.Contains("/Employee") && x.Status==200);

        await loginPage.Login("admin", "password");
        var isExist = await loginPage.IsEmployeeDetailsExsists();
        Assert.IsTrue(isExist);
    }
}