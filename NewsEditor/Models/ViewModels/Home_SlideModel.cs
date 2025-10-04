using AspNetCoreGeneratedDocument;
using NewsEditor.Models.DB;
namespace NewsEditor.Models.ViewModels
{
    public class Home_SlideModel
    {
        public News? Article { get; set; }
        public bool IsSlideActive { get; set; }
        public Home_SlideModel(News? article, bool isSlideActive) 
        {
            Article = article;
            IsSlideActive = isSlideActive;
        }
    }
}
