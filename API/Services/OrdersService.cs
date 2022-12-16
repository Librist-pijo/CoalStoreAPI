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

        public ResultData CreateOrders(CreateOrdersDTO ordersDTO)
        {
            ResultData result = new();

            try
            {
                Orders orders = _ordersFactory.CreateOrders(ordersDTO);
                var customer = _customersRepository.GetCustomerById(ordersDTO.CustomerId).GetAwaiter().GetResult();
                if (customer.AddressLine1 is not null)
                {
                    orders.ShippingAddress = $"{customer.AddressLine1}, {customer.PostCode} {customer.AddressLine2}";
                }
                var validation = _ordersValidator.ValidateCreateAsync(orders).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                var id = _ordersRepository.Create(orders).GetAwaiter().GetResult();
                var createInvoiceResult = CreateInvoice(ordersDTO.Products, id);
                if (!createInvoiceResult.Success)
                {
                    _ordersRepository.Delete(id);
                    result.Success = false;
                    return result;
                }

                var createOPResult = CreateOrdersProducts(ordersDTO.Products, id);
                if (!createOPResult.Success)
                {
                    _ordersRepository.Delete(id);
                    result.Success = false;
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

        public ResultData DeleteOrders(int orderId)
        {
            ResultData result = new();

            try
            {
                var validation = _ordersValidator.ValidateDeleteAsync(orderId).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _ordersProductsService.DeleteOrdersProducts(orderId);
                _ordersRepository.Delete(orderId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }
            return result;
        }

        public Orders GetOrderById(int orderId)
        {
            var order = _ordersRepository.GetById(orderId).GetAwaiter().GetResult();
            return order;
        }

        public List<Orders> GetOrders()
        {
            var order = _ordersRepository.Get().GetAwaiter().GetResult();
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

        public ResultData UpdateOrders(UpdateOrdersDTO ordersDTO)
        {
            ResultData result = new();

            try
            {
                Orders orders = _ordersFactory.UpdateOrders(ordersDTO);
                var validation = _ordersValidator.ValidateUpdateAsync(orders).GetAwaiter().GetResult();
                if (!validation)
                {
                    result.Error = "Błąd walidacji";
                    return result;
                }
                _ordersRepository.Update(orders);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
            }

            return result;
        }

        private ResultData CreateOrdersProducts(List<CreateOrdersProductsDTO> ordersProductsDTO, int orderId)
        {
            ResultData result = new();
            try
            {
                foreach (var product in ordersProductsDTO)
                {
                    product.OrderId = orderId;
                    result = _ordersProductsService.CreateOrdersProducts(product);
                    if (!result.Success)
                    {
                        return result;
                    }

                    Products products = _productsService.GetProductById(product.ProductId);
                    products.Stock = products.Stock - product.Quantity;
                    UpdateProductsDTO updateProductsDTO = new UpdateProductsDTO
                    {
                        Id = products.Id,
                        Name = products.Name,
                        Stock= products.Stock,
                        Price= products.Price
                    };
                    result = _productsService.UpdateProducts(updateProductsDTO);
                    if (!result.Success)
                    {
                        updateProductsDTO.Stock= products.Stock+product.Quantity;
                        _productsService.UpdateProducts(updateProductsDTO);
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
        private ResultData CreateInvoice(List<CreateOrdersProductsDTO> ordersProductsDTO, int orderId)
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

                    Products products = _productsService.GetProductById(product.ProductId);
                    createInvoicesDTO.Amount += products.Price;
                }
                result = _invoicesService.CreateInvoices(createInvoicesDTO);
                if (!result.Success)
                {
                    _invoicesService.DeleteInvoices(orderId);
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

    }
}
