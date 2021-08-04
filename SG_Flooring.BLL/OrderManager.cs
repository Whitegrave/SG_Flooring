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

        public OrderManager(IOrderRepo orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public GetOrderResponse GetOrder(string orderDate, int orderNumber)
        {
            GetOrderResponse response = new GetOrderResponse();

            // Determine if provided date is valid
            CheckDateResponse dateReponse = _orderRepository.CheckDate(orderDate);
            if (!dateReponse.Success)
            {
                response.Success = false;
                response.Message = dateReponse.Message;
                return response;
            }

            // Check if order provided exists in file
            response.Order = _orderRepository.GetOrder(dateReponse.Date.ToString(), orderNumber);

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

        //public AccountDepositResponse Deposit(string accountNumber, decimal amount)

        //public AccountWithdrawResponse Withdraw(string accountNumber, decimal amount)
    }
}
