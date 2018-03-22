using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.BusinessLogic
{
    public class BookingPaymentBLL
    {
        public BookingRepository BookingRepository { get; set; }
        public TransactionRepository TransactionRepository { get; set; }
        public USDRateRepository USDRateRepository { get; set; }
        public BookingPaymentBLL()
        {
            BookingRepository = new BookingRepository();
            TransactionRepository = new TransactionRepository();
            USDRateRepository = new USDRateRepository();
        }

        public void Dispose()
        {
            if (BookingRepository != null)
            {
                BookingRepository.Dispose();
                BookingRepository = null;
            }

            if (TransactionRepository != null)
            {
                TransactionRepository.Dispose();
                TransactionRepository = null;
            }

            if (USDRateRepository != null)
            {
                USDRateRepository.Dispose();
                USDRateRepository = null;
            }
        }

        public Booking BookingGetById(int bookingId)
        {
            return BookingRepository.BookingGetById(bookingId);
        }

        public void TransactionSaveOrUpdate(Transaction transaction)
        {
            TransactionRepository.SaveOrUpdate(transaction);
        }

        public USDRate USDRateGetByDate(DateTime dateTime)
        {
            return USDRateRepository.USDRateGetByDate(dateTime);
        }

        public void BookingSaveOrUpdate(Booking booking)
        {
            BookingRepository.SaveOrUpdate(booking);
        }
    }
}