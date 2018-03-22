using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Portal.Modules.OrientalSails.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository() : base() { }

        public CustomerRepository(ISession session) : base(session) { }

        public string CustomerGetNameByBookingId(int bookingId)
        {
            BookingRoom bookingRoomAlias = null;
            var customer = _session.QueryOver<Customer>()
                .JoinAlias(x => x.BookingRooms, () => bookingRoomAlias)
                .JoinQueryOver(() => bookingRoomAlias.Book)
                .Where(x => x.Id == bookingId)
                .Take(1).FutureValue<Customer>().Value;

            if (customer == null)
                return "";

            return customer.Fullname;
        }

        public int CustomerCountPaxByBookingId(int bookingId)
        {
            BookingRoom bookingRoomAlias = null;
            var customerCounting = _session.QueryOver<Customer>()
                .Fetch(x => x.BookingRooms).Eager
                .JoinAlias(x => x.BookingRooms, () => bookingRoomAlias)
                .Fetch(x => bookingRoomAlias.Book).Eager
                .JoinQueryOver(() => bookingRoomAlias.Book)
                .Where(x => x.Id == bookingId)
                .Select(Projections.RowCount())
                .FutureValue<int>().Value;

            return customerCounting;
        }

        public int CustomerCountAdult(int bookingId)
        {
            BookingRoom bookingRoomAlias = null;
            var customerAdultCounting = _session.QueryOver<Customer>()
                .Where(x => x.IsChild == false)
                .Fetch(x => x.BookingRooms).Eager
                .JoinAlias(x => x.BookingRooms, () => bookingRoomAlias)
                .Fetch(x => bookingRoomAlias.Book).Eager
                .JoinQueryOver(() => bookingRoomAlias.Book)
                .Where(x => x.Id == bookingId)
                .Select(Projections.RowCount())
                .FutureValue<int>().Value;

            return customerAdultCounting;
        }

        public void CustomerSaveOrUpdate(Customer customer)
        {
            SaveOrUpdate(customer);
        }
    }
}