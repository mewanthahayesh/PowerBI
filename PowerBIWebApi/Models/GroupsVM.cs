
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebClientDemo.Models
{
    public class GroupsVM
    {
        public SelectListItem[] GroupFromList { get; set; }
        public SelectListItem[] GroupToList { get; set; }
        public SelectListItem[] GroupEmebdList { get; set; }
    }
}