using Domain.Attributes;
using Domain.Discounts;
using Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order
{
    [Auditable]
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; private set; }
        public DateTime OrderDate { get; private set; } = DateTime.Now;
        public OrderAddress Address { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> orderItems => _orderItems.AsReadOnly();

        public decimal DiscountAmount { get; private set; }
        public Discount AppliedDiscount { get; private set; }
        public int? AppliedDiscountId { get; private set; }

        public Order(string buyerId, OrderAddress address, PaymentMethod paymentMethod, List<OrderItem> orderItems, Discount discount)
        {
            BuyerId = buyerId;
            Address = address;
            PaymentMethod = paymentMethod;
            _orderItems = orderItems;
            if(discount != null)
            {
                AppliedDiscount = discount;
                AppliedDiscountId = discount.Id;
                DiscountAmount = discount.GetDiscountAmount(TotalPriceWithOutDiscount());
            }
        }
        // for ef
        public Order()
        {
            
        }
        public void PaymentDone()
        {
            PaymentStatus = PaymentStatus.Paid;
        }
        public void OrderDelivered()
        {
            OrderStatus = OrderStatus.Delivered;
        }
        public void OrderReturned()
        {
            OrderStatus = OrderStatus.Returned;
        }
        public void OrderCancelled()
        {
            OrderStatus = OrderStatus.Cancelled;
        }
        public int TotalPrice()
        {
            int totalPrice = _orderItems.Sum(p => p.UnitPrice * p.Units);
            if(totalPrice != 0 && AppliedDiscount != null)
                totalPrice -= AppliedDiscount.GetDiscountAmount(totalPrice);
            return totalPrice;
        }
        public int TotalPriceWithOutDiscount()
        {
            return _orderItems.Sum(p => p.UnitPrice * p.Units);
        }
        public void ApplyDiscountCode(Discount discount)
        {
            AppliedDiscount = discount;
            AppliedDiscountId = discount.Id;
            DiscountAmount = discount.GetDiscountAmount(TotalPriceWithOutDiscount());
        }
    }
    [Auditable]
    public class OrderItem
    {
        public int Id { get; set; }
        public ProductItem Product { get; set; }
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
        public int UnitPrice { get; private set; }
        public int Units { get; private set; }
        public OrderItem(int productId, string productName, string pictureUri, int unitPrice, int units)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUri = pictureUri;
            UnitPrice = unitPrice;
            Units = units;
        }
        // for ef
        public OrderItem()
        {
            
        }
    }
    public class OrderAddress
    {
        public string City { get; private set; }
        public string address { get; private set; }
        public int ZipCode { get; private set; }
        public string ReciverName { get; private set; }
        public void SetProperties(string reciverName, string address, string city, int zipCode)
        {
            ReciverName = reciverName;
            this.address = address;
            City = city;
            ZipCode = zipCode;
        }
    }
    public enum PaymentMethod 
    {
        /// <summary>
        /// پرداخت آنلاین
        /// </summary>
        OnlinePeyment = 0,
        /// <summary>
        /// پرداخت در محل
        /// </summary>
        PaymentOnTheSpot = 1
    }
    public enum PaymentStatus
    {
        /// <summary>
        /// منتظر پرداخت
        /// </summary>
        WaitingForPayment = 0,
        /// <summary>
        /// پرداخت انجام شده
        /// </summary>
        Paid = 1
    }
    public enum OrderStatus
    {
        /// <summary>
        /// درحال پردازش
        /// </summary>
        Processing = 0,
        /// <summary>
        /// تحویل داده شده
        /// </summary>
        Delivered = 1,
        /// <summary>
        /// مرجوعی
        /// </summary>
        Returned = 2,
        /// <summary>
        /// لغو شده
        /// </summary>
        Cancelled = 3
    }
}
