using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGloves
{
    public class PageList
    {
        public List<Material> Materials { get; set; }
        public List<Material> OffsetMaterial => Materials.Skip(CurrentPage * CountInPage).Take(CountInPage).ToList();
        public int CountInPage { get; set; } = 15;
        public int CurrentPage { get; set; } = 0;
        public bool IsFirstPage => CurrentPage != 0;
        public bool IsLastPage => Materials.Count - ((CurrentPage + 2) * CountInPage) > -CountInPage;

        public int EndPage => Materials.Count / CountInPage;
        public PageList(List<Material> materials) => Materials = materials;
    }
}
