using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        #region Atributos/Propriedades
        private ICategoryRepository _categoruRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Construtor
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoruRepository = categoryRepository;
            _mapper = mapper;
        }
        #endregion

        #region Métodos Sobrescritos
        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categories = await _categoruRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var category = await _categoruRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task Add(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoruRepository.CreateAsync(category);
        }

        public async Task Update(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoruRepository.UpdateAsync(category);
        }

        public async Task Remove(int? id)
        {
            var category = _categoruRepository.GetByIdAsync(id).Result;
            await _categoruRepository.RemoveAsync(category);
        }
        #endregion
    }
}
