using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.PageBase;

namespace Website.Pages
{
    [IsLayout("index", "main")]
    [ZoneRegion("main", "layouts:null")]
    [ZoneComponent("main", "content_index")]
    internal class IndexPageLayout : ContentElement { }

    [IsPage("index")]
    [Route("/content/index/*", Method.Get, Priority = -10)]
    [PageTitle("OWIN Framework Index")]
    [ZoneLayout("body", "index")]
    public class IndexPage: NavigationMasterPage
    { 
    }
}