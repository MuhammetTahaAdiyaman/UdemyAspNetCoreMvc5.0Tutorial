using System.Collections.Generic;

namespace UdemyAspNetCore.Models
{
    public class NewsContext
    {
        public static List<News> NewsList = new List<News>()
        {
            new News{Title = "Haber 1"},
            new News{Title = "Haber 2"},
            new News{Title = "Haber 3"},
            new News{Title = "Haber 4"},
            new News{Title = "Haber 5"},

        };
    }
}
