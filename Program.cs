using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProgam
{
    class Calcs
    {
        //Here i set my var's that will be used to recall the information
        private double doubleBmi;
        private string stringBmi;
        private double energyNeeds;
        Input inputs = new Input();

        //This methos is made so that the program will simply just auto cacl everything so we don't need to ask ever cacl method to calculate a answer for us.
        public void startCacl(Input sendInputs)
        {
            inputs = sendInputs;
            bmiCacl();
            bmiFindString();
            checkGender();
        }

        //bmiCacl calculates the bmi and puts it into the doubleBmi varieble 
        private void bmiCacl()
        {
            doubleBmi = (inputs.GetWeight()) / (Math.Pow(inputs.GetHeight() / 100, 2));
        }

        //bmi find string is made to find the text so that the user gets a litte text to tell them about their cur-rent state.
        private void bmiFindString()
        {
            if (doubleBmi < 18.5)
            {
                stringBmi = "underweight";
            }
            else if (doubleBmi <= 25)
            {
                stringBmi = "normal weight";
            }
            else if (doubleBmi <= 30)
            {
                stringBmi = "overweight";
            }
            else if (doubleBmi <= 35)
            {
                stringBmi = "obesity class I";
            }
            else if (doubleBmi <= 40)
            {
                stringBmi = "obesity class II";
            }
            else if (doubleBmi > 40)
            {
                stringBmi = "obesity class III";
            }
        }

        //These get's is made so its easy to access the information from outside.
        public double getBmiDouble()
        {
            return doubleBmi;
        }
        public string getBmiString()
        {
            return stringBmi;
        }

        //here we use a method to send a information to energy needs and this is done to remove redun-dans
        private void checkGender()
        {
            if (inputs.GetSex())
            {
                energyNeedsCacl(-676.2);
            }
            else
            {
                energyNeedsCacl(+21);
            }
        }

        //here we cacl the energy needs
        private void energyNeedsCacl(double genderValue)
        {
            energyNeeds = ((inputs.GetWeight() * 42) + (inputs.GetHeight() * 26.3) - (20.7 * inputs.GetAge()) - genderValue) / 4.2;
        }

        //This get is made so its easy to acces the information from outside.
        public double getEnergyNeeds()
        {
            return energyNeeds;
        }

    }
    class Input
    {
        //Here we set the variables that we are going to use
        private double weight;
        private double height;
        private string cpr;
        private int age;
        //If sex is true the gender is set to woman if false the gender is man
        private bool sex = true;

        //Here we input the Weight and make access to get the information out fast and easy
        public void SetWeight(double weightInput)
        {
            weight = weightInput;
        }
        public double GetWeight()
        {
            return weight;
        }

        //Here we input the Height and make acces to get the information out fast and easy
        public void SetHeight(double heightInput)
        {
            height = heightInput;
        }
        public double GetHeight()
        {
            return height;
        }

        //Here we input the Cpr and make access to get the information out fast and easy
        //subtring the Cpr to make us able to find the ang and the gender.
        public void SetCpr(string newCpr)
        {
            cpr = newCpr;
            SetAge(cpr.Substring(0, 6));
            SetSex(Convert.ToInt32(cpr[cpr.Length - 1]));
        }
        public string GetCpr()
        {
            return cpr;
        }

        //Here we set the age by first setting our var's then we check which year the person is born and find out more specific when they have birthday.
        //This depends on the month and the date and that's what this method also cacls
        private void SetAge(string DateOfBirth)
        {
            DateTime now = DateTime.Now;
            int i;
            int month = now.Month - Convert.ToInt32(DateOfBirth.Substring(2, 2));
            int day = now.Day - Convert.ToInt32(DateOfBirth.Substring(0, 2));

            if (Convert.ToInt32(DateOfBirth.Substring(4)) > 19)
            {
                i = 1900;
            }
            else
            {
                i = 2000;
            }

            if (month < 0)
            {
                i = i + 1;
            }
            else if (month == 0)
            {
                if (day < 0)
                {
                    i = i + 1;
                }
            }

            age = now.Year - (i + Convert.ToInt32(DateOfBirth.Substring(4)));

        }
        //Easy access to get the age
        public int GetAge()
        {
            return age;
        }

        //Here we find out if its a man or a woman this depends and we use the remainder operator,
        //if the number isn't 0 its a man else its a woman
        private void SetSex(int sexInput)
        {
            if (sexInput % 2 != 0)
            {
                sex = false;
            }
        }
        //Easy access to get the gender
        public bool GetSex()
        {
            return sex;
        }
    }
    class Menu
    {
        //We set a ConsoleKeyInfo here so we don’t need to set it multiple time in deffind methods in the class.
        private ConsoleKeyInfo preesedKey;

        //Here's the start menu where you input data and it will then be sent to the input class
        public void startmenue()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 3);
            Input inputs = new Input();

            Console.Write("Input your weight in kilogram: \n - ");
            inputs.SetWeight(Convert.ToDouble(Console.ReadLine()));

            Console.Write("Input your height in centimeter: \n - ");
            inputs.SetHeight(Convert.ToDouble(Console.ReadLine()));

            Console.Write("Input your cpr-number: \n - ");
            inputs.SetCpr(Console.ReadLine());

            Console.WriteLine($"\n Your weigh is {inputs.GetWeight()}kg, height is {inputs.GetHeight()}cm, CPR is {inputs.GetCpr()}, you are {inputs.GetAge()} years old");

            //Here we make the step so that the use can get forward into what the program can do.
            Console.WriteLine("\n \n Press -> to show caclulated BMI and energy needs, or press <- to caclulate customers bic-mac problems.");
            choseMenue(inputs);
        }
        //This is made to choose which menu you want to use, we wanted to reuse this instead of making the method ask to retry but we weren’t able to find out what type a menu is.
        //So that we could put it into a var.
        private void choseMenue(Input inputs)
        {
            preesedKey = Console.ReadKey();
            if (preesedKey.Key == ConsoleKey.RightArrow)
            {
                bmiAndEnergyNeedsMenue(inputs);
            }
            else if (preesedKey.Key == ConsoleKey.LeftArrow)
            {
                customerBicMacProblems(inputs);
            }
            else
            {
                Console.WriteLine("Invalid Imput try again");
                choseMenue(inputs);
            }
            Console.WriteLine("\n\nDo you want to reuse the program then press -> else if you want to end it press <-");
            askRetry();
        }
        //Here we write the info that we've calc
        private void bmiAndEnergyNeedsMenue(Input inputs)
        {
            Calcs caclulationInfo = new Calcs();
            Console.Clear();
            Console.SetCursorPosition(0, 3);
            caclulationInfo.startCacl(inputs);

            Console.WriteLine($"Your bmi is {caclulationInfo.getBmiDouble(),1} and you are a part of the group {caclulationInfo.getBmiString()}." +
                $"\n\nYour current energy needs are:" +
                $"\n {Math.Round(caclulationInfo.getEnergyNeeds() * 1.4, 2)} kcal if you are sedentary \r-" +
                $"\n {Math.Round(caclulationInfo.getEnergyNeeds() * 1.65, 2)} kcal if you are sedentary with little movement\r-" +
                $"\n {Math.Round(caclulationInfo.getEnergyNeeds() * 1.85, 2)} kcal if you standing when you work \r-" +
                $"\n {Math.Round(caclulationInfo.getEnergyNeeds() * 2.2, 2)} kcal if you are doing hard physical work \r-");
        }
        //Here we ask for the the amount of big-mac's the user has eaten and cacl how long they should run to brun all the kcal
        private void customerBicMacProblems(Input inputs)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 3);


            Console.Write("You've said that you have a bic-mac problem. So let me hear about it, how many big-mac's do you eat in a day" +
                "\n- ");
            double distance = Math.Round((Convert.ToDouble(Console.ReadLine()) * 503) / inputs.GetWeight(), 2);
            Console.WriteLine($"You'll need to run {distance} km");
        }
        //works the same way as choseMenue it just send you to deffiend menues
        private void askRetry()
        {
            preesedKey = Console.ReadKey();
            if (preesedKey.Key == ConsoleKey.RightArrow)
            {
                startmenue();
            }
            else if (preesedKey.Key == ConsoleKey.LeftArrow)
            {
                return;
            }
            else
            {
                Console.WriteLine("Invalid Imput try again");
                askRetry();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Menu menues = new Menu();
            menues.startmenue();
        }
    }
}

