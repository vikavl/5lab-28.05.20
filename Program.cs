using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Lab5_Vlasenko_Viktoria_IS_81_Var4
{
    public class Client
    {
        public int id;
        public string name;
        public int age;
        public Client(int id, string name, int age)
        {
            this.id = id;
            this.name = name;
            this.age = age;
        }
        public override string ToString()
        {
            return "(id = " + this.id.ToString() + "; name = " + this.name + "; age = " + this.age.ToString() + ")";
        }
    }
    public class Computer
    {
        public int id;
        public string name;
        public double price;
        public double power;
        public Client client;
        public Computer(int id, string name, double price, double power, Client client)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.power = power;
            this.client = client;
        }
        public string Print(List<Client> list)
        {
            string str = "";
            foreach (Client p in list)
            {
                str += "\n\t\t" + p.ToString();
            }
            return str;
        }
        public override string ToString()
        {
            string str = "(id = " + this.id.ToString() + "; name = '" + this.name + "'; price = " + this.price.ToString() + "; power = " + this.power.ToString() + "; "+ client.ToString() + ")";
            return str;
        }
    }
    [Serializable]
    public class XMLComputer
    {
        public string name { get; set; }
        public double price { get; set; }
        public double power { get; set; }
        public XMLClient client { get; set; }

    }
    [Serializable]
    public class XMLClient
    {
        public string name { get; set; }
        public int age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            XDocument xdoc = new XDocument();
            XElement xcomputers = new XElement("computers");
            Computer computer;
            Console.Write("Enter number of computer you want to write: ");
            int comp_count = Convert.ToInt16(Console.ReadLine());
            string comp_name;
            double comp_price;
            double comp_power;

            string person_name;
            int person_age;
            //entering computer info
            for (int i = 0; i < comp_count; i++)
            {
                Console.Write($"\nEnter name of {i+1} computer: ");
                comp_name = Console.ReadLine();
                Console.Write($"Enter price of {i + 1} computer: ");
                comp_price = Convert.ToDouble(Console.ReadLine());
                Console.Write($"Enter power of {i+1} computer: ");
                comp_power = Convert.ToDouble(Console.ReadLine());
                Console.Write("\tEnter name of client: ");
                person_name = Console.ReadLine();
                Console.Write($"\tEnter age of client: ");
                person_age = Convert.ToInt16(Console.ReadLine());
                Client client = new Client(1, person_name, person_age);
                computer = new Computer(i + 1, comp_name,comp_price, comp_power, client); //write computer info to computer object
                //====================Creating XElements for XDocument=========================//
                XElement xcomputer = new XElement("computer",
                new XAttribute("name", computer.name),
                new XElement("price", computer.price),
                new XElement("power", computer.power));
                XElement xclient = new XElement("client",
                        new XAttribute("name", client.name),
                        new XElement("age", client.age));
                xcomputer.Add(xclient); //adding list of clients to computer element
                xcomputers.Add(xcomputer); //adding computer element to list of computers
                //Console.WriteLine(computer.ToString()); //output computer info
                
            }
            xdoc.Add(xcomputers);
            //xdoc.Add(xcomputers); //adding list of computers to XDocument
            xdoc.Save("computers.xml"); //saving XDocument
            
            XmlSerializer formatter = new XmlSerializer(typeof(XMLComputer[]));
            using (FileStream fs = new FileStream("computers.xml", FileMode.OpenOrCreate))
            {
                XMLComputer[] new_comp = (XMLComputer[])formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                foreach(XMLComputer c in new_comp)
                {
                    Console.WriteLine(c.name + " " + c.power.ToString() + " " + c.price.ToString() + " " + c.client.name);
                }
                //Console.WriteLine(new_comp.name + " " + new_comp.power.ToString() + " " + new_comp.price.ToString() + " " + new_comp.client.name);
            }
            Console.WriteLine("done");
        }
    }
}
