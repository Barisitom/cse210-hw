using System;
using System.Collections.Generic;

// ------------------- Address Class -------------------
class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.ToLower() == "usa";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

// ------------------- Customer Class -------------------
class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public string GetName()
    {
        return name;
    }

    public Address GetAddress()
    {
        return address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }
}

// ------------------- Product Class -------------------
class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public double GetTotalCost()
    {
        return price * quantity;
    }

    public string GetPackingInfo()
    {
        return $"{name} (ID: {productId})";
    }
}

// ------------------- Order Class -------------------
class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalPrice()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.GetTotalCost();
        }

        total += customer.LivesInUSA() ? 5 : 35;

        return total;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in products)
        {
            label += $"- {product.GetPackingInfo()}\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress().GetFullAddress()}";
    }
}

// ------------------- Main Program -------------------
class Program
{
    static void Main(string[] args)
    {
        // Order 1: USA Customer
        Address address1 = new Address("123 Main St", "Dallas", "TX", "USA");
        Customer customer1 = new Customer("Alice Smith", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Yoga Mat", "YM100", 20.00, 2));
        order1.AddProduct(new Product("Dumbbells", "DB200", 50.00, 1));

        // Order 2: International Customer
        Address address2 = new Address("78 Queen Street", "London", "England", "UK");
        Customer customer2 = new Customer("John Doe", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Protein Shake", "PS300", 30.00, 3));
        order2.AddProduct(new Product("Jump Rope", "JR400", 15.00, 1));

        // Display Order 1
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():0.00}\n");

        // Display Order 2
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():0.00}");
    }
}
