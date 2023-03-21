using Microsoft.Playwright;

namespace PlaywrightDemo.Pages;

public class LoginPageUpgraded
{
    private IPage _page;    
    public LoginPageUpgraded(IPage page) => _page = page;
    private ILocator _lnkLogin => _page.Locator("text=Login");
    private ILocator _txtUserName => _page.Locator("#UserName");
    private ILocator _txtPassword => _page.Locator("#Password");
    private ILocator _btnLogin => _page.Locator("text=Log in");
    private ILocator _lnkEmplyeeDetails => _page.Locator("text='Employee Details'");    
    private ILocator _lnkEmplyeeList => _page.Locator("text='Employee List'");


    public async Task ClickLogin()
    {
        await _page.RunAndWaitForNavigationAsync( async () => 
        {
            await _lnkLogin.ClickAsync();
        }, new PageRunAndWaitForNavigationOptions
        {
            UrlString = "**/Login"
        });
    }
    
    public async Task ClickEmployeeList() => await _lnkEmplyeeList.ClickAsync();
    
    public async Task Login(string userName, string password)
    {
        await _txtUserName.FillAsync(userName); 
        await _txtPassword.FillAsync(password);
        await _btnLogin.ClickAsync();
    }

    public async Task<bool> IsEmployeeDetailsExsists() => await _lnkEmplyeeDetails.IsVisibleAsync();
}

