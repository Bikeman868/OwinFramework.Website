using OwinFramework.Pages.Core.Attributes;

namespace Website.Pages
{
    //------------------------------------------------------------------------------------
    // Default page styles

    [IsComponent("defaultStyles")]
    [PartOf("application")]
    [DeployedAs("content")]
    [DeployCss("body", "margin:10px; background-color: whitesmoke;", 1)]
    [DeployCss("p", "font-size:11pt;", 2)]
    [DeployCss("h1", "font-size:16pt;", 3)]
    [DeployCss("h2", "font-size:14pt;", 4)]
    [DeployCss("h3", "font-size:12pt;", 5)]
    [DeployCss("div.{ns}_header-region", "height: 120px; width:100%; background: gray; color: whitesmoke; clear: both;", 6)]
    [DeployCss("div.{ns}_header-region h1", "font-size: 3em; padding: 20px 0px 0px 30px;", 7)]
    [DeployCss("div.{ns}_footer-region", "height: 50px; width:100%; padding:5px; background: gray; color: whitesmoke;", 8)]
    internal class DefaultStylesComponent { }

    //------------------------------------------------------------------------------------
    // Blank master page

    [PartOf("application")]
    [NeedsComponent("defaultStyles")]
    public class BlankMasterPage
    {
    }
}