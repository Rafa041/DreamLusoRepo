using DreamLuso.Application.CQ.Categories.Queries.Retrieve;

namespace DreamLuso.Application.CQ.Categories.Queries.RetrieveAllCategories;

public class RetrieveAllCategoriesResponse
{
    public IEnumerable<RetrieveCategoryResponse> Categories { get; set; }
}