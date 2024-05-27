using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IPurchaseRepository
    {
        Purchase Create(Purchase purchase);
        List<Purchase> GetAll();
        List<Purchase> FindPurchasesByMonthAndYear(int month, int year);
    }
}
