using SG_Flooring.Models;
using SG_Flooring.Models.Interfaces;
using SG_Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_Flooring.BLL
{
    public class OrderManager
    {
        private IOrderRepo _orderRepository;
        private IStateRepo _stateRepo;
        private IProductRepo _prodRepo;

        public OrderManager(IOrderRepo orderRepository, IStateRepo stateRepo, IProductRepo prodRepo)
        {
            _orderRepository = orderRepository;
            _stateRepo = stateRepo;
            _prodRepo = prodRepo;
        }
        
        public GetOrderResponse LookupOrder(string orderDate, int orderNumber)
        {
            GetOrderResponse response = new GetOrderResponse();

            // Check if order provided exists in file
            response.Order = _orderRepository.GetOrderFromFile(orderDate, orderNumber);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = $"An Order number {orderNumber} was not found for date {orderDate}...";
            }
            else
            {
                response.Success = true;
                response.Message = $"Order found.";
            }

            return response;
        }

        // Method to allow workflow to validate date entry without reaching into repo/data
        public CheckDateResponse ValidateDate(string orderDate)
        {
            CheckDateResponse response = new CheckDateResponse();
            return _orderRepository.CheckDate(orderDate);
        }

        public Response SaveOrder(Order saveMe)
        {
            // Attempt to save, return result
            return _orderRepository.SaveOrderToFile(saveMe);
        }

        public void DeleteOrder(Order removeMe)
        {
            _orderRepository.DeleteOrderFromFile(removeMe);
        }

        // Return State list for displaying to user
        public List<State> GetStatesFromRepo()
        {
            return _stateRepo.GetStates();
        }

        // Return Product list for displaying to user
        public List<Product> GetProductsFromRepo()
        {
            return _prodRepo.GetProducts();
        }
    }
}
