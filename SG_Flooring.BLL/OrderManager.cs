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

        public GetOrderResponse LookupOrder(string orderDate, int orderNumber)
        {
            GetOrderResponse response = new GetOrderResponse();

            // Check if order provided exists in file
            response.Order = _orderRepository.GetOrder(orderDate, orderNumber);

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

        // Passhtrough method to allow workflow to check date entry without reaching into repo/data
        public CheckDateResponse ValidateDate(string orderDate)
        {
            CheckDateResponse response = new CheckDateResponse();
            return _orderRepository.CheckDate(orderDate);
        }

        //public AccountDepositResponse Deposit(string accountNumber, decimal amount)

        //public AccountWithdrawResponse Withdraw(string accountNumber, decimal amount)
    }
}
