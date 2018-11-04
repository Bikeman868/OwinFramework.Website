﻿using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages
{
    [IsLayout("homePage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "layouts:null")]
    [LayoutRegion("panel2", "layouts:null")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class HomePageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("home", "right,left")]
    [RegionTemplate("left", "/content/general/home")]
    [RegionLayout("right", "homePage_RightColumn")]
    internal class HomePageLayout : FixedRightColumnLayout { }

    [IsPage("home", "/home")]
    [Route("/home", Methods.Get, Priority = 100)]
    [Route("/", Methods.Get, Priority = 100)]
    [PageTitle("OWIN Framework Home")]
    [RegionLayout("body", "home")]
    public class HomePage: NavigationMasterPage{ }
}