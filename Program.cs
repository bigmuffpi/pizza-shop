using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizza_shop
{
    class Program
    {
        static void Main(string[] args)
        {

            //hard coding items list
            string[,] menu = new string[10, 2]
            {
                {"pizza", "1.50"}, {"soda", "1.00"}, {"french fries", "2.50"}, {"mozzarella sticks", "5.00"}, {"cheesey bread", "6.00"}, {"undercooked burgers", " 5.00"},
                {"garlic knots", "1.00"}, {"empanadas", "2.50"}, {"chicken wings", "5.00"}, {"pasta", "7.00"}
            };

            List<string[,]> cart = new List<string[,]>(); 

            displayGreeting();

            float moneyAmount = getMoney();
            bool enoughMoney;

            //no money gg program
            if (moneyAmount == -1)
            {
                Console.WriteLine("Program over!");
                Console.ReadLine();
            }
            //normal running
            else
            {
                Console.WriteLine("money amount: {0:c}\n", moneyAmount);
                float total = 0;
                
                //main loop
                while(true)
                {
                    displayMenu(menu);
                    cart = addToCart(cart, menu);
                    float priceToAdd = float.Parse(cart[cart.Count - 1][0, 1]);
                    //Console.WriteLine(cart[0][0, 0]); food
                    //Console.WriteLine(cart[0][0, 1]); price
                    total = total + priceToAdd;
                    Console.WriteLine("Adding total...");

                    foreach (string[,] pair in cart)
                    {
                        //prints all the prices
                        Console.WriteLine("{0:c}",float.Parse(pair[0,1]));
                    }

                    Console.WriteLine("Total: {0:c}\n", total);

                    Console.Write("Do you wish to add more to your cart? Enter 1 to continue, 0 to cash out: ");
                    int response = int.Parse(Console.ReadLine());

                    while (response != 1 && response != 0)
                    {
                        Console.WriteLine("Enter valid response, 1 to continue, 0 to cash out: ");
                        response = int.Parse(Console.ReadLine());
                    }
                    if (response == 0)
                    {
                        float remaining = moneyAmount - total;
                        enoughMoney = cashOut(remaining);

                        if (enoughMoney)
                        {
                            Console.WriteLine("Here is your change!");
                            Console.WriteLine("{0:c}", remaining);
                            break;
                        }
                        else
                        {
                            while (!enoughMoney)
                            {
                                Console.WriteLine("Not enough money!");
                                Console.WriteLine("You only have {0:c}!", moneyAmount);
                                Console.WriteLine("Please remove some items from cart.");
                                cart = removeItems(cart);

                                //recalculate total
                                float newTot = 0;

                                foreach (string[,] pair in cart)
                                {
                                    newTot = newTot + float.Parse(pair[0, 1]);
                                }
                                Console.WriteLine("Your current total is now {0:c}\n", newTot);

                                remaining = moneyAmount - newTot;
                                enoughMoney = cashOut(remaining);
                            }
                            //you now have enough money, end the loop and cashout
                            Console.WriteLine("Here is your change!");
                            Console.WriteLine("{0:c}", remaining);
                            break;
                        }
                    }
                }
                Console.WriteLine("Thanks for stopping by!");
                Console.ReadLine();
            }   
        }

        static void displayGreeting()
        {
            Console.WriteLine("Welcome to KFig's Pizza Shop!");
        }

        static float getMoney()
        {

            Console.Write("Please enter the amount of money you have: ");
            float money = float.Parse(Console.ReadLine());

            if (money <= 0)
            {
                Console.WriteLine("You have no money. Later scrub.");
                return -1;
            }

            return money;
        }

        static void displayMenu(string[,] menu)
        {

            int priceIndex = 1;
            for (int i = 0; i < menu.GetLength(0); i++)
            {
                Console.Write("Item {1}: {0} ", menu[i, 0], i+1);
                Console.WriteLine("Price: {0}", menu[i,priceIndex]);
            }
        }

        static List<string[,]> addToCart(List<string[,]> cart, string [,] menu)
        {
            Console.Write("\nPlease enter the item you want to add to your cart: ");
            int item = int.Parse(Console.ReadLine());
            
            while(item < 1 || item > 10)
            {
                Console.Write("Please enter a valid item: ");
                item = int.Parse(Console.ReadLine());
            }
            
            //Console.WriteLine(menu[item-1, 0]); pizza 
            //Console.WriteLine(menu[item-1, 1]); 1.50

            string food = menu[item - 1, 0];
            string price = menu[item - 1, 1];

            string[,] tmp = new string[1, 2]
            {
                {food,price}
            };

            cart.Add(tmp);
            
            return cart;
        }

        static bool cashOut(float remaining)
        {
            
            if (remaining >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        static List<string[,]> removeItems(List<string[,]> cart)
        {
            int i = 0;
            int toRemove;

            foreach (string[,] pair in cart)
            {
                ++i;
                Console.WriteLine("Item {0}: {1} Price: {2}",i, pair[0,0], pair[0,1]);
            }
            Console.Write("Enter the item number that you wish to remove: ");
            toRemove = int.Parse(Console.ReadLine());

            while(toRemove <= 0 || toRemove > i)
            {
                Console.Write("Please enter a valid item: ");
                toRemove = int.Parse(Console.ReadLine());
            }
            cart.RemoveAt(toRemove-1);
            return cart;
        }
    }
}
