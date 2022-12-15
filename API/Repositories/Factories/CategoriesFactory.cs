using API.ModelsDTO.CategoriesDTO;
using API.Repositories.Models;

namespace API.Repositories.Factories
{
    public class CategoriesFactory
    {
        public Categories CreateCategories(CreateCategoriesDTO categoriesDTO)
        {
            Categories newCategory = new Categories
            {
                Name = categoriesDTO.Name
            };
            return newCategory;
        }
        public Categories UpdateCategories(UpdateCategoriesDTO categoriesDTO)
        {
            Categories newCategory = new Categories
            {
                Id = categoriesDTO.Id,
                Name = categoriesDTO.Name
            };
            return newCategory;
        }
    }
}
