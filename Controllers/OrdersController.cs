using WebshopAPI.data.views;
using WebshopAPI.lib;
using WebshopAPI.lib.Services;

namespace WebshopAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : AdminBaseController
    {
        public OrdersManagerService ordersManagerService;

        public OrdersController(OrdersManagerService ordersManagerService)
        {
            this.ordersManagerService = ordersManagerService;
        }

        [HttpGet("Orders")]
        public IActionResult ListOrders()
        {
            APIResponse response = new APIResponse();
            try
            {
                List<v_OrderData> orders = ordersManagerService.ListOrders();
                response.Data = orders;
                response.StatusCode = 200;

                return Ok(response);
            } catch (Exception e)
            {
                response.StatusCode = 200;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderBody order)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (order == null)
                {
                    throw new BodyEmptyException();
                }

                ordersManagerService.CreateOrder(order);
                await "Új rendelés lett hozzáadva".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = order;

                return Ok(response);
            } 
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés tartalma űres";
            } 
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (order == null)
                {
                    throw new BodyEmptyException();
                }
                if (order.OrderID == 0)
                {
                    throw new MandatoryPropertyEmptyException("megrendelés azonosító");
                }

                ordersManagerService.UpdateOrder(order);
                await "Rendelés módosítva lett.".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = order;

                return Ok(response);
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = e.GetExceptionMessage();
            }
            catch (ItemNotExistsException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Az elem nem található az adatbázisban!";
            }
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés tartalma űres";
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }

        [HttpPost("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromBody] Order order)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (order == null)
                {
                    throw new BodyEmptyException();
                }
                if (order.OrderID == 0)
                {
                    throw new MandatoryPropertyEmptyException("megrendelés azonosító");
                }

                ordersManagerService.DeleteOrder(order.OrderID);
                await "Rendelés törölve lett.".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = order;

                return Ok(response);
            }
            catch (MandatoryPropertyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = e.GetExceptionMessage();
            }
            catch (ItemNotExistsException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "Az elem nem található az adatbázisban!";
            }
            catch (BodyEmptyException e)
            {
                response.StatusCode = e.statusCode;
                response.Message = "A kérés tartalma űres";
            }
            catch (Exception e)
            {
                response.StatusCode = 400;
                response.Message = e.GetExceptionMessage();
            }
            return BadRequest(response);
        }

        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder(int OrderID)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (OrderID == null || OrderID == 0)
                {
                    throw new MandatoryPropertyEmptyException("megrendelés azonosító");
                }

                Order order = ordersManagerService.GetOrder(OrderID);
                await "Megrendelés adatai sikeresen le lett kérve.".WriteInformationLogAsync(_CurrentUser);

                response.StatusCode = 200;
                response.Data = order;

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
