using API.Enums;
using API.ModelsDTO;
using API.ModelsDTO.InvoicesDTO;
using API.ModelsDTO.Orders;
using API.ModelsDTO.OrdersProductsDTO;
using API.ModelsDTO.ProductsDTO;
using API.Repositories;
using API.Repositories.Factories;
using API.Repositories.Interfaces;
using API.Repositories.Models;
using API.Services.Interfaces;
using API.Validators;
using API.Validators.Interfaces;

namespace API.Services
{
    public class OrdersService : IOrdersService
    {
        protected readonly IOrdersRepository _ordersRepository;
        protected readonly IOrdersValidator _ordersValidator;
        protected readonly IProductsService _productsService;
        protected readonly ICustomersRepository _customersRepository;
        protected readonly IOrdersProductsService _ordersProductsService;
        protected readonly IInvoicesService _invoicesService;
        protected readonly InvoicesFactory _invoicesFactory;
        protected readonly OrdersFactory _ordersFactory;

        public OrdersService(IOrdersRepository OrdersRepository,
                             IOrdersValidator OrdersValidator,
                             IProductsService productsService,
                             ICustomersRepository customersRepository,
                             IOrdersProductsService ordersProductsService,
                             IInvoicesService invoicesService)
        {
            _ordersRepository = OrdersRepository;
            _ordersValidator = OrdersValidator;
            _ordersFactory = new OrdersFactory();
            _invoicesFactory= new InvoicesFactory();
            _productsService = productsService;
            _customersRepository = customersRepository;
            _ordersProductsService = ordersProductsService;
            _invoicesService = invoicesService;
        }

        public async Task<ResultData> CreateOrders(CreateOrdersDTO ordersDTO)
        {
            ResultData result = new();

            try
            {
                Orders orders = _ordersFactory.CreateOrders(ordersDTO);
                var customer = await _customersRepository.GetCustomerById(ordersDTO.CustomerId);
                if (customer.AddressLine1 is not null)
                {
                    orders.ShippingAddress = $"{customer.AddressLine1}, {customer.PostCode} {customer.AddressLine2}";
                }
                var validation = await _ordersValidator.ValidateCreateAsync(orders);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                var id = await _ordersRepository.Create(orders);
                var createInvoiceResult = await CreateInvoice(ordersDTO.Products, id);
                if (!createInvoiceResult.Success)
                {
                    await DeleteOrders(id);
                    result = createInvoiceResult;
                    return result;
                }

                var createOPResult = await CreateOrdersProducts(ordersDTO.Products, id);
                if (!createOPResult.Success)
                {
                    await DeleteOrders(id);
                    result = createOPResult;
                    return result;
                }
                result.Success = true;

            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        public async Task<ResultData> DeleteOrders(int orderId)
        {
            ResultData result = new();

            try
            {
                var validation = await _ordersValidator.ValidateDeleteAsync(orderId);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                await _invoicesService.DeleteInvoices(orderId);
                await _ordersProductsService.DeleteOrdersProducts(orderId);
                await _ordersRepository.Delete(orderId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public async Task<Orders> GetOrderById(int orderId)
        {
            var order = await _ordersRepository.GetById(orderId);
            return order;
        }

        public async Task<List<Orders>> GetOrders()
        {
            var order = await _ordersRepository.Get();
            return order;
        }

        public List<EnumDescriptionDTO<OrderState>> GetOrdersStates()
        {
            try
            {
                List<EnumDescriptionDTO<OrderState>> result = EnumService<OrderState>.GetEnumValues();
                return result;
            }
            catch (Exception ex)
            {
                return new List<EnumDescriptionDTO<OrderState>>();
            }
        }

        public async Task<ResultData> UpdateOrders(UpdateOrdersDTO ordersDTO)
        {
            ResultData result = new();

            try
            {
                Orders orders = _ordersFactory.UpdateOrders(ordersDTO);
                Orders ordersUpdate = await GetOrderById(orders.Id);
                orders.OrderDate = ordersUpdate.OrderDate;
                orders.CustomerId = ordersUpdate.CustomerId;
                var validation = await _ordersValidator.ValidateUpdateAsync(orders);
                if (!validation.Success)
                {
                    result = validation;
                    return result;
                }
                await _ordersRepository.Update(orders);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        private async Task<ResultData> CreateOrdersProducts(List<CreateOrdersProductsDTO> ordersProductsDTO, int orderId)
        {
            ResultData result = new();
            try
            {
                foreach (var product in ordersProductsDTO)
                {
                    product.OrderId = orderId;
                    result = await _ordersProductsService.CreateOrdersProducts(product);
                    if (!result.Success)
                    {
                        return result;
                    }

                    Products products = await _productsService.GetProductById(product.ProductId);
                    products.Stock = products.Stock - product.Quantity;
                    UpdateProductsDTO updateProductsDTO = new UpdateProductsDTO
                    {
                        Id = products.Id,
                        Name = products.Name,
                        Stock= products.Stock,
                        Price= products.Price
                    };
                    result = await _productsService.UpdateProducts(updateProductsDTO);
                    if (!result.Success)
                    {
                        updateProductsDTO.Stock= products.Stock+product.Quantity;
                        await _productsService.UpdateProducts(updateProductsDTO);
                        return result;
                    }
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }
        private async Task<ResultData> CreateInvoice(List<CreateOrdersProductsDTO> ordersProductsDTO, int orderId)
        {
            ResultData result = new();
            try
            {
                CreateInvoicesDTO createInvoicesDTO = new CreateInvoicesDTO
                {
                    OrderId = orderId,
                    State = 0,
                    PaymentMethod = 1
                };
                foreach (var product in ordersProductsDTO)
                {
                    product.OrderId = orderId;

                    Products products = await _productsService.GetProductById(product.ProductId);
                    createInvoicesDTO.Amount += (products.Price*product.Quantity);
                }
                result = await _invoicesService.CreateInvoices(createInvoicesDTO);
                if (!result.Success)
                {
                    await _invoicesService.DeleteInvoices(orderId);
                    return result;
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public Task<List<Orders>> GetOrdersByCustomerId(int customerId)
        {
            try
            {
                return _ordersRepository.GetOrdersByCustomerId(customerId);
            }
            catch(Exception ex)
            {
                return Task.FromResult(new List<Orders>());
            }
        }
    }
}
