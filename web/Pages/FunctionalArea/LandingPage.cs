﻿using OwinFramework.Pages.Core.Attributes;
using OwinFramework.Pages.Core.Enums;
using Website.Content;
using Website.Content.Layouts.PageStructure;
using Website.PageBase;

namespace Website.Pages.FunctionalArea
{
    [IsLayout("areaLandingPage_Header", "title,detail")]
    [RegionTemplate("detail", "/data/functionalarea")]
    internal class LandingPageHeaderLayout : DocumentHeadLayout { }

    [IsLayout("areaLandingPage_LeftColumn", "header,body")]
    [LayoutRegion("header", "layouts:null")]
    [LayoutRegion("body", "layouts:null")]
    [RegionLayout("header", "areaLandingPage_Header")]
    [RegionComponent("body", "content__template")]
    internal class LandingPageLeftColumnLayout : ContentElement { }

    [IsLayout("areaLandingPage_RightColumn", "panel1,panel2")]
    [LayoutRegion("panel1", "layouts:null")]
    [LayoutRegion("panel2", "layouts:null")]
    [RegionLayout("panel1", "functional_area__list")]
    [RegionLayout("panel2", "project__list")]
    internal class LandingPageRightColumnLayout : FixedRightColumnLayout { }

    [IsLayout("areaLanding", "right,left")]
    [RegionLayout("left", "areaLandingPage_LeftColumn")]
    [RegionLayout("right", "areaLandingPage_RightColumn")]
    internal class LandingPageLayout : FixedRightColumnLayout { }

    [IsPage("areaLanding")]
    [Route("/content/area/**", Methods.Get, Priority = -80)]
    [PageTitle("OWIN Framework")]
    [RegionLayout("body", "areaLanding")]
    public class LandingPage: NavigationMasterPage{ }
}