using System.Linq.Expressions;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using FluentValidation;
using FluentValidation.Results;

namespace BusinessLogicLayer.Services{
    public class ProductService : IProductsService
    {
        private readonly IValidator<ProductAddRequest> _productAddRequestValidator;
        private readonly IValidator<ProductUpdateRequest> _productUpdateRequestValidator;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductService(IValidator<ProductAddRequest> productAddRequestValidator,IValidator<ProductUpdateRequest> productUpdateRequestValidator, IMapper mapper, IProductRepository repository){
            _productAddRequestValidator = productAddRequestValidator;
            _productUpdateRequestValidator = productUpdateRequestValidator;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ProductResponse?> AddProduct(ProductAddRequest productAddRequest)
        {
            if(productAddRequest == null)
            {
                throw new ArgumentNullException(nameof(productAddRequest));
            }

            ValidationResult result =  await _productAddRequestValidator.ValidateAsync(productAddRequest);

            if(!result.IsValid)
            {
                 string errors = String.Join(", ",result.Errors.Select(temp => temp.ErrorMessage));
                 throw new ArgumentNullException(errors);
            }



            Product product = await _repository.AddProduct(_mapper.Map<Product>(productAddRequest));
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<bool> DeleteProduct(Guid productID)
        {
             Product? existingProduct = await _repository.GetProductByCondition(temp => temp.ProductId == productID);

                if (existingProduct == null)
                {
                return false;
                }

                //Attempt to delete product
                bool isDeleted = await _repository.DeleteProduct(productID);
                return isDeleted;
        }

        public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> conditionExpression)
  {
    Product? product = await _repository.GetProductByCondition(conditionExpression);
    if (product == null)
    {
      return null;
    }

    ProductResponse productResponse = _mapper.Map<ProductResponse>(product); //Invokes ProductToProductResponseMappingProfile
    return productResponse;
  }


  public async Task<List<ProductResponse?>> GetProducts()
  {
    IEnumerable<Product?> products = await _repository.GetProducts();
    

    IEnumerable<ProductResponse?> productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products); //Invokes ProductToProductResponseMappingProfile
    return productResponses.ToList();
  }


  public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> conditionExpression)
  {
    IEnumerable<Product?> products = await _repository.GetProductsByCondition(conditionExpression);

    IEnumerable<ProductResponse?> productResponses = _mapper.Map<IEnumerable<ProductResponse>>(products); //Invokes ProductToProductResponseMappingProfile
    return productResponses.ToList();
  }


  public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest productUpdateRequest)
  {
    Product? existingProduct = await _repository.GetProductByCondition(temp => temp.ProductId == productUpdateRequest.ProductId);

    if(existingProduct == null)
    {
      throw new ArgumentException("Invalid Product ID");
    }


    //Validate the product using Fluent Validation
    ValidationResult validationResult = await _productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

    // Check the validation result
    if (!validationResult.IsValid)
    {
      string errors = string.Join(", ", validationResult.Errors.Select(temp => temp.ErrorMessage)); //Error1, Error2, ...
      throw new ArgumentException(errors);
    }


    //Map from ProductUpdateRequest to Product type
    Product product = _mapper.Map<Product>(productUpdateRequest); //Invokes ProductUpdateRequestToProductMappingProfile

    Product? updatedProduct = await _repository.UpdateProduct(product);

    ProductResponse? updatedProductResponse = _mapper.Map<ProductResponse>(updatedProduct);

    return updatedProductResponse;
  }
    }
}