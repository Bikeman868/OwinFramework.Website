using OwinFramework.Pages.Core.Attributes;

namespace Website.Content.Layouts.PageStructure
{
    //------------------------------------------------------------------------------------
    // This layout is used for the header region above document templates

    [IsRegion("documentHead_Title")]
    [Container("div", "{ns}_title")]
    internal class DocumentHeadTitleRegion : ContentElement { }

    [IsRegion("documentHead_Detail")]
    [Container("div", "{ns}_detail")]
    internal class DocumentHeadDetailRegion : ContentElement { }

    [LayoutRegion("title", "documentHead_Title")]
    [LayoutRegion("detail", "documentHead_Detail")]
    [RegionComponent("title", "text:verticalText")]
    [Container("div", "{ns}_document_head_layout")]
    internal class DocumentHeadLayout : ContentElement { }
}