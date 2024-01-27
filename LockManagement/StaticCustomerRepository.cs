using LockManagement.Classes;

namespace LockManagement
{
    public static class StaticCustomerRepository
    {
        public static  List<CustomerBalance> customerBalances  = new List<CustomerBalance>
        {
            new CustomerBalance { CustomerNumber = "1", CardNumber = "1111111111111111", Balance = 2000.0m },
            new CustomerBalance { CustomerNumber = "1", CardNumber = "4444444444444444", Balance = 2000.0m },
            new CustomerBalance { CustomerNumber = "2", CardNumber = "2222222222222222", Balance = 2000.0m },
            new CustomerBalance { CustomerNumber = "3", CardNumber = "3333333333333333", Balance = 2000.0m },
        };
    }
}
