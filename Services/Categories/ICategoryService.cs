namespace Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AddCategoryResponseDto> AddAsync(AddCategoryRequestDto model, CancellationToken cancellationToken = default);
        Task<EditCategoryDto> EditCategoryAsync(EditCategoryDto model, CancellationToken cancellationToken = default);  
    }
}
