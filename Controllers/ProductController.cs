using Microsoft.IdentityModel.Tokens;
using WebshopAPI.data;
using WebshopAPI.lib;
using WebshopAPI.lib.Services;

namespace WebshopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : AdminBaseController
    {
        private ProductManagerService productManagerService;

        public ProductController(ProductManagerService productManagerService)
        {
            this.productManagerService = productManagerService;
        }

        [HttpGet("Products")]
        public IActionResult GetProducts()
        {
            APIResponse response = new APIResponse();
            List<Product> products = productManagerService.ListProducts();

            response.StatusCode = 200;
            response.Data = products;

            return Ok(response);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (product == null)
                {
                    throw new BodyEmptyException();
                }

                ObjectValidatorService<Product> validator = new ObjectValidatorService<Product>(product);
                validator.IsValid();
                productManagerService.CreateProduct(product);
                await "Új termék sikeresen rögzítve lett.".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = product;

                return Ok(response);
            }
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés tartalma üres!";
            }
            catch (ItemAlreadyExistsException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez az elem már létezik az adatbázisban!";
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = e.GetExceptionMessage();
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }
        
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (product == null)
                {
                    throw new BodyEmptyException();
                }

                if (product.ProductID == null || product.ProductID == 0)
                {
                    throw new MandatoryPropertyEmptyException("termék azonosító");
                }

                productManagerService.UpdateProduct(product);
                await "Termék sikeresen frissítve lett.".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = product;

                return Ok(response);
            }
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés tartalma üres!";
            }
            catch (ItemNotExistsException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez az elem nem létezik az adatbázisban!";
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = e.GetExceptionMessage();
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }

        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromBody] Product product)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (product == null)
                {
                    throw new BodyEmptyException();
                }

                if (product.ProductID == null || product.ProductID == 0)
                {
                    throw new MandatoryPropertyEmptyException("termék azonosító");
                }

                productManagerService.DeleteProduct(product);
                await "Termék sikeresen törölve lett.".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = product;

                return Ok(response);
            }
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés tartalma üres!";
            }
            catch (ItemNotExistsException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez az elem nem létezik az adatbázisban!";
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = e.GetExceptionMessage();
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int ProductID)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (ProductID == null || ProductID == 0)
                {
                    throw new MandatoryPropertyEmptyException("termék azonosító");
                }

                Product product = productManagerService.GetProduct(ProductID);
                await "Termék adatai sikeresen le lett kérve.".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = product;

                return Ok(response);
            }
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés tartalma üres!";
            }
            catch (ItemNotExistsException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Ez az elem nem létezik az adatbázisban!";
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = e.GetExceptionMessage();
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }
    }
}
