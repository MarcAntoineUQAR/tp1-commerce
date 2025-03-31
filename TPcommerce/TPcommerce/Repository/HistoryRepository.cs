using System;
using System.Collections.Generic;
using System.Linq;
using TPcommerce.Models;

namespace TPcommerce.Repository
{
    public class HistoryRepository
    {
        public GenericResponse<Bill> SaveBill(Bill bill)
        {
            try
            {
                using var context = new TpcommerceContext();
                context.Bills.Add(bill);
                context.SaveChanges();
                return new GenericResponse<Bill>(bill, "Facture sauvegardée avec succès.", true);
            }
            catch (Exception e)
            {
                return new GenericResponse<Bill>("Erreur lors de la sauvegarde de la facture: " + e.Message, false);
            }
        }
        public List<Bill> GetBillsByUser(string userId)
        {
            using var context = new TpcommerceContext();
            return context.Bills.Where(b => b.OwnerId == userId).ToList();
        }
    }
}
