using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaShop
{
    class Program
    {
        PizzaShopContext context;
        User activeUser = new();
        Order order;
        OrdersDetail ordersDetail;
        int totalPrice = 0;
        public Program()
        {
            context = new PizzaShopContext();

        }
        public void Registration()
        {
            Console.WriteLine("Registration new user.");
            Console.WriteLine("Please enter e-mail");
            string email = Console.ReadLine();
            Console.WriteLine("Please enter Password");
            string pass = Console.ReadLine();

            if (context.Users.Any(e => e.UserEmail == email))
            {
                Console.WriteLine("User with this email already exist");
                Login();
            }
            else
            {
                User newUser = new User() { UserEmail = email, Password = pass };
                Console.WriteLine("Please enter your name");
                newUser.Name = Console.ReadLine();
                Console.WriteLine("Please enter your address");
                newUser.Address = Console.ReadLine();
                Console.WriteLine("Please enter your phone number");
                newUser.Phone = Console.ReadLine();
                activeUser = newUser;
                context.Users.Add(newUser);
                context.SaveChanges();
                Console.WriteLine("New user added. Welcome in PizzaShop");
            }
        }
        public void Login()
        {
            User user = new();
            string pass;
            do
            {
                Console.WriteLine("Login user.");
                Console.WriteLine("Please enter e-mail");
                string email = Console.ReadLine();
                Console.WriteLine("Please enter Password");
                pass = Console.ReadLine();
                if (context.Users.Any(e => e.UserEmail == email))
                {
                    user = context.Users.Where(u => u.UserEmail == email).FirstOrDefault();
                    if (user.Password == pass)
                    {

                        Console.WriteLine("Welcome in PizzaShop");
                        activeUser = user;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect Email/Password Combination. Please try again");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect Email/Password Combination. Please try again");
                }
            } while (user.Password != pass);
        }
        public void LoginMenu()
        {
            string choise = "";
            do
            {
                Console.WriteLine("Pizza ordering system");
                Console.WriteLine("Please login or register");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Registration();
                        break;
                    default:
                        Console.WriteLine("Incorrect enter. Please try again");
                        break;
                }
            } while (activeUser.UserEmail == null);
            order = new Order() { UserId = activeUser.UserEmail };
            context.Orders.Add(order);
            context.SaveChanges();
        }


        public void PizzaChoise()
        {
            int pNumber;
            Console.WriteLine("The following are the pizza that are available for ordering");
            List<Pizza> pizzaList = context.Pizzas.ToList();
            Console.WriteLine($"Number    Name  	Price      Type");
            foreach (var item in pizzaList)
            {
                Console.WriteLine($"{item.PizzaNumber}         {item.Name}      ${item.Prise}        {item.Type}");
            }
            Console.WriteLine("Enter the Pizza of your choice");
            pNumber = GetIntNumber();
            if (context.Pizzas.Any(e => e.PizzaNumber == pNumber))
            {
                Pizza pizza = context.Pizzas.Where(e => e.PizzaNumber == pNumber).FirstOrDefault();
                Console.WriteLine($"You have selected {pizza.Name} for	${pizza.Prise}");
                totalPrice += (int)pizza.Prise;
                ordersDetail = new() { PizzaNumber = pizza.PizzaNumber, OId = order.OId };
                context.OrdersDetails.Add(ordersDetail);
                context.SaveChanges();
                Console.WriteLine("Total $" + totalPrice);
            }
            else
            {
                Console.WriteLine("There is no pizza with this number");
            }
        }
        public void TopingChoise()
        {
            int tNumber;
            Console.WriteLine("Do u want extra toppings?y/n");
            List<Toping> topingList = context.Topings.ToList();
            string choise = Console.ReadLine().ToLower();
            do
            {

                if (choise == "y")
                {
                    Console.WriteLine("The folowing are teh toppings");
                    Console.WriteLine($"Number    Name  	Price");
                    foreach (var item in topingList)
                    {
                        Console.WriteLine($"{item.ToppingNumber}         {item.Name}      {item.Prise}");
                    }
                    Console.WriteLine("Select the topping");
                    tNumber = GetIntNumber();
                    if (context.Topings.Any(e => e.ToppingNumber == tNumber))
                    {
                        Toping toping = context.Topings.Where(e => e.ToppingNumber == tNumber).FirstOrDefault();
                        totalPrice += (int)toping.Prise;
                        Console.WriteLine($"You have selected {toping.Name} for	${toping.Prise} fo total {totalPrice}");
                        OrderItemDetail orderItemDetail = new() { ItemNumber = ordersDetail.ItemNumber, ToppingNumber = toping.ToppingNumber };
                        context.OrderItemDetails.Add(orderItemDetail);
                        context.SaveChanges();
                        orderItemDetail = null;
                        Console.WriteLine("Do u wnat one more toppings?y/n");
                        choise = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        Console.WriteLine("There is no toping with this number");
                    }
                }
            } while (choise == "y");
        }

        public void PizzaMenu()
        {
            string pChoise = "";
            do
            {
                PizzaChoise();
                TopingChoise();
                Console.WriteLine("Do you want to select another pizza for this order?y/n");
                pChoise = Console.ReadLine().ToLower();
            } while (pChoise == "y");
            order.Totall = totalPrice;
            context.Orders.Update(order);
            context.SaveChanges();
        }

        void FinalOutput()
        {
            List<Toping> topingsList;
            Console.WriteLine("Your order summary");
            List<OrdersDetail> finalOrdDtail = context.OrdersDetails.Where(e => e.OId == order.OId).ToList();
            List<Pizza> pList = new();

            for (int i = 0; i < finalOrdDtail.Count; i++)
            {
                Pizza pizza = (context.Pizzas.Where(e => e.PizzaNumber == finalOrdDtail[i].PizzaNumber).FirstOrDefault());
                List<OrderItemDetail> oad = context.OrderItemDetails.Where(e => e.ItemNumber == finalOrdDtail[i].ItemNumber).ToList();
                List<Toping> tList = new();
                Console.WriteLine("Pizza " + (i + 1));
                Console.WriteLine($"{i + 1}    {pizza.Name}   ${pizza.Prise}    {pizza.Type}");
                foreach (var details in oad)
                {
                    tList.Add(context.Topings.Where(e => e.ToppingNumber == details.ToppingNumber).FirstOrDefault());
                }
                Console.WriteLine("Toppings");
                if (tList.Count != 0)
                {
                    for (int j = 0; j < tList.Count; j++)
                    {
                        Console.WriteLine($"{j + 1}    {tList[j].Name}   ${tList[j].Prise}");
                    }
                }else
                    Console.WriteLine("Nothing");
            }
            

        }
        void PayDeliveryOutput()
        {
            int delivery = 0;
            string confirm = "";
            if (totalPrice < 25)
            {
                totalPrice += 5;
                delivery = 5;
                order.Totall = totalPrice;
                context.Orders.Update(order);
                context.SaveChanges();
            }
            Console.WriteLine($"Total price - ${totalPrice}");
            Console.WriteLine($"Delivery cost - ${delivery}");
            Console.WriteLine("Note- delivery cost of $5 will be added for order less than $25");
            Console.WriteLine("Please confirm your order y/n?");
            confirm = Console.ReadLine().ToLower();
            if(confirm == "y")
            {
                Console.WriteLine("The order will be delivered to address");
                Console.WriteLine("-----------------");
                Console.WriteLine($"{activeUser.Address}");
                Console.WriteLine($"Name: {activeUser.Name}, phone: {activeUser.Phone}");
                Console.WriteLine("Please pay on delivery");
                Console.WriteLine("Thank you");
            }
            else
            {
                Console.WriteLine("Order was canselled. Bye");
            }
        }

        static void Main(string[] args)
        {
            Program program = new();

            program.LoginMenu();
            program.PizzaMenu();
            program.FinalOutput();
            program.PayDeliveryOutput();

        }

        public int GetIntNumber()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Invalid entry. Please enter again");
            }
            return num;
        }
        void PrintMenuList()
        {
            Console.WriteLine("1. List All Products");
            Console.WriteLine("2. Exit");
        }
    }
}
